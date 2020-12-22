export class Device {
    public id: number;
    public name: string;
    public imageUrl: string;
    public typeName: string;
    public typeId: number;
    public isExpanded: boolean;

    constructor(args) {
        this.id = args?.id;
        this.name = args?.name;
        this.imageUrl = args?.imageUrl;
        this.typeName = args?.typeName;
        this.typeId = args?.typeId;
    }
}