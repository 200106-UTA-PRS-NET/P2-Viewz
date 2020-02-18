import { Component, OnInit } from '@angular/core';
import { ContentEntry } from '../content-entry';
import { WikiConnectorService } from '../wiki-connector.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PageHistoryService } from '../page-history.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.css']
})
export class PageComponent implements OnInit {
  title = '';
  content : string;
  contents: ContentEntry[];
  sub : Subscription;
  pageContent : HTMLElement;
  constructor(
    private route: ActivatedRoute,
    private wikiService: WikiConnectorService,
    private history: PageHistoryService,
    private router: Router
  ){}

  ngOnInit() {
    this.pageContent = document.getElementById('pageContent');
    this.sub = this.route.params.subscribe((params: Params) => {
      this.getPage(params['page']);
      this.history.add(params['page']);
    });
  }

  getPage(pageUrl: string): void {

    this.wikiService.getPage(pageUrl)
      .subscribe(page => {
        this.title = page['pageName'];
        this.content = page['content']//this.sanitized.bypassSecurityTrustHtml(page['content']);
        this.contents = page['contents'];
        this.pageContent.innerHTML = this.content;
        let anchors: any = this.pageContent.getElementsByTagName("a");
        for (let anchor of anchors) {
          anchor.onclick = e => {
            try{
              this.router.navigateByUrl(anchor['pathname']);
              return false;
            }catch{
              return true;
            }
            //this.router.navigateByUrl(e.)
          }
        }
      });
  }

  scrollToElement(id: string): boolean {
    try{
    document.getElementById(id).scrollIntoView({"behavior": "smooth"});
  } catch {
    document.getElementById(id).scrollIntoView();
  }
    return false;
  }
}
