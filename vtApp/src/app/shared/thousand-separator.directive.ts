import {
    Directive,
    ElementRef,
    Renderer2,
    HostListener,
    HostBinding,
    Input,
    Optional
  } from "@angular/core";
  import { NgControl } from "@angular/forms";
  import { DecimalPipe } from "@angular/common";
  
  @Directive({
    selector: "[appThousandSeparator]"
  })
  export class ThousandSeparatorDirective {
    textbox: HTMLInputElement;
  
    constructor(
      private el: ElementRef,
      private renderer: Renderer2,
      @Optional() private ngControl: NgControl,
      private decimalPipe: DecimalPipe
    ) {}
  
    ngAfterViewInit() {
      this.textbox =
        this.el.nativeElement.tagName === "INPUT"
          ? this.el.nativeElement
          : this.el.nativeElement.querySelector("input");
    }
  
    @HostListener("blur", ["$event"]) onBlur(event) {
      const formattedVal = this.decimalPipe.transform(
        this.textbox.value,
        "1.2-2"
      );

      console.log('heresssss');
      console.log(formattedVal);
      
      
      if (this.ngControl) {
        this.ngControl.control.setValue(formattedVal, { emitEvent: false });
      } else {
        this.renderer.setProperty(this.textbox, "value", formattedVal);
      }
    }
  
    @HostListener("focus", ["$event"]) onFocus(event) {

      console.log('heresssss');
      const initVal = this.textbox.value.replace(/,/g, '');
      if (this.ngControl) {
        this.ngControl.control.setValue(initVal, { emitEvent: false });
      } else {
        this.renderer.setProperty(this.textbox, "value", initVal);
      }
    }
  }
  