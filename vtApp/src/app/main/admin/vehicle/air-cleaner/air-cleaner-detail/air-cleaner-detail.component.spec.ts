import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AirCleanerDetailComponent } from './air-cleaner-detail.component';

describe('AirCleanerDetailComponent', () => {
  let component: AirCleanerDetailComponent;
  let fixture: ComponentFixture<AirCleanerDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AirCleanerDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AirCleanerDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
