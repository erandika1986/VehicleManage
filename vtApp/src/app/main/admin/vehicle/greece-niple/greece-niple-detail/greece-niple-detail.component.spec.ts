import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GreeceNipleDetailComponent } from './greece-niple-detail.component';

describe('GreeceNipleDetailComponent', () => {
  let component: GreeceNipleDetailComponent;
  let fixture: ComponentFixture<GreeceNipleDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GreeceNipleDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GreeceNipleDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
