import { TestBed } from '@angular/core/testing';

import { VehicleDifferentialOilChangeMilageService } from './vehicle-differential-oil-change-milage.service';

describe('VehicleDifferentialOilChangeMilageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleDifferentialOilChangeMilageService = TestBed.get(VehicleDifferentialOilChangeMilageService);
    expect(service).toBeTruthy();
  });
});
