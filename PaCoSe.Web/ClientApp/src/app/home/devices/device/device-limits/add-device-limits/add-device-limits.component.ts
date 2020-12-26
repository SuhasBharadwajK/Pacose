import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Device } from '../../../../../common/models/device';
import { LimitInterval } from '../../../../../common/models/limit-interval';
import { DayInWeek } from '../day-in-week';
import { DeviceLimitsService } from './device-limits.service';

@Component({
  selector: 'app-add-device-limits',
  templateUrl: './add-device-limits.component.html',
  styleUrls: ['./add-device-limits.component.scss']
})
export class AddDeviceLimitsComponent implements OnInit {
  device: Device;
  dayInWeek: DayInWeek;
  dayInWeekCopy: DayInWeek;
  hoursInDay: number[];
  newInterval: LimitInterval;

  get endHours(): number[] {
    return this.hoursInDay?.filter(h => h > this.newInterval.startTime) || [];
  }

  get startHours(): number[] {
    return this.hoursInDay?.slice(0, this.hoursInDay.length - 1) || [];
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: { dayInWeek: DayInWeek, device: Device },
    private dialogRef: MatDialogRef<AddDeviceLimitsComponent>,
    private deviceLimitsService: DeviceLimitsService,
  ) {
    this.device = data.device;
    this.dayInWeek = data.dayInWeek;
    this.dayInWeekCopy = new DayInWeek(this.dayInWeek);
    this.hoursInDay = [...Array(49).keys()].map(f => f / 2);
    this.newInterval = new LimitInterval({ startTime: this.hoursInDay[0], endTime: this.hoursInDay[1] });
    this.deviceLimitsService.init(this.dayInWeekCopy);
  }

  ngOnInit(): void {
  }

  save() {
    this.dialogRef.close(this.dayInWeekCopy);
  }

  cancel() {
    this.dialogRef.close();
  }

  getTime(hour: number): string {
    const date = new Date();
    const minutes = hour - Math.floor(hour);
    date.setHours(hour);
    date.setMinutes(minutes * 60);
    return date.toLocaleString('en-IN', { hour: 'numeric', minute: 'numeric', hour12: true })
  }

  addInterval() {
    this.deviceLimitsService.addInterval(this.newInterval);
  }

  onStartTimeChanged() {
    if (this.newInterval.endTime <= this.newInterval.startTime && this.newInterval.endTime > 0) {
      this.newInterval.endTime = this.endHours[1] || 0;
    }
  }

  deleteInterval(intervalIndex: number) {
    this.dayInWeekCopy.intervals.splice(intervalIndex, 1);
  }
}
