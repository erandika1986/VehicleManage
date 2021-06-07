import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GreeceNipleListComponent } from './greece-niple-list.component';

describe('GreeceNipleListComponent', () => {
  let component: GreeceNipleListComponent;
  let fixture: ComponentFixture<GreeceNipleListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GreeceNipleListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GreeceNipleListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
