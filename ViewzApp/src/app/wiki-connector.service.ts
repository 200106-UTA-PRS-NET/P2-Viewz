import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class WikiConnectorService {

  constructor(
    private http: HttpClient
  ) { }
  
  pageUrl = environment.apiUrl;

  getWikis(){
    return this.http.get(`${this.pageUrl}`);
  }

  getWiki(wikiURL: string){
    return this.http.get(`${this.pageUrl}/${wikiURL}`);
  }

  getPage(wikiPageURL: string){
    return this.http.get(`${this.pageUrl}/${wikiPageURL}?html=Html`);
  }
}
