import { Component, OnInit } from '@angular/core';
import { PageHead } from '../pageHead';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { WikiConnectorService } from '../wiki-connector.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-wiki',
  templateUrl: './wiki.component.html',
  styleUrls: ['./wiki.component.css']
})
export class WikiComponent implements OnInit {
  title = '';
  wikiDescription: HTMLElement;
  popularPages : PageHead[];
  sub : Subscription;
  content : string;
  constructor(
    private route: ActivatedRoute,
    private wikiService: WikiConnectorService,
    private sanitized: DomSanitizer,
    private router: Router
  ){
  }

  ngOnInit() {
    this.wikiDescription = document.getElementById('wikiDescription');
    this.sub = this.route.params.subscribe((params: Params) => {
      this.getWiki(params['wiki']);
    });
  }

  getWiki(wiki: string) {
    this.wikiService.getWiki(wiki)
      .subscribe(wiki => {
        this.title = wiki['pageName'];
        this.content = wiki['description'];
        this.wikiDescription.innerHTML = this.content;
        let anchors: any = this.wikiDescription.getElementsByTagName("a");
        for (let anchor of anchors) {
          if (document['baseURI'].startsWith(anchor['origin'])) {
            anchor.onclick = () => {
              this.router.navigateByUrl(`${wiki}${anchor['pathname']}`);
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
      });
  }

}
