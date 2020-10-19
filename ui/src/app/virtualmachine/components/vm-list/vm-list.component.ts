import { Component, OnInit, ViewChild } from '@angular/core';
import { VMService } from 'src/app/virtualmachine/services/vm.service';
import { VM } from 'src/app/models/generated/v-m';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { DialogComponent } from 'src/app/dialogs/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';

// const ipInt = require('ip-to-int');

@Component({
  selector: 'app-vm-list',
  templateUrl: './vm-list.component.html',
  styleUrls: ['./vm-list.component.scss']
})
export class VMListComponent implements OnInit {

  @ViewChild(MatSort, {static: false}) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  searchField;
  displayedColumns: string[] = ['name', 'ip', 'critical', 'on', 'actions'];
  dataSource: MatTableDataSource<VM>;
  
  constructor(private vmService: VMService, private dialog: MatDialog) { }

  ngOnInit() {
      this.load();
  }

  load() {
      this.vmService.getAll().subscribe(
        (data: VM[]) => {
        this.dataSource = new MatTableDataSource<VM>(data);
        this.dataSource.sortingDataAccessor = (item, property) => {
          switch(property) {
            case 'ip': {
              var ipl=0;
              item.ip.split('.').forEach(function( octet ) {
                  ipl<<=8;
                  ipl+=parseInt(octet);
              });
              return(ipl >>>0);
            }
            default: {
              return item[property];
            }
          }                
        };
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      });
  }

  openDialog(id: number, name: string) {
    const dialogRef = this.dialog.open(DialogComponent,{
      data: {
        message: 'Are you sure want to delete the virual machine ' + name.toUpperCase() + '? All applications will be delete',
        buttonText: {
          ok: 'Delete',
          cancel: 'Cancel'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.vmService.delete(id).subscribe(data => this.load());
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
