import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FlexLayoutModule } from "@angular/flex-layout";

import { AppRoutingModule } from './app.routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MaterialComponentsModule } from './material-module';
import { DevicesComponent } from './home/devices/devices.component';
import { SettingsComponent } from './home/settings/settings.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { HeaderComponent } from './header/header.component';
import { DeviceComponent } from './home/devices/device/device.component';
import { DevicesService } from './common/services/devices.service';
import { DeviceDetailsComponent } from './home/devices/device/device-details/device-details.component';
import { DeviceLimitsComponent } from './home/devices/device/device-limits/device-limits.component';
import { DeviceSettingsComponent } from './home/devices/device/device-settings/device-settings.component';
import { DeviceOwnersComponent } from './home/devices/device/device-owners/device-owners.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DevicesComponent,
    SettingsComponent,
    DashboardComponent,
    HeaderComponent,
    DeviceComponent,
    DeviceDetailsComponent,
    DeviceLimitsComponent,
    DeviceSettingsComponent,
    DeviceOwnersComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FlexLayoutModule,
    FormsModule,
    MaterialComponentsModule,
    AppRoutingModule,
  ],
  providers: [
    DevicesService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
