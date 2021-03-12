import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductSubCategoryListComponent } from './product-sub-category-list.component';

describe('ProductSubCategoryListComponent', () => {
  let component: ProductSubCategoryListComponent;
  let fixture: ComponentFixture<ProductSubCategoryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductSubCategoryListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductSubCategoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
