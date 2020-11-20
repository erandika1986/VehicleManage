import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LanguageTranslationModule } from './shared/modules/language-translation/language-translation.module'
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthGuard } from './shared';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { NgxSpinnerModule } from "ngx-spinner";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VahicleRegNoValidator } from './validators/vehicle-reg-no.validator';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    imports: [
        CommonModule,
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        LanguageTranslationModule,
        AppRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgxSpinnerModule,
        ToastrModule.forRoot(),
        NgbModule
    ],
    declarations: [AppComponent],
    providers:
        [

            VahicleRegNoValidator,
            AuthGuard,
            {
                provide: HTTP_INTERCEPTORS,
                useClass: AuthInterceptorService,
                multi: true
            }
        ],
    entryComponents: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
