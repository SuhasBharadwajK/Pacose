export class DeviceType {
    public id: number;
    public name: string;
    public imageUrl: string;

    constructor(args) {
        this.id = args?.id;
        this.name = args?.name;
        this.imageUrl = args?.imageUrl;
    }
}