import { Component, Pipe, PipeTransform } from '@angular/core';
import { WikiConnectorService } from './wiki-connector.service'
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { ContentEntry } from './content-entry';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = '';
  content : SafeHtml;
  popularPages : page[];
  constructor(
    private wikiService: WikiConnectorService,
    private sanitized: DomSanitizer
  ){}

  ngOnInit() {
    this.getWiki();
  }

  getWiki(){
    this.wikiService.getWiki()
      .subscribe(wiki => {
        this.title = wiki['pageName'];
        this.content = this.sanitized.bypassSecurityTrustHtml(wiki['description']);
        this.popularPages = Array.from(wiki['popularPages'], wikiPages => <page> {
          pageUrl: wikiPages['url'],
          pageName: wikiPages['pageName'],
          content: null
        })
      })
  }

  getPage(pageUrl: string): void {
    this.wikiService.getPage(pageUrl)
      .subscribe(page => {
        this.title = page['pageName'];
        this.content = this.sanitized.bypassSecurityTrustHtml(page['content']);
    });
  }

  scrollToElement(id: string): boolean {
    debugger;
    try{
    document.getElementById(id).scrollIntoView({"behavior": "smooth"});
  } catch {
    document.getElementById(id).scrollIntoView();
  }
    return false;
  }
}

interface page {
  pageUrl: string,
  pageName: string,
  content: SafeHtml | null
}