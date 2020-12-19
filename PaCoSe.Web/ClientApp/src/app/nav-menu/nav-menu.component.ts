import { MatSidenav } from '@angular/material/sidenav';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
    @Input('drawer') drawer: MatSidenav;

    toggle() {
      this.drawer.toggle();
  }
}
