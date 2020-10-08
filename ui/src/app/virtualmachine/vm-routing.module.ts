import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VMDetailComponent } from './components/vm-detail/vm-detail.component';
import { VMListComponent } from './components/vm-list/vm-list.component';
import { VMComponent } from './components/vm/vm.component';

const routes: Routes = [
  {
      path: '',
      component: VMListComponent
  },
  {
      path: 'vm/:id',
      component: VMComponent
  },
  {
      path: 'vmdetail/:id',
      component: VMDetailComponent
  }
  
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class VMRoutingModule { }
