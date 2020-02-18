import { Component, OnInit } from '@angular/core';
import { PageHistoryService } from '../page-history.service';
import { FavoritePageHead } from '../favoritePageHead';

@Component({
  selector: 'app-page-list',
  templateUrl: './page-list.component.html',
  styleUrls: ['./page-list.component.css']
})
export class PageListComponent implements OnInit {
  constructor(
    public history: PageHistoryService
  ) { }

  ngOnInit(): void {

  }

  UpdateFavorite(page: FavoritePageHead){
    if(page.favorite){
      this.history.addFavorite(page.page);
    } else {
      this.history.removeFavorite(page.page);
    }
  }

}
