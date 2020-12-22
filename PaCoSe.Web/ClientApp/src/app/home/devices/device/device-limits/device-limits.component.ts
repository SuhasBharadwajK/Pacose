import { Component, Input, OnInit } from '@angular/core';
import { Device } from '../../../../common/models/device';

@Component({
  selector: 'app-device-limits',
  templateUrl: './device-limits.component.html',
  styleUrls: ['./device-limits.component.scss']
})
export class DeviceLimitsComponent implements OnInit {
  @Input('device') device: Device;

  constructor() { }

  ngOnInit(): void {
  }

}
