export class Device {
    public id: number;
    public name: string;
    public type: string;
    public imageUrl: string;
    public isExpanded: boolean;

    constructor(args) {
        this.id = args?.id;
        this.name = args?.name;
        this.type = args?.type;
        this.imageUrl = args?.imageUrl;
    }
}