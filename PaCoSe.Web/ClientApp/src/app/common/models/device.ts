import { DeviceLimit } from "./deivce-limits";

export class Device {
    public id: number;
    public name: string;
    public imageUrl: string;
    public token: string;
    public isScreenTimeEnabled: boolean;
    public deviceLimits: DeviceLimit[];
    public typeName: string;
    public typeId: number;
    public isExpanded: boolean;

    constructor(args) {
        this.id = args?.id;
        this.name = args?.name;
        this.imageUrl = args?.imageUrl;
        this.isScreenTimeEnabled = args?.isScreenTimeEnabled || false;
        this.deviceLimits = args && args.deviceLimits?.length ? args.deviceLimits.map(l => new DeviceLimit(l)) : [];
        this.token = args?.token;
        this.typeName = args?.typeName;
        this.typeId = args?.typeId;
    }
}