import { Component, Input, OnInit } from '@angular/core';
import { Device } from '../../../../common/models/device';

@Component({
  selector: 'app-device-owners',
  templateUrl: './device-owners.component.html',
  styleUrls: ['./device-owners.component.scss']
})
export class DeviceOwnersComponent implements OnInit {
  @Input('device') device: Device;

  constructor() { }

  ngOnInit(): void {
  }

}
