import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { routeAnimations } from '../animations';

@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.scss'],
  templateUrl: './home.component.html',
  animations: [
    routeAnimations
  ],
})
export class HomeComponent {

  get heightDiff(): number {
    const sideNav = document.getElementById('sidenav-main-content');
    return sideNav.scrollHeight - sideNav.clientHeight;
  }

  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData.animation;
  }
}
