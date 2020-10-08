import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VM } from '../../models/generated/v-m';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class VMService {

  private url = "/api/vm";
 
  constructor(private http: HttpClient) {
  }

  getAll() {
      return this.http.get(this.url);
  }
    
  getById(id: number) {
      return this.http.get(this.url + '/' + id);
  }
    
  create(vm: VM) {
      return this.http.post(this.url, vm, { observe: 'response' });
  }
  update(vm: VM) {

      return this.http.put(this.url, vm);
  }
  delete(id: number) {
      return this.http.delete(this.url + '/' + id);
  }

}
