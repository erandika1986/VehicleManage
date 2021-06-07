import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GearBoxDetailComponent } from './gear-box-detail.component';

describe('GearBoxDetailComponent', () => {
  let component: GearBoxDetailComponent;
  let fixture: ComponentFixture<GearBoxDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GearBoxDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GearBoxDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
