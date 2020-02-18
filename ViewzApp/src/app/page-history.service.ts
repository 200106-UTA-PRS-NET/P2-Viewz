import { Injectable } from '@angular/core';
import { PageHead } from './pageHead';
import { FavoritePageHead } from './favoritePageHead';

@Injectable({
  providedIn: 'root'
})
export class PageHistoryService {
  favoritePages: FavoritePageHead[] = [];
  historyPages: FavoritePageHead[] = [];
  constructor() {
    let favs = window.sessionStorage['wikiPageFavorites'];
    if(favs){
      this.favoritePages = JSON.parse(favs);
    }
    let hist = window.sessionStorage['wikiPageHistory'];
    if(hist){
      this.historyPages = JSON.parse(hist);
    }
  }

  add(page: PageHead) {
    if (this.favoritePages.filter(p => p.page.pageUrl == page.pageUrl).length==0) {
      this.historyPages = this.historyPages.filter(p => p.page.pageUrl != page.pageUrl);
      this.historyPages.unshift({
        page: page,
        favorite: false
      });
      if(this.historyPages.length>10){
        this.historyPages.pop();
      }
      window.sessionStorage['wikiPageHistory'] = JSON.stringify(this.historyPages);
    }
  }
  addFavorite(page: PageHead){
    this.favoritePages = this.favoritePages.filter(p => p.page.pageUrl != page.pageUrl);
    this.favoritePages.unshift({
      page: page,
      favorite: true
    });
    this.historyPages = this.historyPages.filter(p => p.page.pageUrl != page.pageUrl);
    window.sessionStorage['wikiPageFavorites'] = JSON.stringify(this.favoritePages);
  }
  removeFavorite(page: PageHead){
    this.favoritePages = this.favoritePages.filter(p => p.page.pageUrl != page.pageUrl);
    this.historyPages = this.historyPages.filter(p => p.page.pageUrl != page.pageUrl);
    this.historyPages.unshift({
      page: page,
      favorite: false
    });
    window.sessionStorage['wikiPageFavorites'] = JSON.stringify(this.favoritePages);
  }
}
