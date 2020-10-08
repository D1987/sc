import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HostDetailComponent } from './components/host-detail/host-detail.component';
import { HostListComponent } from './components/host-list/host-list.component';
import { HostComponent } from './components/host/host.component';

const routes: Routes = [
  {
      path: '',
      component: HostListComponent
  },
  {
      path: 'host/:id',
      component: HostComponent
  },
  {
      path: 'hostdetail/:id',
      component: HostDetailComponent
  }
  
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class HostRoutingModule { }
