import { Component, Input, OnInit } from '@angular/core';
import { Device } from '../../../../common/models/device';
import { DeviceType } from '../../../../common/models/device-type';
import { DevicesService } from '../../../../common/services/devices.service';

@Component({
  selector: 'app-device-details',
  templateUrl: './device-details.component.html',
  styleUrls: ['./device-details.component.scss']
})
export class DeviceDetailsComponent implements OnInit {
  @Input('device') device: Device;
  deviceCopy: Device;
  deviceName: string;
  isNameEditingActive: boolean;
  isTypeChanged: boolean;
  deviceTypes: DeviceType[];

  constructor(
    private devicesService: DevicesService,
  ) { }

  ngOnInit(): void {
    this.initializeDeviceCopy();
    this.devicesService.getDeviceTypes().subscribe(types => {
      this.deviceTypes = types && types.length ? types.map(t => new DeviceType(t)) : [];
    });
  }

  cancelEditing() {
    this.isNameEditingActive = false;
    this.initializeDeviceCopy();
  }

  saveDevice() {
    this.deviceCopy.typeName = this.deviceTypes.find(d => d.id === this.deviceCopy.typeId).name;
    this.deviceCopy.imageUrl = this.deviceTypes.find(d => d.id === this.deviceCopy.typeId).imageUrl;
    this.devicesService.saveDevice(this.device.id, this.deviceCopy).subscribe(d => {
      this.device = new Device(d);
      this.isNameEditingActive = false;
      this.initializeDeviceCopy();
    });
  }

  initializeDeviceCopy() {
    this.deviceCopy = new Device(this.device);
  }
}
