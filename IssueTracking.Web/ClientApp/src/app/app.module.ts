import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';

import {IssueTrackingService} from "./_Services/IssueTrackingService";
import {ApiServices} from "./_Services/api.service";
import {PagerService} from "./_Services/pager.service";
import {NgxDropzoneModule} from "ngx-dropzone";

@NgModule({
  imports: [
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    NgxDropzoneModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
  ],
  providers: [
    ApiServices,
    IssueTrackingService,
    PagerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
