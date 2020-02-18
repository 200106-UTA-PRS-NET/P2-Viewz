import { Component, OnInit } from '@angular/core';
import { WikiConnectorService } from '../wiki-connector.service';
import { PageHead } from '../pageHead';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  popularWikis: PageHead[] = [];
  constructor(
    private wikiService: WikiConnectorService
    ) { }

  ngOnInit(): void {
    this.wikiService.getWikis().subscribe((wikis:any) =>{
      this.popularWikis = wikis.map(wiki => <PageHead> {
        pageName: wiki.pageName,
        pageUrl: wiki.url
      })
    });
  }

}
