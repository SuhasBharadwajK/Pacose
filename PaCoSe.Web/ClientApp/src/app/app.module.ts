import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FlexLayoutModule } from "@angular/flex-layout";

import { AppRoutingModule } from './app.routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialComponentsModule } from './material-module';
import { DevicesComponent } from './home/devices/devices.component';
import { SettingsComponent } from './home/settings/settings.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { HeaderComponent } from './header/header.component';
import { DeviceComponent } from './home/devices/device/device.component';
import { DevicesService } from './common/services/devices.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    DevicesComponent,
    SettingsComponent,
    DashboardComponent,
    HeaderComponent,
    DeviceComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FlexLayoutModule,
    FormsModule,
    MaterialComponentsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
  ],
  providers: [
    DevicesService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
