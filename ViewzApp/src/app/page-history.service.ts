import { Injectable } from '@angular/core';
import { PageHead } from './pageHead';
import { FavoritePageHead } from './favoritePageHead';

@Injectable({
  providedIn: 'root'
})
export class PageHistoryService {
  favoritePages: FavoritePageHead[] = [];
  historyPages: FavoritePageHead[] = [];
  constructor() { }

  add(page: PageHead) {
    if (!this.favoritePages.includes({
      page: page,
      favorite: true
    })) {
      let unfavoritedPage = {
        page: page,
        favorite: false
      }
      let idx;
      while ((idx = this.historyPages.indexOf(unfavoritedPage)) > -1) {
        this.historyPages.splice(idx, 1);
      }
      this.historyPages.unshift(unfavoritedPage);
      window.sessionStorage['wikiPageHistory'] = JSON.stringify(this.historyPages);
    }
  }
  addFavorite(page: PageHead){
    let idx;
    let favoritedPage = {
      page: page,
      favorite: true
    }
    while ((idx = this.favoritePages.indexOf(favoritedPage)) > -1) {
      this.favoritePages.splice(idx, 1);
    }
    this.favoritePages.unshift(favoritedPage);
    while ((idx = this.historyPages.indexOf(favoritedPage)) > -1) {
      this.historyPages.splice(idx, 1);
    }
  }
}
