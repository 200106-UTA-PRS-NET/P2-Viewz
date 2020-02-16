import { Component } from '@angular/core';
import { WikiConnectorService } from './wiki-connector.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ViewzApp';
  content = '';
  constructor(
    private wikiService: WikiConnectorService
  ){}

  ngOnInit() {
    this.getPage();
  }

  getPage(): void {
    this.wikiService.getPage()
      .subscribe(page => this.content = page['content']);
  }
}
