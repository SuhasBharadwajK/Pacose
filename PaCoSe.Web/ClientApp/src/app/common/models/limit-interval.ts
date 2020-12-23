export class LimitInterval {
    public startTime: Date;
    public endTime: Date;

    constructor(args) {
        this.startTime = new Date(args?.startTime || new Date())
        this.endTime = new Date(args?.endTime || new Date())
    }
}
