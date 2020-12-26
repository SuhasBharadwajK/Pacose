import { Component } from '@angular/core';
import { OverlayContainer } from '@angular/cdk/overlay';

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.scss'],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  isDarkThemeActive: boolean = false;

  get currentThemeClass() {
    return this.isDarkThemeActive ? 'app-theme-dark' : 'app-theme-light';
  }

  constructor(
    private _overlayContainer: OverlayContainer
  ) {
    this.setAppTheme();
  }

  toggleTheme(isDarkThemeActive: boolean) {
    this.isDarkThemeActive = isDarkThemeActive;
    localStorage.setItem('isDarkThemeActive', this.isDarkThemeActive.toString());
    this.setOverlayTheme();
  }

  setAppTheme() {
    this.isDarkThemeActive = localStorage.getItem('isDarkThemeActive') ? localStorage.getItem('isDarkThemeActive') === 'true' : false;
    this.setOverlayTheme();
  }

  setOverlayTheme() {
    const overlayContainerClasses = this._overlayContainer.getContainerElement().classList;
    const themeClassNames = Array.from(overlayContainerClasses)
      .filter((item: string) => item.includes('app-theme-'));
    if (themeClassNames.length) {
      overlayContainerClasses.remove(...themeClassNames);
    }

    overlayContainerClasses.add(this.currentThemeClass);
  }
}
