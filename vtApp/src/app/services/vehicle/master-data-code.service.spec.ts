import { TestBed } from '@angular/core/testing';

import { MasterDataCodeService } from './master-data-code.service';

describe('MasterDataCodeService', () => {
  let service: MasterDataCodeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MasterDataCodeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
