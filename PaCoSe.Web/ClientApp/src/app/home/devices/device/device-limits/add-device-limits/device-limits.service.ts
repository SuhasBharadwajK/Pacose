import { Injectable } from "@angular/core";

import { LimitInterval } from "../../../../../common/models/limit-interval";
import { DayInWeek } from "../day-in-week";

@Injectable()
export class DeviceLimitsService {
    dayInWeek: DayInWeek;

    constructor() {
    }

    init = (dayInWeek: DayInWeek) => {
        this.dayInWeek = dayInWeek;
    }

    addInterval = (newInterval: LimitInterval) => {
        if (this.dayInWeek.intervals.length === 0 || this.dayInWeek.intervals.every(i => i.endTime < newInterval.startTime || i.startTime > newInterval.endTime)) {
            this.pushNewInterval(newInterval);
        } else {
            const intervals = this.dayInWeek.intervals;
            const { headIndex, tailIndex } = this.getHeadAndTailIndices(intervals, newInterval);

            const headInterval = intervals[headIndex];
            const tailInterval = intervals[tailIndex];

            if (newInterval.startTime < headInterval.startTime) {
                headInterval.startTime = newInterval.startTime;
            }

            if (newInterval.endTime > tailInterval.endTime) {
                headInterval.endTime = newInterval.endTime;
            } else {
                headInterval.endTime = tailInterval.endTime;
            }

            if (headIndex !== tailIndex) {
                intervals.splice(headIndex + 1, tailIndex - headIndex);
            }
        }
    }

    getHeadAndTailIndices = (intervals: LimitInterval[], newInterval: LimitInterval) => {
        const headIndex = intervals.findIndex((x, index) => x.endTime === newInterval.startTime
            || x.startTime === newInterval.startTime
            || (x.startTime <= newInterval.startTime && x.endTime >= newInterval.startTime)
            || (x.startTime >= newInterval.startTime && (index === 0 || intervals[index - 1].endTime < newInterval.startTime)));

        const tailIndex = intervals.findIndex((x, index) => x.startTime === newInterval.endTime
            || x.endTime === newInterval.endTime
            || (x.startTime <= newInterval.endTime && x.endTime >= newInterval.endTime)
            || (x.endTime <= newInterval.endTime && (index === intervals.length - 1 || intervals[index + 1].startTime > newInterval.endTime)));

        return { headIndex, tailIndex };
    }

    private pushNewInterval = (limitInterval: LimitInterval) => {
      this.dayInWeek.intervals.push(new LimitInterval({ ...limitInterval }));
      this.dayInWeek.intervals.sort((a, b) => { if (a.startTime < b.startTime) return -1; if (a.startTime > b.startTime) return 1; return 0 });
    }
}