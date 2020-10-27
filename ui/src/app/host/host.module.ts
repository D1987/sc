import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HostDetailComponent } from './components/host-detail/host-detail.component';
import { HostListComponent } from './components/host-list/host-list.component';
import { HostFormComponent } from './components/host-form/host-form.component';
import { HostRoutingModule } from './host-routing.module';
import { HostService } from './services/host.service';
import { HostComponent } from './components/host/host.component';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { MatTabsModule } from '@angular/material/tabs';


@NgModule({
  declarations: [
    HostDetailComponent,
    HostListComponent,
    HostFormComponent,
    HostComponent,
  ],
  imports: [
    CommonModule,
    HostRoutingModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSelectModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatCardModule,
    FormsModule,
    ClipboardModule,
    MatTabsModule
  ],
  providers: [ HostService ]
})
export class HostModule { }
