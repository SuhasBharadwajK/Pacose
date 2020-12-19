import { Injectable } from "@angular/core";
import { Observable, of } from 'rxjs';

import { Device } from "../models/device";

@Injectable()
export class DevicesService {
    private deivces: Device[];
    constructor() {
        this.deivces = [
            new Device({
                id: 1,
                name: 'Suhas\' Pi 4',
                type: 'Raspberry Pi 4 Model B',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png'
            }),
            new Device({
                id: 2,
                name: 'Suhas\' Pi 4',
                type: 'Raspberry Pi 4 Model B',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png'
            }),
            new Device({
                id: 3,
                name: 'Suhas\' Pi 4',
                type: 'Raspberry Pi 4 Model B',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png'
            }),
            new Device({
                id: 4,
                name: 'Suhas\' Pi 4',
                type: 'Raspberry Pi 4 Model B',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png'
            }),
            new Device({
                id: 5,
                name: 'Suhas\' Pi 4',
                type: 'Raspberry Pi 4 Model B',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png'
            }),
            new Device({
                id: 6,
                name: 'Suhas\' Pi 4',
                type: 'Raspberry Pi 4 Model B',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png'
            }),
            new Device({
                id: 7,
                name: 'Suhas\' Pi 4',
                type: 'Raspberry Pi 4 Model B',
                imageUrl: 'https://projects-static.raspberrypi.org/projects/raspberry-pi-setting-up/cbae334626cbdcefe0a012a9766b408f6d5bb81a/en/images/raspberry-pi.png'
            })
        ];
    }

    public getMyDevices(): Observable<Device[]> {
        return of(this.deivces);
    }

    public getDevice(deviceId: number): Observable<Device> {
        return of(this.deivces.find(d => d.id === deviceId));
    }
}