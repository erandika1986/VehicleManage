import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextAirCleanerDetailsFormComponent } from './next-air-cleaner-details-form.component';

describe('NextAirCleanerDetailsFormComponent', () => {
  let component: NextAirCleanerDetailsFormComponent;
  let fixture: ComponentFixture<NextAirCleanerDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextAirCleanerDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextAirCleanerDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
