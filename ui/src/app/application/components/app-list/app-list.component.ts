import { Component, OnInit, ViewChild } from '@angular/core';
import { AppService } from '../../services/app.service';
import { App } from '../../../models/generated/app';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/dialogs/dialog/dialog.component';
import { MatPaginator } from '@angular/material/paginator';
import { ElementRef } from '@angular/core';

@Component({
  selector: 'app-app-list',
  templateUrl: './app-list.component.html',
  styleUrls: ['./app-list.component.scss']
})
export class AppListComponent implements OnInit {

  @ViewChild(MatSort, {static: false}) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  searchField;


  displayedColumns: string[] = ['name', 'project', 'domain', 'critical', 'on', 'actions'];
  dataSource: MatTableDataSource<App>;
  app: App;
  

  constructor(private appService: AppService, private dialog: MatDialog) { }

  ngOnInit() {
      this.load();      
  }

  load() {
    this.appService.getAll().subscribe(
      (data: App[]) => {
        this.dataSource = new MatTableDataSource<App>(data);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }
    );
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  clearFilters() {    
    this.searchField = '';
    this.searchField = null;
    this.dataSource.filter = '';
    this.dataSource.filter = null;
  }

  openDialog(id: number, name: string) {
    const dialogRef = this.dialog.open(DialogComponent,{
      data: {
        message: 'Are you sure want to delete the application ' + name.toUpperCase(),
        buttonText: {
          ok: 'Delete',
          cancel: 'Cancel'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.appService.delete(id).subscribe(data => this.load());
      }
    });
  } 

}