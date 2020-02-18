import { Component, OnInit, Input } from '@angular/core';
import { ContentEntry } from '../content-entry';
import { WikiConnectorService } from '../wiki-connector.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PageHistoryService } from '../page-history.service';
import { Subscription, Observable } from 'rxjs';
import { PageAndContents } from '../pageAndContents';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.css']
})
export class PageComponent implements OnInit {
  @Input() page: PageAndContents = {
    pageHeader: {
      pageName : null,
      pageUrl : null
    },
    content : '',
    contents: [],
    details: []
  };
  // title = '';
  // content : string;
  // contents: ContentEntry[];
  sub : Subscription;
  pageContent : HTMLElement;
  wikiUrl : string;
  pageUrl : string;
  editMode : boolean = false;
  newPage : boolean = false;
  constructor(
    private route: ActivatedRoute,
    private wikiService: WikiConnectorService,
    private history: PageHistoryService,
    private router: Router
  ){}

  ngOnInit() {
    this.pageContent = document.getElementById('pageContent');
    this.sub = this.route.params.subscribe((params: Params) => {
      this.wikiUrl = params['wiki'];
      this.pageUrl = params['page'];
      this.page.pageHeader.pageUrl = params['page'];
      this.editMode = this.route.snapshot.queryParams['edit'];
      this.newPage = false;
      this.getPage(params['wiki'], params['page']);
    });
  }

  getPage(wikiUrl: string, pageUrl: string): void {
    let sub: Observable<Object>;
    if(this.editMode){
      sub = this.wikiService.getPageMD(`${wikiUrl}/${pageUrl}`);
    } else {
      sub = this.wikiService.getPage(`${wikiUrl}/${pageUrl}`);
    }
      sub.subscribe(page => {
        this.page.pageHeader.pageName = page['pageName'];
        this.page.content = page['content'];
        this.page.contents = page['contents'];
        this.pageContent.innerHTML = this.page.content;
        if (!this.editMode) {
          this.history.add({
            pageName: page['pageName'],
            pageUrl: `${wikiUrl}/${pageUrl}`
          });
        }

        let anchors: any = this.pageContent.getElementsByTagName("a");
        for (let anchor of anchors) {
          if(document['baseURI'].startsWith(anchor['origin'])){
            anchor.onclick = () => {
              this.router.navigateByUrl(`${wikiUrl}${anchor['pathname']}`);
              return false;
            };
          } else {
            anchor.target = "_blank";
          }
        }
      }, () => {
        this.editMode = true;
        this.newPage = true;
        this.page.pageHeader.pageName = pageUrl;
        this.page.content = '';
        this.page.contents = [];
        this.page.details = [];
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
