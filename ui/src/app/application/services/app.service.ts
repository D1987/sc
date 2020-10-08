import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { App } from '../../models/generated/app';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  private url = "/api/app";
 
    constructor(private http: HttpClient) {
    }
 
    getAll() {
        return this.http.get(this.url);
    }
     
    getById(id: number) {
        return this.http.get(this.url + '/' + id);
    }
     
    create(app: App) {
        return this.http.post(this.url, app, { observe: 'response' });
    }

    update(app: App) {  
        return this.http.put(this.url, app);
    }
    
    delete(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}
