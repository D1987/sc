import { Component, OnInit, ViewChild } from '@angular/core';
import { Host } from 'src/app/models/generated/host';
import { ActivatedRoute } from '@angular/router';
import { HostService } from 'src/app/host/services/host.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { App } from 'src/app/models/generated/app';

@Component({
  selector: 'app-host-detail',
  templateUrl: './host-detail.component.html',
  styleUrls: ['./host-detail.component.scss']
})
export class HostDetailComponent implements OnInit {

    @ViewChild('paginatorVm') paginatorVm: MatPaginator;
    @ViewChild('paginatorApp') paginatorApp: MatPaginator;    
    @ViewChild(MatSort, {static: true}) sort: MatSort;

    searchField;
    id: number;
    host: Host;
    loaded: boolean = false;
    dataSourceVm: MatTableDataSource<Host>;
    dataSourceApp: MatTableDataSource<App>;   
    displayedVmColumns: string[] = ['name'];
    displayedAppColumns: string[] = ['name','project'];
    hide: boolean = false;
    shownVm: boolean = true;
    shownApp: boolean = true;

    constructor(
      private hostService: HostService,
      activeRoute: ActivatedRoute,
      private matIconRegistry: MatIconRegistry,
      private domSanitizer: DomSanitizer) {
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
        this.matIconRegistry.addSvgIcon(
          "copy",
          this.domSanitizer.bypassSecurityTrustResourceUrl('assets/copy-content.svg'));          
    }
 
    ngOnInit() {
      if (this.id)
        this.hostService.getById(this.id)
            .subscribe((data: Host) => {
            this.host = data;
            this.loaded = true;

            if (this.host.vms.length === 0) {
              this.shownVm = false
            }

            if (this.host.apps.length === 0) {
              this.shownApp = false
            }

            this.dataSourceVm = new MatTableDataSource<any>(data.vms);
            this.dataSourceVm.sort = this.sort;
            !this.dataSourceVm.paginator ? this.dataSourceVm.paginator = this.paginatorVm : null;
        
            this.dataSourceApp = new MatTableDataSource<any>(data.apps);
            this.dataSourceApp.sort = this.sort;
            !this.dataSourceApp.paginator ? this.dataSourceApp.paginator = this.paginatorApp : null;
           
        });
    } 

    applyFilterVm(event: Event) {
      const filterValue = (event.target as HTMLInputElement).value;
      this.dataSourceVm.filter = filterValue.trim().toLowerCase();
      
      if (this.dataSourceVm.paginator) {
        this.dataSourceVm.paginator.firstPage();
      }
    }

    applyFilterApp(event: Event) {
      const filterValue = (event.target as HTMLInputElement).value;
      this.dataSourceApp.filter = filterValue.trim().toLowerCase();

      if (this.dataSourceApp.paginator) {
        this.dataSourceApp.paginator.firstPage();
      }
    }

    clearFiltersVm() {    
      this.searchField = '';
      this.searchField = null;
      this.dataSourceVm.filter = '';
      this.dataSourceVm.filter = null;
    }

    clearFiltersApp() {    
      this.searchField = '';
      this.searchField = null;
      this.dataSourceApp.filter = '';
      this.dataSourceApp.filter = null;
    }

    clearFilters() {    
      this.searchField = '';
      this.searchField = null;
      
      this.dataSourceVm.filter = '';
      this.dataSourceVm.filter = null;
    
      this.dataSourceApp.filter = '';
      this.dataSourceApp.filter = null;
      
    }

    hidePassword() {
      this.hide = !this.hide;
    }
}
