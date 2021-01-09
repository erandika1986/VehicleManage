import { TestBed } from '@angular/core/testing';

import { VehicleMessageService } from './vehicle-message.service';

describe('VehicleMessageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleMessageService = TestBed.get(VehicleMessageService);
    expect(service).toBeTruthy();
  });
});
