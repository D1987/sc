import { Component, OnInit } from '@angular/core';
import { AppService } from '../../services/app.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Validators, FormBuilder} from '@angular/forms';
import { App } from 'src/app/models/generated/app';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from 'src/app/dialogs/dialog/dialog.component';
import { Pattern } from 'src/app/helpers/validators/patterns';

@Component({
  selector: 'app-app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  id: number;
  app: App = new App();
  appForm = this.formBuilder.group({
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
    enabled: ['true', [Validators.required]],
    critical: ['true', [Validators.required]],
    host: this.formBuilder.group({
      id: ['', [Validators.required]]
    }),
    vm: this.formBuilder.group({
      id: ['', [Validators.required]]
    })
  });
  added: boolean = true;
  loaded: boolean = false;

  constructor(private appService: AppService,
              private router: Router,
              activeRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private dialog: MatDialog) {
      this.id = Number.parseInt(activeRoute.snapshot.params["id"]);      
  }

  ngOnInit() {
    if (this.id) {
      this.appService.getById(this.id)
        .subscribe((data: App) => {
            this.appForm.get('id').setValue(data.id);
            this.appForm.get('name').setValue(data.name);
            this.appForm.get('ip').setValue(data.ip);
            this.appForm.get('domain').setValue(data.domain);
            this.appForm.get('login').setValue(data.login);
            this.appForm.get('password').setValue(data.password);
            this.appForm.get('description').setValue(data.description);
            this.appForm.get('project').setValue(data.project);
            this.appForm.get('type').setValue(data.type);
            this.appForm.get('critical').setValue(data.critical);
            this.appForm.get('enabled').setValue(data.enabled);
            this.appForm.get('host.id').setValue(data.host?.id);
            this.appForm.get('vm.id').setValue(data.vm?.id);

            if (this.appForm != null) this.added = false; this.loaded = true;
        });
      } else {
        // tslint:disable-next-line:align
        this.appForm.get('critical').setValue(true);
        this.appForm.get('enabled').setValue(true);
      }
  }

  openDialog() {
    if (this.id === 0) {

      const dialogRef = this.dialog.open(DialogComponent,{
        data: {
          message: 'Add new application?',
          buttonText: {
            ok: 'Add',
            cancel: 'Cancel'
          }
        }
      });

      dialogRef.afterClosed().subscribe((confirmed: boolean) => {
        if (confirmed) {
          this.app = this.appForm.value;
          this.appService.create(this.app).subscribe(data => this.router.navigateByUrl("/apps"));
        }
      });

    } else {
      const dialogRef = this.dialog.open(DialogComponent,{
        data: {
          message: 'Update the application?',
          buttonText: {
            ok: 'Update',
            cancel: 'Cancel'
          }
        }
      });

      dialogRef.afterClosed().subscribe((confirmed: boolean) => {
        if (confirmed) {
          this.app = this.appForm.value;
          this.appService.update(this.app).subscribe(data => this.router.navigateByUrl("/apps"));
        }
      });
    } 
  }
}
