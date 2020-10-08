import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppDetailComponent } from './components/app-detail/app-detail.component';
import { AppListComponent } from './components/app-list/app-list.component';
import { AppComponent } from './components/app/app.component';

const routes: Routes = [
  {
      path: '',
      component: AppListComponent
  },
  {
      path: 'app/:id',
      component: AppComponent
  },  
  {
      path: 'appdetail/:id',
      component: AppDetailComponent
  }
  
];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
