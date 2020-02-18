import { Component, OnInit, Input } from '@angular/core';
import { PageHead } from '../pageHead';
import { Subscription, Observable, merge } from 'rxjs';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { WikiConnectorService } from '../wiki-connector.service';
import { Location } from '@angular/common';
import { PageHistoryService } from '../page-history.service';

@Component({
  selector: 'app-wiki',
  templateUrl: './wiki.component.html',
  styleUrls: ['./wiki.component.css']
})
export class WikiComponent implements OnInit {
  @Input() title = '';
  wikiDescription: HTMLElement;
  popularPages : PageHead[];
  sub : Subscription;
  @Input() content : string;
  url: string;
  editMode: boolean = false;
  newWiki: boolean = false;
  constructor(
    private route: ActivatedRoute,
    private wikiService: WikiConnectorService,
    private history: PageHistoryService,
    private router: Router,
    private location: Location
  ){
  }

  ngOnInit() {
    this.history.setUpPage({
      pageName: 'Home',
      pageUrl: ''
    })
    this.sub = merge(this.route.params, this.route.queryParams).subscribe(() => {
      this.editMode = this.route.snapshot.queryParams['edit'];
      this.newWiki = false;
      this.url = this.route.snapshot.params['wiki']
      this.getWiki(this.url);
    });
  }

  getWiki(wikiUrl: string) {
    let sub : Observable<Object>;
    if(this.editMode){
      sub = this.wikiService.getWikiMD(wikiUrl)
    } else {
      sub = this.wikiService.getWiki(wikiUrl)
    }
    sub.subscribe(wiki => {
      this.title = wiki['pageName'];
      this.content = wiki['description'];
      if (!this.editMode) {
      this.wikiDescription = document.getElementById('wikiDescription');
      this.wikiDescription.innerHTML = this.content;
        let anchors: any = this.wikiDescription.getElementsByTagName("a");
        for (let anchor of anchors) {
          if (document['baseURI'].startsWith(anchor['origin'])) {
            anchor.onclick = () => {
              this.router.navigateByUrl(`${wikiUrl}${anchor['pathname']}`);
              return false;
            };
          } else {
            anchor.target = "_blank";
          }
        }
        this.popularPages = wiki['popularPages'].map(wikiPages => <PageHead>{
          pageUrl: wikiPages['url'],
          pageName: wikiPages['pageName'],
        });
      }
    },
      () => {
        this.editMode = true;
        this.newWiki = true;
        this.title = wikiUrl;
        this.url = wikiUrl;
        this.content = '';
      });
  }

  Save(){
    this.wikiService.saveWiki({
      url: this.url,
      pageName: this.title,
      description: this.content,
      popularPages: []
    }, this.newWiki).subscribe(()=>{
      if(!this.newWiki){
      this.router.navigateByUrl(this.url);
    } else {
      this.newWiki = false;
      this.editMode = false;
      this.getWiki(this.url);
    }
    });
  }
  Cancel(){
    if(!this.newWiki){
      this.router.navigateByUrl(this.url);
  } else {
    this.location.back();
  }
  }
  Edit(){
    this.router.navigate([this.url], {queryParams : {'edit': 'true'}})
  }
}
