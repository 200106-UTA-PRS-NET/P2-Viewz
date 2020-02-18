import { Component, OnInit, Input } from '@angular/core';
import { WikiConnectorService } from '../wiki-connector.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PageHistoryService } from '../page-history.service';
import { Subscription, Observable } from 'rxjs';
import { PageAndContents } from '../pageAndContents';
import { merge } from 'rxjs';
import { Location } from '@angular/common';
import { DetailEntry } from '../detail-entry';

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
    private router: Router,
    private location: Location
  ){}

  ngOnInit() {
    this.sub = merge(this.route.params, this.route.queryParams).subscribe(() => {
      this.wikiUrl = this.route.snapshot.params['wiki'];
      this.pageUrl = this.route.snapshot.params['page'];
      this.page.pageHeader.pageUrl = this.pageUrl;
      this.editMode = this.route.snapshot.queryParams['edit'];
      this.newPage = false;
      this.getPage(this.wikiUrl, this.pageUrl);
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
        this.page.details = page['details'];
        if (!this.editMode) {
        this.pageContent = document.getElementById('pageContent');
        this.pageContent.innerHTML = this.page.content;
          this.history.add({
            pageName: page['pageName'],
            pageUrl: `${wikiUrl}/${pageUrl}`
          });
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

  AddDetail(){
    if(this.page.details==null||this.page.details.length==0||this.page.details[this.page.details.length-1].DetKey!=''){
    this.page.details.push({
      DetKey: '',
      DetVal: ''
    });
  }
  }

  RemoveDetail(detail: DetailEntry){
    this.page.details = this.page.details.filter(d => d !== detail);
  }

  Save(){
    this.wikiService.savePage(this.wikiUrl, this.page, this.newPage).subscribe(()=>{
      if(!this.newPage){
      this.router.navigateByUrl(`${this.wikiUrl}/${this.pageUrl}`);
    } else {
      this.newPage = false;
      this.editMode = false;
      this.getPage(this.wikiUrl, this.pageUrl);
    }
    });
  }
  Cancel(){
    if(!this.newPage){
      this.router.navigateByUrl(`${this.wikiUrl}/${this.pageUrl}`);
  } else {
    this.location.back();
  }
  }
  Edit(){
    this.router.navigate([`${this.wikiUrl}/${this.pageUrl}`], {queryParams : {'edit': 'true'}})
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
