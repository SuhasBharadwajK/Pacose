import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
            { path: 'devices', component: DevicesComponent, data: { animation: 'verticalCentre' } },
            { path: 'devices/:id', component: DeviceComponent, data: { animation: 'left' } },
            { path: 'settings', component: SettingsComponent, data: { animation: 'bottom' }, pathMatch: 'full' },
            { path: 'dashboard', component: DashboardComponent, data: { animation: 'top' }, pathMatch: 'full' },
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
