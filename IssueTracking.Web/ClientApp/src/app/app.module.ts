import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';
import {LoginComponent} from "./login/login.component";
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import {IssueTrackingService} from "./_Services/IssueTrackingService";
import {ApiServices} from "./_Services/api.service";
import {PagerService} from "./_Services/pager.service";
import {NgxDropzoneModule} from "ngx-dropzone";
import {IssuesComponent} from "./issues/issues.component";
import {IssuesModule} from "./issues/issues.module";

@NgModule({
  imports: [
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    NgxDropzoneModule,
    IssuesModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    LoginComponent,
    IssuesComponent

  ],
  providers: [
    ApiServices,
    IssueTrackingService,
    PagerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
