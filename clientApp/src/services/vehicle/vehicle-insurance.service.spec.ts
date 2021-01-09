import { TestBed } from '@angular/core/testing';

import { VehicleInsuranceService } from './vehicle-insurance.service';

describe('VehicleInsuranceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleInsuranceService = TestBed.get(VehicleInsuranceService);
    expect(service).toBeTruthy();
  });
});
