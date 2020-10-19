import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppDetailComponent } from './components/app-detail/app-detail.component';
import { AppFormComponent } from './components/app-form/app-form.component';
import { AppListComponent } from './components/app-list/app-list.component';
import { AppRoutingModule } from '../application/app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppService } from './services/app.service';
import { AppComponent } from './components/app/app.component';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule, MAT_RADIO_DEFAULT_OPTIONS } from '@angular/material/radio';
import { ClipboardModule } from '@angular/cdk/clipboard';



@NgModule({
  declarations: [
    AppDetailComponent,
    AppFormComponent,
    AppListComponent,
    AppComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
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
    MatRadioModule,
    FormsModule,
    ClipboardModule
  ],
  providers: [ 
    AppService,
    {
      provide: MAT_RADIO_DEFAULT_OPTIONS,
      useValue: { color: 'primary' },
    } 
  ]
})
export class AppModule { }
