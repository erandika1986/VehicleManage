import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueLicenceDetailComponent } from './revenue-licence-detail.component';

describe('RevenueLicenceDetailComponent', () => {
  let component: RevenueLicenceDetailComponent;
  let fixture: ComponentFixture<RevenueLicenceDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevenueLicenceDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RevenueLicenceDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
