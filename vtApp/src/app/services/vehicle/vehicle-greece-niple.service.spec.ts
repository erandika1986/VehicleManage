import { TestBed } from '@angular/core/testing';

import { VehicleGreeceNipleService } from './vehicle-greece-niple.service';

describe('VehicleGreeceNipleService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleGreeceNipleService = TestBed.get(VehicleGreeceNipleService);
    expect(service).toBeTruthy();
  });
});
