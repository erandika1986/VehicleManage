import { TestBed } from '@angular/core/testing';

import { VehicleFuelFilterChangeMilageService } from './vehicle-fuel-filter-change-milage.service';

describe('VehicleFuelFilterChangeMilageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleFuelFilterChangeMilageService = TestBed.get(VehicleFuelFilterChangeMilageService);
    expect(service).toBeTruthy();
  });
});
