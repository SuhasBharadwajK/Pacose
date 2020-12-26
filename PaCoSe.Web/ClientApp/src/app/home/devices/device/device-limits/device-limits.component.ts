import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { DeviceLimit } from '../../../../common/models/deivce-limits';
import { Device } from '../../../../common/models/device';
import { DeviceConfig } from '../../../../common/models/device-config';
import { DayOfTheWeek } from '../../../../common/models/enums';
import { DevicesService } from '../../../../common/services/devices.service';
import { AddDeviceLimitsComponent } from './add-device-limits/add-device-limits.component';
import { DayInWeek } from './day-in-week';

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
    private dialog: MatDialog,
    ) {
    this.headerHours = [0, 4, 8, 12, 16, 20];
    this.hours = [...Array(48).keys()];
    this.allowedHours = [...Array(24).keys()].map(f => (f + 1) / 2);
  }

  ngOnInit(): void {
    this.initializeDeviceCopy();
    for (let value in DayOfTheWeek) {
      const dayOfTheWeek = parseInt(DayOfTheWeek[value], 10);
      if (!isNaN(dayOfTheWeek)) {
        const limitIndex = this.device.deviceLimits.findIndex(l => l.dayOfTheWeek === dayOfTheWeek);

        this.daysOfTheWeek.push(new DayInWeek({
          dayOfTheWeek: dayOfTheWeek,
          dayName: value,
          ...(limitIndex >= 0 ? new DeviceLimit(this.device.deviceLimits[limitIndex]) : {})
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
    const limitIndex = this.deviceCopy.deviceLimits.findIndex(l => l.dayOfTheWeek === dayInWeek.dayOfTheWeek);
    if (limitIndex < 0) {
      this.deviceCopy.deviceLimits.push(new DeviceLimit({
        allowedHours: dayInWeek.allowedHours,
        dayOfTheWeek: dayInWeek.dayOfTheWeek,
        intervals: dayInWeek.intervals
      }));
    } else {
      this.deviceCopy.deviceLimits[limitIndex].allowedHours = dayInWeek.allowedHours || 0;
      this.deviceCopy.deviceLimits[limitIndex].dayOfTheWeek = dayInWeek.dayOfTheWeek;
      this.deviceCopy.deviceLimits[limitIndex].intervals = dayInWeek.intervals;
    }

    this.saveDevice();
  }

  openLimitsDialog(dayInWeek: DayInWeek) {
    const index = this.deviceCopy.deviceLimits.findIndex(l => l.dayOfTheWeek === dayInWeek.dayOfTheWeek);

    dayInWeek.intervals = index < 0 ? [] : this.deviceCopy.deviceLimits[index].intervals;

    const dialogRef = this.dialog.open(AddDeviceLimitsComponent, { data: { dayInWeek: dayInWeek, device: this.deviceCopy },
    width: '505px' });

    dialogRef.afterClosed().subscribe((result: DayInWeek) => {
      if (result) {
        if (index < 0) {
          this.deviceCopy.deviceLimits.push(new DeviceLimit(result));
        } else {
          this.deviceCopy.deviceLimits[index].intervals = result.intervals;
        }

        dayInWeek.intervals = result.intervals;

        this.saveDevice();
      }
    });
  }

  saveDevice() {
    const deviceConfig = new DeviceConfig(this.deviceCopy);
    this.devicesService.addDeviceLimits(this.device.id, deviceConfig).subscribe(result => {
      this.device.deviceLimits = result.deviceLimits;
      this.device.isScreenTimeEnabled = result.isScreenTimeEnabled;
    });
  }

  trackDayOfWeekBy(index: number, day: DayInWeek): number {
    return day.dayOfTheWeek;
  }

  trackByIndex(index: number, item: number): number {
    return index;
  }

  isTimeActive(hour: number, dayOfTheWeek: DayInWeek) {
    return dayOfTheWeek.intervals.some(i => i.startTime <= hour / 2 && i.endTime >= hour / 2);
  }
}
