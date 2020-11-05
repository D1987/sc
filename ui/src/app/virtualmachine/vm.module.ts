import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VMFormComponent } from './components/vm-form/vm-form.component';
import { VMDetailComponent } from './components/vm-detail/vm-detail.component';
import { VMListComponent } from './components/vm-list/vm-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VMRoutingModule } from './vm-routing.module';
import { VMService } from './services/vm.service';
import { VMComponent } from './components/vm/vm.component';
import { MatSelectModule } from '@angular/material/select';
import { HostService } from '../host/services/host.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatRadioModule, MAT_RADIO_DEFAULT_OPTIONS } from '@angular/material/radio';
import { ClipboardModule } from '@angular/cdk/clipboard';


@NgModule({
  declarations: [
    VMFormComponent,
    VMDetailComponent,
    VMListComponent,
    VMComponent
  ],
  imports: [
    CommonModule,
    VMRoutingModule,
    ReactiveFormsModule,
    MatPaginatorModule,
    MatSelectModule,
    MatSortModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatGridListModule,
    MatRadioModule,
    FormsModule,
    ClipboardModule
  ],
  providers: [ 
    VMService,
    HostService,
    {
      provide: MAT_RADIO_DEFAULT_OPTIONS,
      useValue: { color: 'primary' },
    } 
  ]
})
export class VMModule { }
