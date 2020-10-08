import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/dialogs/dialog/dialog.component';
import { HostService } from 'src/app/host/services/host.service';
import { Host } from '../../../models/generated/host';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-host-list',
  templateUrl: './host-list.component.html',
  styleUrls: ['./host-list.component.scss'],
  providers: [HostService]
})
export class HostListComponent implements OnInit {
  
  @ViewChild(MatSort, {static: false}) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  searchField;   
  displayedColumns: string[] = ['name', 'location', 'actions'];
  dataSource: MatTableDataSource<Host>;
  
  constructor(private hostService: HostService, private dialog: MatDialog) { }

  ngOnInit() {
    this.load();  
  }

  load() {
    this.hostService.getAll().subscribe(
      (data: Host[]) => {
        this.dataSource = new MatTableDataSource<Host>(data);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      });
  }

  openDialog(id: number, name: string) {
    const dialogRef = this.dialog.open(DialogComponent,{
      data: {
        message: 'Are you sure want to delete the host ' + name.toUpperCase()
         + '? All vm and applications will be delete',
        buttonText: {
          ok: 'Delete',
          cancel: 'Cancel'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.hostService.delete(id).subscribe(data => this.load());
      }
    });
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
}
