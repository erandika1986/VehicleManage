import { TestBed } from '@angular/core/testing';

import { VehicleEmissionTestService } from './vehicle-emission-test.service';

describe('VehicleEmissionTestService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleEmissionTestService = TestBed.get(VehicleEmissionTestService);
    expect(service).toBeTruthy();
  });
});
