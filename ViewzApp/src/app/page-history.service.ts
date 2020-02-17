import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PageHistoryService {
  pages: string[] = [];
  constructor() { }

  add(page: string){
    let idx;
    while((idx = this.pages.indexOf(page))>-1){
      this.pages.splice(idx, 1);
    }
    this.pages.unshift(page);
    window.sessionStorage['wikiPageHistory'] = this.pages;
    let storedFavorites = window.localStorage['wikiPageFavorites'];
    if(!storedFavorites){
      window.localStorage['wikiPageFavorites'] = this.pages;
    } else {
      let sF;
      try{
        sF = JSON.parse(storedFavorites);
      } catch {
        sF = [storedFavorites];
      }
      if (sF.length <= this.pages.length) {
        window.localStorage['wikiPageFavorites'] = this.pages;
      }
    }
  }
}
