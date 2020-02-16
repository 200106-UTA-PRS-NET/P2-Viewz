import { Component, Pipe, PipeTransform } from '@angular/core';
import { WikiConnectorService } from './wiki-connector.service'
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = '';
  content;
  contents: ContentEntry[];
  constructor(
    private wikiService: WikiConnectorService,
    private sanitized: DomSanitizer
  ){}

  ngOnInit() {
    this.getPage();
  }

  getPage(): void {
    this.wikiService.getPage()
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

interface ContentEntry {
  content: string;
  id: string;
  level: number;
}