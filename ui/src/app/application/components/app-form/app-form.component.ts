import { Component, OnInit, Input } from '@angular/core';
import { VM } from 'src/app/models/generated/v-m';
import { VMService } from 'src/app/virtualmachine/services/vm.service';
import { FormBuilder, Validators } from '@angular/forms';
import { AppType } from '../../../helpers/enums/apptype';
import { Pattern } from 'src/app/helpers/validators/patterns';

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
    vm: this.formBuilder.group({
      id: ['', [Validators.required]]
    })
  }); 

  vms: VM[];
  selectedValue: string;
  hide: boolean = true;  
  keys = Object.keys;
  appTypes = AppType;

  constructor(private vmService: VMService,
              private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.loadVMs();    
  }

  loadVMs(): void {
    this.vmService.getAll().subscribe((data: VM[]) => this.vms = data);
  }

  hidePassword() {
    this.hide = !this.hide;
  }
}
