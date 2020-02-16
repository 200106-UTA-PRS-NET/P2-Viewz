import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WikiConnectorService {

  constructor(
    private http: HttpClient
  ) { }
  
  pageUrl = 'https://viewzapicanary.azurewebsites.net/api/wiki/training-code/readme';

  getPage(){
    return this.http.get(this.pageUrl);
  }
}
