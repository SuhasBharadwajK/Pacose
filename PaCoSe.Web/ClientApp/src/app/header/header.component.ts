import { MatSidenav } from '@angular/material/sidenav';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  @Input('drawer') drawer: MatSidenav;
  @Input('isDarkThemeActive') isDarkThemeActive: boolean;
  @Output('switchTheme') switchTheme: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor() {
  }

  toggle() {
    this.drawer.toggle();
  }

  toggleTheme() {
    this.switchTheme?.emit(!this.isDarkThemeActive);
  }
}
