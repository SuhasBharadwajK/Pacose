import { Component, OnInit } from '@angular/core';
import { Device } from '../../common/models/device';
import { DevicesService } from '../../common/services/devices.service';
import { MatDialog } from '@angular/material/dialog';
import { DeviceComponent } from './device/device.component';
import { ActivatedRoute } from '@angular/router';

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
    private devicesService: DevicesService,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    ) {  }

  ngOnInit(): void {
    this.devicesService.getMyDevices().subscribe(data => {
      this.devices = data && data.length ? data.map(d => new Device(d)) : [];
    });

    const deviceId = parseInt(this.route.snapshot.paramMap.get('id') || '', 10);
    if (!isNaN(deviceId) && deviceId > 0) {
      this.openDeviceDialog(deviceId);
    }
  }

  openDeviceDialog(deviceId: number) {
    const dialogRef = this.dialog.open(DeviceComponent, { data: { deviceId: deviceId },
    width: '1100px' });

    dialogRef.afterClosed().subscribe(result => {
      // Do something.
    });
  }
}
