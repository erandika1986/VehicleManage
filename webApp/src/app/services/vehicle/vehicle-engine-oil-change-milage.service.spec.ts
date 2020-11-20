import { TestBed } from '@angular/core/testing';

import { VehicleEngineOilChangeMilageService } from './vehicle-engine-oil-change-milage.service';

describe('VehicleEngineOilChangeMilageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleEngineOilChangeMilageService = TestBed.get(VehicleEngineOilChangeMilageService);
    expect(service).toBeTruthy();
  });
});
