import { TestBed } from '@angular/core/testing';

import { DailyBeatService } from './daily-beat.service';

describe('DailyBeatService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DailyBeatService = TestBed.get(DailyBeatService);
    expect(service).toBeTruthy();
  });
});
