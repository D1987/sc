import { Component, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Location } from '../../../helpers/enums/location';
import { Pattern } from '../../../helpers/validators/patterns';

@Component({
  selector: 'app-host-form',
  templateUrl: './host-form.component.html',
  styleUrls: ['./host-form.component.scss']
})
export class HostFormComponent {

   @Input() hostForm = this.formBuilder.group({
    id: ['0', [Validators.required]],             
    name: ['', [Validators.required]],
    ip: ['', [
      Validators.pattern(Pattern.ipPattern())
    ]],
    os: [''],
    login: ['', [Validators.required]],
    password: ['', [Validators.required]],
    description: [''],
    location: ['', [Validators.required]]
  });
  hide: boolean = true;
  keys = Object.keys;
  locations = Location;

  constructor(private formBuilder: FormBuilder) {}

  hidePassword() {
    this.hide = !this.hide;
  }
}
