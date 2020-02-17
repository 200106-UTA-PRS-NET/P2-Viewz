import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class WikiConnectorService {

  constructor(
    private http: HttpClient
  ) { }
  
  pageUrl = `${environment.apiUrl}/training-code`;

  getWiki(){
    return this.http.get(this.pageUrl);
  }

  getPage(pageName: string){
    return this.http.get(`${this.pageUrl}/${pageName}`);
  }
}
