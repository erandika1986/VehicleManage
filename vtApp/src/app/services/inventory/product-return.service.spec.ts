import { TestBed } from '@angular/core/testing';

import { ProductReturnService } from './product-return.service';

describe('ProductReturnService', () => {
  let service: ProductReturnService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductReturnService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
