import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/application/services/app.service';
import { App } from '../../../models/generated/app';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-app-detail',
  templateUrl: './app-detail.component.html',
  styleUrls: ['./app-detail.component.scss']
})
export class AppDetailComponent implements OnInit {
  
  id: number;
  app: App;
  loaded: boolean = false;
  hide: boolean = false;

  constructor(private appService: AppService, activeRoute: ActivatedRoute) {
    this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  ngOnInit() {
    if (this.id)
      this.appService.getById(this.id)
          .subscribe((data: App) => { 
            this.app = data; 
            this.loaded = true;
          });
  }

  hidePassword() {
    this.hide = !this.hide;
  }
}
