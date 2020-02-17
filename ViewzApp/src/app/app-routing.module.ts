import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageComponent } from './page/page.component';
import { WikiComponent } from './wiki/wiki.component';

const routes: Routes = [
  { path: '', component: WikiComponent, pathMatch: 'full' },
  { path: ':page', component: PageComponent}
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
