import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeletedBarComponent } from './seleted-bar.component';

describe('SeletedBarComponent', () => {
  let component: SeletedBarComponent;
  let fixture: ComponentFixture<SeletedBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeletedBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeletedBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
