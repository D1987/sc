import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Pattern } from 'src/app/helpers/validators/patterns';
import { HostService } from 'src/app/host/services/host.service';
import { Host } from 'src/app/models/generated/host';

@Component({
  selector: 'app-vm-form',
  templateUrl: './vm-form.component.html',
  styleUrls: ['./vm-form.component.scss']
})
export class VMFormComponent implements OnInit {

   @Input() vmForm = this.formBuilder.group({
    id: ['0', [Validators.required]],             
    name: ['', [Validators.required]],
    ip: ['', [
      Validators.pattern(Pattern.ipPattern())
    ]],
    os: [''],
    login: ['', [Validators.required]],
    password: ['', [Validators.required]],
    description: [''],
    critical: ['true', [Validators.required]],
    enabled: ['true', [Validators.required]],
    host: this.formBuilder.group({
      id: ['', [Validators.required]]
    })
  });
  
  hosts: Host[];
  selectedValue: string;
  hide: boolean = true;

  constructor(private hostService: HostService, private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.loadVMs();
  }

  loadVMs(): void {
    this.hostService.getAll().subscribe((data: Host[]) => this.hosts = data);
  }

  get idControl() {
    return this.vmForm.get('host.id') as FormControl;
  }

  hidePassword() {
    this.hide = !this.hide;
  }
}
