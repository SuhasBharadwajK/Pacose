import { Component, Input, OnInit } from '@angular/core';
import { Device } from '../../../../common/models/device';

@Component({
  selector: 'app-device-settings',
  templateUrl: './device-settings.component.html',
  styleUrls: ['./device-settings.component.scss']
})
export class DeviceSettingsComponent implements OnInit {
  @Input('device') device: Device;

  constructor() { }

  ngOnInit(): void {
  }

}
