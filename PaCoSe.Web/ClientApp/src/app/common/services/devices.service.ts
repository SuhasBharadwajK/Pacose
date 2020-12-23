import { Injectable } from "@angular/core";
import { Observable, of } from 'rxjs';

import { Device } from "../models/device";
import { DeviceConfig } from "../models/device-config";
import { DeviceType } from "../models/device-type";

@Injectable()
export class DevicesService {
    private devices: Device[];
    private deviceTypes: DeviceType[];
    
    constructor() {
        this.devices = [
            new Device({
                id: 1,
                name: 'Suhas\' Pi 4',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png',
                typeName: 'Raspberry Pi 4 Model B',
                typeId: 3,
            }),
            new Device({
                id: 2,
                name: 'Suhas\' Pi 4',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png',
                typeName: 'Raspberry Pi 4 Model B',
                typeId: 3,
            }),
            new Device({
                id: 3,
                name: 'Suhas\' Pi 4',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png',
                typeName: 'Raspberry Pi 4 Model B',
                typeId: 3,
            }),
            new Device({
                id: 4,
                name: 'Suhas\' Pi 4',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png',
                typeName: 'Raspberry Pi 4 Model B',
                typeId: 3,
            }),
            new Device({
                id: 5,
                name: 'Suhas\' Pi 4',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png',
                typeName: 'Raspberry Pi 4 Model B',
                typeId: 3,
            }),
            new Device({
                id: 6,
                name: 'Suhas\' Pi 4',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png',
                typeName: 'Raspberry Pi 4 Model B',
                typeId: 3,
            }),
            new Device({
                id: 7,
                name: 'Suhas\' Pi 4',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png',
                typeName: 'Raspberry Pi 4 Model B',
                typeId: 3,
            })
        ];

        this.deviceTypes = [
            new DeviceType({
                id: 1,
                name: 'Raspberry Pi 2 Model B'
            }),
            new DeviceType({
                id: 2,
                name: 'Raspberry Pi B+'
            }),
            new DeviceType({
                id: 3,
                name: 'Raspberry Pi 4 Model B'
            }),
            new DeviceType({
                id: 4,
                name: 'Raspberry Pi 400'
            }),
        ];
    }

    public getMyDevices = (): Observable<Device[]> => {
        return of(this.devices);
    }

    public getDevice = (deviceId: number): Observable<Device> => {
        return of(this.devices.find(d => d.id === deviceId));
    }

    public saveDevice = (id: number, device: Device): Observable<Device> => {
        for (let i = 0; i < this.devices.length; i++) {
            const element = this.devices[i];
            if (element.id === id) {
                this.devices[i] = device;
            }
        }

        return of(device);
    }

    public getDeviceTypes = (): Observable<DeviceType[]> => {
        return of(this.deviceTypes);
    }

    public toggleDeviceLimits = (id: number): Observable<boolean> => {
        let device: Device = null;
        for (let i = 0; i < this.devices.length; i++) {
            const element = this.devices[i];
            if (element.id === id) {
                this.devices[i].isScreenTimeEnabled = !this.devices[i].isScreenTimeEnabled;
                device = this.devices[i];
            }
        }

        return of(device.isScreenTimeEnabled);
    }

    public addDeviceLimits = (id: number, config: DeviceConfig): Observable<DeviceConfig> => {
        for (let i = 0; i < this.devices.length; i++) {
            const element = this.devices[i];
            if (element.id === id) {
                this.devices[i].deviceLimits = config.deviceLimits;
                this.devices[i].isScreenTimeEnabled = config.isScreenTimeEnabled;
            }
        }

        return of(config);
    }
}