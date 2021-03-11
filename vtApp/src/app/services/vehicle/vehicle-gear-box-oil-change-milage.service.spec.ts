import { TestBed } from '@angular/core/testing';

import { VehicleGearBoxOilChangeMilageService } from './vehicle-gear-box-oil-change-milage.service';

describe('VehicleGearBoxOilChangeMilageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleGearBoxOilChangeMilageService = TestBed.get(VehicleGearBoxOilChangeMilageService);
    expect(service).toBeTruthy();
  });
});
