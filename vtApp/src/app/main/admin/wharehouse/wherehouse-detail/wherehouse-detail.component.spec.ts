import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WherehouseDetailComponent } from './wherehouse-detail.component';

describe('WherehouseDetailComponent', () => {
  let component: WherehouseDetailComponent;
  let fixture: ComponentFixture<WherehouseDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WherehouseDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WherehouseDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
