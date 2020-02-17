import { Component, OnInit } from '@angular/core';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import { ContentEntry } from '../content-entry';
import { WikiConnectorService } from '../wiki-connector.service';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.css']
})
export class PageComponent implements OnInit {
  title = '';
  content : SafeHtml;
  contents: ContentEntry[];
  constructor(
    private wikiService: WikiConnectorService,
    private sanitized: DomSanitizer
  ){}

  ngOnInit() {
    this.getPage('readme');
  }

  getPage(pageUrl: string): void {
    this.wikiService.getPage(pageUrl)
      .subscribe(page => {
        this.title = page['pageName'];
        this.content = this.sanitized.bypassSecurityTrustHtml(page['content']);
        this.contents = page['contents'];
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
