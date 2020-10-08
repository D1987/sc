import { Component, OnDestroy, ChangeDetectorRef, ViewChild } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/auth/services/login.service';
import { User } from 'src/app/models/user';
import { MatSidenav } from '@angular/material/sidenav';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnDestroy {
  @ViewChild('sidenav') sidenav: MatSidenav;
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  currentUser: User;
  isExpanded = true;
  isShowing = false;

  constructor(
    changeDetectorRef: ChangeDetectorRef,
    media: MediaMatcher,
    private router: Router,
    private loginService: LoginService,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer) {
      this.mobileQuery = media.matchMedia('(max-width: 600px)');
      this._mobileQueryListener = () => changeDetectorRef.detectChanges();
      this.mobileQuery.addListener(this._mobileQueryListener);
      this.matIconRegistry.addSvgIcon(
        "host",
        this.domSanitizer.bypassSecurityTrustResourceUrl('assets/server.svg'));
      this.matIconRegistry.addSvgIcon(
        "vm",
        this.domSanitizer.bypassSecurityTrustResourceUrl('assets/desktop.svg'));
      this.matIconRegistry.addSvgIcon(
        "app",
        this.domSanitizer.bypassSecurityTrustResourceUrl('assets/website.svg'));
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  shouldRun = [/(^|\.)plnkr\.co$/, /(^|\.)stackblitz\.io$/].some(h => h.test(window.location.host));

  logout() {
    this.loginService.logout();
    this.router.navigate(['/login']);
  }
}
