import { DeviceLimit } from "./deivce-limits";

export class DeviceConfig {
    public name: string;
    public isScreenTimeEnabled: boolean;
    public deviceLimits: DeviceLimit[];

    constructor(args) {
        this.name = args?.name;
        this.isScreenTimeEnabled = args?.isScreenTimeEnabled || false;
        this.deviceLimits = args?.deviceLimits?.length ? args.deviceLimits.map(l => new DeviceLimit(l)) : []
    }
}