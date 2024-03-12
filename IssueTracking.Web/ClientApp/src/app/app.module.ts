import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {RouterModule, RouterOutlet} from '@angular/router';
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
import {IssuesRoutingModule, routes} from "./issues/issues.routing";
import {LoginModule} from "./login/login.module";
import {TimeDifferencePipe} from "./time-difference.pipe";
import {DialogModule} from "./_shared/dialog/dialog.module";
import {DialogService} from "./_shared/dialog/dialog.service";
import {NotificationService} from "./_Services/notification.service";
import {NotificationsComponent} from "./notifications/notifications.component";

@NgModule({
  imports: [
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule.forChild(routes),
    AppRoutingModule,
    NgxDropzoneModule,
    DialogModule,

  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    TimeDifferencePipe,
    NotificationsComponent

  ],
  providers: [
    ApiServices,
    IssueTrackingService,
    PagerService,
    DialogService,
    NotificationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
