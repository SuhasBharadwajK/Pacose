import { Component, OnInit } from '@angular/core';
import { Device } from '../../common/models/device';
import { DevicesService } from '../../common/services/devices.service';

@Component({
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.scss']
})
export class DevicesComponent implements OnInit {
  devices: Device[] = [];

  get isAnyDeviceExpanded(): boolean {
    return this.devices.some(d => d.isExpanded);
  }

  constructor(
    private devicesService: DevicesService
  ) {  }

  ngOnInit(): void {
    this.devicesService.getMyDevices().subscribe(data => {
      this.devices = data && data.length ? data.map(d => new Device(d)) : [];
    });
  }

  expand(device: Device) {
    this.devices.forEach(d => {
      if (d.id === device.id) {
        d.isExpanded = true;
      } else {
        d.isExpanded = false;
      }
    });
  }
}
