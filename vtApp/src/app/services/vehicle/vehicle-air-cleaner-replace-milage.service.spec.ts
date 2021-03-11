import { TestBed } from '@angular/core/testing';

import { VehicleAirCleanerReplaceMilageService } from './vehicle-air-cleaner-replace-milage.service';

describe('VehicleAirCleanerReplaceMilageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleAirCleanerReplaceMilageService = TestBed.get(VehicleAirCleanerReplaceMilageService);
    expect(service).toBeTruthy();
  });
});
