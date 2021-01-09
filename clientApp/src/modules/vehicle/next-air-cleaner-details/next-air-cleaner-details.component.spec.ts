import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextAirCleanerDetailsComponent } from './next-air-cleaner-details.component';

describe('NextAirCleanerDetailsComponent', () => {
  let component: NextAirCleanerDetailsComponent;
  let fixture: ComponentFixture<NextAirCleanerDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextAirCleanerDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextAirCleanerDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
