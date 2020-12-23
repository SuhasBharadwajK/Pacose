import { DayOfTheWeek } from "./enums";
import { LimitInterval } from "./limit-interval";

export class DeviceLimit {
    public allowedHours: number;
    public dayOfTheWeek: DayOfTheWeek;
    public intervals: LimitInterval[];

    constructor(args) {
        this.allowedHours = args?.allowedHours;
        this.dayOfTheWeek = args?.dayOfTheWeek;
        this.intervals = args && args.intervals?.length ? args.intervals.map(i => new LimitInterval(i)) : [];
    }
}
