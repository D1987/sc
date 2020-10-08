import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Host } from '../../models/generated/host';
import { environment } from 'src/environments/environment.prod';
 
@Injectable()
export class HostService {
 
    private url = "/api/host";
 
    constructor(private http: HttpClient) {
    }
 
    getAll() {
        return this.http.get(this.url);
    }
     
    getById(id: number) {
        return this.http.get(this.url + '/' + id);
    }
     
    create(host: Host) {
        return this.http.post(this.url, host, { observe: 'response' });
    }
    update(host: Host) {
  
        return this.http.put(this.url, host);
    }
    delete(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}