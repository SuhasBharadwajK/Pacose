import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Device } from '../../../common/models/device';
import { DevicesService } from '../../../common/services/devices.service';

@Component({
  selector: 'app-device',
  templateUrl: './device.component.html',
  styleUrls: ['./device.component.scss']
})
export class DeviceComponent implements OnInit {
  device: Device;

  constructor(
    private route: ActivatedRoute,
    private devicesService: DevicesService
  ) {
  }

  ngOnInit(): void {
    const deviceId = parseInt(this.route.snapshot.paramMap.get('id') || '', 10);
    this.devicesService.getDevice(deviceId).subscribe(data => {
      this.device = new Device(data);
    });
  }
}
