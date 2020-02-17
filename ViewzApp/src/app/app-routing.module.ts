import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { WikiComponent } from './wiki/wiki.component';

const routes: Routes = [
  { path: '', component: AppComponent },
  { path: ':wiki', component: WikiComponent}
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
  constructor() {
    
  }
}
