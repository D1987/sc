import { Component, OnInit, Input, Host } from '@angular/core';
import { VM } from 'src/app/models/generated/v-m';
import { VMService } from 'src/app/virtualmachine/services/vm.service';
import { FormBuilder, Validators } from '@angular/forms';
import { AppType } from '../../../helpers/enums/apptype';
import { Pattern } from 'src/app/helpers/validators/patterns';
import { HostService } from 'src/app/host/services/host.service';
import { distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-app-form',
  templateUrl: './app-form.component.html',
  styleUrls: ['./app-form.component.scss']
})
export class AppFormComponent implements OnInit {

  @Input() appForm = this.formBuilder.group({
    id: ['0', [Validators.required]],             
    name: ['', [Validators.required]],
    ip: ['', [
      Validators.pattern(Pattern.ipPattern())
    ]],
    domain: ['', [
      Validators.pattern(Pattern.domainPattern())
    ]],
    login: [''],
    password: [''],
    description: [''],
    project: [''],
    type: ['Application', [Validators.required]],
    critical: ['true', [Validators.required]],
    enabled: ['true', [Validators.required]],
    host: this.formBuilder.group({
      id: ['', [Validators.required]]
    }),
    vm: this.formBuilder.group({
      id: ['', [Validators.required]]
    })
  }); 

  hosts: Host[];
  vms: VM[];
  selectedValue: string;
  hide: boolean = true;  
  keys = Object.keys;
  appTypes = AppType;
  timeForm: any;


  constructor(
      private hostService: HostService,
      private vmService: VMService,
      private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.loadHosts();
    this.loadVMs();
    this.onChanges();
  }

  loadHosts(): void {
    this.hostService.getAll().subscribe((data: Host[]) => this.hosts = data);
  }

  loadVMs(): void {
    this.vmService.getAll().subscribe((data: VM[]) => this.vms = data);
  }

  hidePassword() {
    this.hide = !this.hide;
  }

  onChanges() {
      this.appForm.get('host.id').valueChanges.pipe(distinctUntilChanged())
      .subscribe(id => {
          if (id !== null && id !== 0 && id !== undefined) {
              this.appForm.get('vm').reset();
              this.appForm.get('vm').disable();
          }
          else {
              this.appForm.get('vm').enable();
          }
      });

      this.appForm.get('vm.id').valueChanges.pipe(distinctUntilChanged())
      .subscribe(id => {
          if (id !== null && id !== 0 && id !== undefined) {
              this.appForm.get('host').reset();
              this.appForm.get('host').disable();
          }
          else {
              this.appForm.get('host').enable();
          }
      });
  }  
  
}
