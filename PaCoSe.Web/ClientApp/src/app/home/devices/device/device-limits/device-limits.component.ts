import { Component, Input, OnInit } from '@angular/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { DeviceLimit } from '../../../../common/models/deivce-limits';
import { Device } from '../../../../common/models/device';
import { DeviceConfig } from '../../../../common/models/device-config';
import { DayOfTheWeek } from '../../../../common/models/enums';
import { LimitInterval } from '../../../../common/models/limit-interval';
import { DevicesService } from '../../../../common/services/devices.service';

@Component({
  selector: 'app-device-limits',
  templateUrl: './device-limits.component.html',
  styleUrls: ['./device-limits.component.scss']
})
export class DeviceLimitsComponent implements OnInit {
  @Input('device') device: Device;
  deviceCopy: Device;
  daysOfTheWeek: DayInWeek[] = [];
  hours: number[];
  allowedHours: number[];
  headerHours: number[];

  constructor(
    private devicesService: DevicesService,
  ) {
    this.headerHours = [0, 4, 8, 12, 16, 20];
    this.hours = [...Array(48).keys()];
    this.allowedHours = [...Array(24).keys()].map(f => (f + 1) / 2);
  }

  ngOnInit(): void {
    this.initializeDeviceCopy();
    for (let value in DayOfTheWeek) {
      const dayNumber = parseInt(DayOfTheWeek[value], 10);
      if (!isNaN(dayNumber)) {
        const limitIndex = this.device.deviceLimits.findIndex(l => l.dayOfTheWeek === dayNumber);

        this.daysOfTheWeek.push(new DayInWeek({
          dayNumber: dayNumber,
          dayName: value,
          ...(limitIndex >= 0 ? this.device.deviceLimits[limitIndex] : {})
        }));
      }
    }
  }

  toggleDeviceScreenTime(event: MatSlideToggleChange) {
    this.devicesService.toggleDeviceLimits(this.deviceCopy.id).subscribe(result => {
      this.device.isScreenTimeEnabled = result;
      this.initializeDeviceCopy();
    });
  }

  initializeDeviceCopy() {
    this.deviceCopy = new Device(this.device || {});
  }

  getTime(hour: number): string {
    const date = new Date();
    date.setHours(hour);
    return date.toLocaleString('en-IN', { hour: 'numeric', hour12: true })
  }

  getHoursLabel(hoursCount: number) {
    const hours = Math.floor(hoursCount);
    const minutes = hoursCount - Math.floor(hoursCount);
    return `${hours ? hours + ' h' : ''} ${minutes ? 60 * minutes + ' min' : ''}`.trim();
  }

  onAllowedHoursUpdated(dayInWeek: DayInWeek) {
    const limitIndex = this.deviceCopy.deviceLimits.findIndex(l => l.dayOfTheWeek === dayInWeek.dayNumber);
    if (limitIndex < 0) {
      this.deviceCopy.deviceLimits.push(new DeviceLimit({
        allowedHours: dayInWeek.allowedHours,
        dayOfTheWeek: dayInWeek.dayNumber,
        intervals: dayInWeek.intervals
      }));
    } else {
      this.deviceCopy.deviceLimits[limitIndex].allowedHours = dayInWeek.allowedHours;
      this.deviceCopy.deviceLimits[limitIndex].dayOfTheWeek = dayInWeek.dayNumber;
      this.deviceCopy.deviceLimits[limitIndex].intervals = dayInWeek.intervals;
    }

    const deviceConfig = new DeviceConfig(this.deviceCopy);
    this.devicesService.addDeviceLimits(this.device.id, deviceConfig).subscribe(result => {
      this.device.deviceLimits = result.deviceLimits;
      this.device.isScreenTimeEnabled = result.isScreenTimeEnabled;
    });
  }

  trackByFn(index, item) {
    return index;
    // or if you have no unique identifier:
    // return index;
  }
}

class DayInWeek {
  dayNumber: number;
  dayName: string;
  allowedHours: number;
  intervals: LimitInterval[];

  constructor(args) {
    this.dayNumber = args?.dayNumber;
    this.dayName = args?.dayName;
    this.allowedHours = args?.allowedHours || 0;
    this.intervals = args?.intervals?.length ? args.intervals.map(l => new LimitInterval(l)) : [];
  }
}
