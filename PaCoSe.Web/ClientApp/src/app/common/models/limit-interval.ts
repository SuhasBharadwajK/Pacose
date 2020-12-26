export class LimitInterval {
    public startTime: number;
    public endTime: number;

    constructor(args) {
        this.startTime = args?.startTime;
        this.endTime = args?.endTime;
    }
}
