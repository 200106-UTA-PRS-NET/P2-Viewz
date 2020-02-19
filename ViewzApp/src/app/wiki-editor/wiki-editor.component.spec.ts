import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WikiEditorComponent } from './wiki-editor.component';

describe('WikiEditorComponent', () => {
  let component: WikiEditorComponent;
  let fixture: ComponentFixture<WikiEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WikiEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WikiEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
