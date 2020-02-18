import { TestBed } from '@angular/core/testing';

import { PageHistoryService } from './page-history.service';

describe('PageHistoryService', () => {
  let service: PageHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PageHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
