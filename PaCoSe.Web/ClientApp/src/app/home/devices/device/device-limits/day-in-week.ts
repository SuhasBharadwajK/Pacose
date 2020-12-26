import { LimitInterval } from "../../../../common/models/limit-interval";

export class DayInWeek {
    dayOfTheWeek: number;
    dayName: string;
    allowedHours: number;
    intervals: LimitInterval[];

    constructor(args) {
        this.dayOfTheWeek = args?.dayOfTheWeek;
        this.dayName = args?.dayName;
        this.allowedHours = args?.allowedHours || 0;
        this.intervals = args?.intervals?.length ? args.intervals.map(l => new LimitInterval(l)) : [];
    }
}