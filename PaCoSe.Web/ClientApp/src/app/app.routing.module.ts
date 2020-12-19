import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { DeviceComponent } from './home/devices/device/device.component';
import { DevicesComponent } from './home/devices/devices.component';
import { HomeComponent } from './home/home.component';
import { SettingsComponent } from './home/settings/settings.component';

const routes: Routes = [
    { path: '', redirectTo: 'app', pathMatch: 'full' },
    {
        path: 'app', component: HomeComponent, children: [
            { path: '', redirectTo: 'devices', pathMatch: 'full' },
            { path: 'devices', component: DevicesComponent, pathMatch: 'full' },
            { path: 'devices/:id', component: DeviceComponent, pathMatch: 'full' },
            { path: 'settings', component: SettingsComponent, pathMatch: 'full' },
            { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
            { path: 'counter', component: CounterComponent, pathMatch: 'full' },
            { path: 'fetch-data', component: FetchDataComponent, pathMatch: 'full' },
        ]
    },
    // { path: 'login', component: LoginComponent, pathMatch: 'full' },
    // { path: 'list', component: RedirectHandlerComponent, pathMatch: 'full' },
    // TODO: Add unauthorized page.
    { path: '**', redirectTo: '' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
