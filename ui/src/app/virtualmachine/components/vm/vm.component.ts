import { Component, OnInit } from '@angular/core';
import { VM } from 'src/app/models/generated/v-m';
import { Router, ActivatedRoute } from '@angular/router';
import { Validators, FormBuilder } from '@angular/forms';
import { VMService } from '../../services/vm.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/dialogs/dialog/dialog.component';
import { Pattern } from 'src/app/helpers/validators/patterns';

@Component({ 
  selector: 'app-vm',
  templateUrl: './vm.component.html',
  styleUrls: ['./vm.component.scss']
})
export class VMComponent implements OnInit {

  id: number;
  vm: VM = new VM();
  vmForm = this.formBuilder.group({
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
  added: boolean = true;
  loaded: boolean = false;

  constructor(private vmService: VMService,
              private router: Router,
              activeRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private dialog: MatDialog) {
      this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  ngOnInit() {
      if (this.id){
        this.vmService.getById(this.id)
          .subscribe((data: VM) => {
            this.vmForm.get('id').setValue(data.id);
            this.vmForm.get('name').setValue(data.name);
            this.vmForm.get('ip').setValue(data.ip);
            this.vmForm.get('os').setValue(data.os);
            this.vmForm.get('login').setValue(data.login);
            this.vmForm.get('password').setValue(data.password);
            this.vmForm.get('description').setValue(data.description);
            this.vmForm.get('critical').setValue(data.critical);
            this.vmForm.get('enabled').setValue(data.enabled);
            this.vmForm.get('host.id').setValue(data.host.id);
            if (this.vm != null) this.added = false; this.loaded = true;
          });
      } else {
        // tslint:disable-next-line:align
        this.vmForm.get('critical').setValue(true);
        this.vmForm.get('enabled').setValue(true);
      }
  }

  openDialog() {
    if (this.id === 0) {

      const dialogRef = this.dialog.open(DialogComponent, {
        data: {
          message: 'Add new virual machine?',
          buttonText: {
            ok: 'Add',
            cancel: 'Cancel'
          }
        }
      });

      dialogRef.afterClosed().subscribe((confirmed: boolean) => {
        if (confirmed) {
          this.vm = this.vmForm.value;
          this.vmService.create(this.vm).subscribe(() => this.router.navigateByUrl("/vms"));
        }
      });

    } else {
      const dialogRef = this.dialog.open(DialogComponent, {
        data: {
          message: 'Update the virual machine?',
          buttonText: {
            ok: 'Update',
            cancel: 'Cancel'
          }
        }
      });

      dialogRef.afterClosed().subscribe((confirmed: boolean) => {
        if (confirmed) {
          this.vm = this.vmForm.value;
          this.vmService.update(this.vm).subscribe(() => this.router.navigateByUrl("/vms"));
        }
      });
    } 
  } 
 
}
