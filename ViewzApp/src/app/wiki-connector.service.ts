import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment'
import { WikiObj } from './wiki-obj';
import { PageAndContents } from './pageAndContents';
import { stringify } from 'querystring';

@Injectable({
  providedIn: 'root'
})
export class WikiConnectorService {

  constructor(
    private http: HttpClient
  ) { }
  
  pageUrl = environment.apiUrl;

  getWikis(){
    return this.http.get(`${this.pageUrl}?count=5`);
  }

  getWiki(wikiURL: string){
    return this.http.get(`${this.pageUrl}/${wikiURL}`);
  }

  getWikiMD(wikiURL: string){
    return this.http.get(`${this.pageUrl}/${wikiURL}?html=false`)
  }

  saveWiki(wiki: WikiObj, newWiki: boolean){
    if(newWiki){
      return this.http.post(`${this.pageUrl}/${wiki.url}`, wiki);
    } else {
      return this.http.patch(`${this.pageUrl}/${wiki.url}`, wiki);
    }
  }

  savePage(wikiUrl: string, page: PageAndContents, newPage: boolean){
    let storePage = {
      url: page.pageHeader.pageUrl,
      pageName: page.pageHeader.pageName,
      content: page.content,
      contents: null,
      details: page.details
    }
    if(newPage){
      return this.http.post(`${this.pageUrl}/${wikiUrl}/${page.pageHeader.pageUrl}`, storePage);
    } else {
      return this.http.patch(`${this.pageUrl}/${wikiUrl}/${page.pageHeader.pageUrl}`, storePage);
    }
  }

  getPage(wikiPageURL: string){
    return this.http.get(`${this.pageUrl}/${wikiPageURL}?html=Html`);
  }
  
  getPageMD(wikiPageURL: string){
    return this.http.get(`${this.pageUrl}/${wikiPageURL}?html=Md`);
  }
}
