import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextGreeceNipleDetailsFormComponent } from './next-greece-niple-details-form.component';

describe('NextGreeceNipleDetailsFormComponent', () => {
  let component: NextGreeceNipleDetailsFormComponent;
  let fixture: ComponentFixture<NextGreeceNipleDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextGreeceNipleDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextGreeceNipleDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
