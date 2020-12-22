import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
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
    private devicesService: DevicesService,
    private router: Router,
    private dialogRef: MatDialogRef<DeviceComponent>,
    @Inject(MAT_DIALOG_DATA) private data: { deviceId: number }
  ) {
    this.devicesService.getDevice(data.deviceId).subscribe(d => {
      this.device = new Device(d);
    });
  }

  ngOnInit(): void {
    this.dialogRef.beforeClosed().subscribe(() => {
      this.router.navigate(['..']);
    });
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
