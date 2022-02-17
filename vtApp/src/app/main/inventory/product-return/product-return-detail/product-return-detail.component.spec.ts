import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductReturnDetailComponent } from './product-return-detail.component';

describe('ProductReturnDetailComponent', () => {
  let component: ProductReturnDetailComponent;
  let fixture: ComponentFixture<ProductReturnDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductReturnDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductReturnDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
