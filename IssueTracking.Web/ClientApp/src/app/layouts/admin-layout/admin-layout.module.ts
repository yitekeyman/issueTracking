import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AdminLayoutRoutes} from './admin-layout.routing';
import {DashboardComponent} from '../../dashboard/dashboard.component';
import {SettingsComponent} from "../../settings/settings.component";
import {SettingsModule} from "../../settings/settings.module";
import {IssuesRoutingModule} from "../../issues/issues.routing";


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    SettingsModule,
    IssuesRoutingModule

  ],
  declarations: [
    DashboardComponent,
    SettingsComponent,


  ]
})

export class AdminLayoutModule {
}
