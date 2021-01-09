import { TestBed } from '@angular/core/testing';

import { VehicleAirCleanerService } from './vehicle-air-cleaner.service';

describe('VehicleAirCleanerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleAirCleanerService = TestBed.get(VehicleAirCleanerService);
    expect(service).toBeTruthy();
  });
});
