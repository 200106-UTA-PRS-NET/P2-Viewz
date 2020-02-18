import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule }    from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { WikiComponent } from './wiki/wiki.component';
import { PageComponent } from './page/page.component';
import { PageEditorComponent } from './page-editor/page-editor.component';
import { WikiEditorComponent } from './wiki-editor/wiki-editor.component';
import { HomeComponent } from './home/home.component';
import { PageListComponent } from './page-list/page-list.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    WikiComponent,
    PageComponent,
    PageEditorComponent,
    WikiEditorComponent,
    HomeComponent,
    PageListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
