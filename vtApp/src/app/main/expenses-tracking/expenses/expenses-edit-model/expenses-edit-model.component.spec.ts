import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpensesEditModelComponent } from './expenses-edit-model.component';

describe('ExpensesEditModelComponent', () => {
  let component: ExpensesEditModelComponent;
  let fixture: ComponentFixture<ExpensesEditModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpensesEditModelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpensesEditModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
