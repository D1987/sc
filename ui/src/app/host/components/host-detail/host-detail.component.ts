import { Component, OnInit, ViewChild } from '@angular/core';
import { Host } from 'src/app/models/generated/host';
import { ActivatedRoute } from '@angular/router';
import { HostService } from 'src/app/host/services/host.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';


@Component({
  selector: 'app-host-detail',
  templateUrl: './host-detail.component.html',
  styleUrls: ['./host-detail.component.scss']
})
export class HostDetailComponent implements OnInit {

    @ViewChild(MatSort, {static: false}) sort: MatSort;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    searchField;
    id: number;
    host: Host;
    loaded: boolean = false;
    dataSource: MatTableDataSource<Host>;    
    displayedColumns: string[] = ['name'];
    hide: boolean = false;
 
    constructor(private hostService: HostService, activeRoute: ActivatedRoute) {
      this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }
 
    ngOnInit() {
      if (this.id)
        this.hostService.getById(this.id)
            .subscribe((data: Host) => {
            this.host = data;            
            this.loaded = true;
            this.dataSource = new MatTableDataSource<any>(data.vms);
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.paginator;
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

    hidePassword() {
      this.hide = !this.hide;
    }
}
