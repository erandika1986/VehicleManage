import { TestBed } from '@angular/core/testing';

import { RouteMessageService } from './route-message.service';

describe('RouteMessageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RouteMessageService = TestBed.get(RouteMessageService);
    expect(service).toBeTruthy();
  });
});
