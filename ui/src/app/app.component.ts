import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from './auth/services/login.service';
import { User } from './models/user';
      
@Component({
    selector: 'app',
    templateUrl: './app.component.html'
})
export class AppComponent {
    currentUser: User;

    constructor(
        private loginService: LoginService
    ) {
        this.loginService.currentUser.subscribe(x => this.currentUser = x);
    }
}