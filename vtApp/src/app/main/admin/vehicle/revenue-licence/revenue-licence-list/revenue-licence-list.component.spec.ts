import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueLicenceListComponent } from './revenue-licence-list.component';

describe('RevenueLicenceListComponent', () => {
  let component: RevenueLicenceListComponent;
  let fixture: ComponentFixture<RevenueLicenceListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevenueLicenceListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RevenueLicenceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
