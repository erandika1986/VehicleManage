import { TestBed } from '@angular/core/testing';

import { VehicleFitnessReportService } from './vehicle-fitness-report.service';

describe('VehicleFitnessReportService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleFitnessReportService = TestBed.get(VehicleFitnessReportService);
    expect(service).toBeTruthy();
  });
});
