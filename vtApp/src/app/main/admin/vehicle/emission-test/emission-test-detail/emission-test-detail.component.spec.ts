import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmissionTestDetailComponent } from './emission-test-detail.component';

describe('EmissionTestDetailComponent', () => {
  let component: EmissionTestDetailComponent;
  let fixture: ComponentFixture<EmissionTestDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmissionTestDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmissionTestDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
