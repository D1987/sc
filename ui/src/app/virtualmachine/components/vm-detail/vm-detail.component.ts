import { Component, OnInit, ViewChild } from '@angular/core';
import { VM } from 'src/app/models/generated/v-m';
import { ActivatedRoute } from '@angular/router';
import { VMService } from 'src/app/virtualmachine/services/vm.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';

@Component({
  selector: 'app-vm-detail',
  templateUrl: './vm-detail.component.html',
  styleUrls: ['./vm-detail.component.scss']
})
export class VMDetailComponent implements OnInit {

  @ViewChild(MatSort, {static: false}) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  searchField;
  id: number;
  vm: VM;
  loaded: boolean = false;
  dataSource: MatTableDataSource<VM>;
  displayedColumns: string[] = ['name', 'project'];
  hide: boolean = false;  

  constructor(
    private VMService: VMService,
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
      this.VMService.getById(this.id)
          .subscribe((data: VM) => { 
            this.vm = data;
            this.loaded = true;
            this.dataSource = new MatTableDataSource<any>(data.apps);
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
