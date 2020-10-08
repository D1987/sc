import { Component, OnInit } from '@angular/core';
import { HostService } from '../../services/host.service';
import { Host } from 'src/app/models/generated/host';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { DialogComponent } from 'src/app/dialogs/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Pattern } from '../../../helpers/validators/patterns';

@Component({
  selector: 'app-host',
  templateUrl: './host.component.html',
  styleUrls: ['./host.component.scss']
})
export class HostComponent implements OnInit {
  
  id: number;
  host: Host = new Host();
  hostForm = this.formBuilder.group({
    id: ['0', [Validators.required]],             
    name: ['', [Validators.required]],
    ip: ['', [
      Validators.pattern(Pattern.ipPattern())
    ]],
    os: [''],
    login: ['', [Validators.required]],
    password: ['', [Validators.required]],
    description: [''],
    location: ['Local', [Validators.required]]
  });
  added: boolean = true;
  loaded: boolean = false;

  constructor(private hostService: HostService,
              private router: Router,
              activeRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private dialog: MatDialog) {
      this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  ngOnInit() {
      if (this.id)
          this.hostService.getById(this.id)
              .subscribe((data: Host) => {
                this.hostForm.get('id').setValue(data.id);
                this.hostForm.get('name').setValue(data.name);
                this.hostForm.get('ip').setValue(data.ip);
                this.hostForm.get('os').setValue(data.os);
                this.hostForm.get('login').setValue(data.login);
                this.hostForm.get('password').setValue(data.password);
                this.hostForm.get('location').setValue(data.location);
                this.hostForm.get('description').setValue(data.description);
                if (this.host != null) this.added = false; this.loaded = true;
              });
  }

  openDialog() {
    if (this.id === 0) {

      const dialogRef = this.dialog.open(DialogComponent, {
        data: {
          message: 'Add new host?',
          buttonText: {
            ok: 'Add',
            cancel: 'Cancel'
          }
        }
      });

      dialogRef.afterClosed().subscribe((confirmed: boolean) => {
        if (confirmed) {
          this.host = this.hostForm.value;
          this.hostService.create(this.host).subscribe(data => this.router.navigateByUrl("/hosts"));
        }
      });

    } else {
      const dialogRef = this.dialog.open(DialogComponent, {
        data: {
          message: 'Update the host?',
          buttonText: {
            ok: 'Update',
            cancel: 'Cancel'
          }
        }
      });

      dialogRef.afterClosed().subscribe((confirmed: boolean) => {
        if (confirmed) {
          this.host = this.hostForm.value;
          this.hostService.update(this.host).subscribe(data => this.router.navigateByUrl("/hosts"));
        }
      });
    } 
  } 

}
