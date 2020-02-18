import { TestBed } from '@angular/core/testing';

import { WikiConnectorService } from './wiki-connector.service';

describe('WikiConnectorService', () => {
  let service: WikiConnectorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WikiConnectorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
