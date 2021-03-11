import { TestBed } from '@angular/core/testing';

import { VehicleRevenueLicenceService } from './vehicle-revenue-licence.service';

describe('VehicleRevenueLicenceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleRevenueLicenceService = TestBed.get(VehicleRevenueLicenceService);
    expect(service).toBeTruthy();
  });
});
