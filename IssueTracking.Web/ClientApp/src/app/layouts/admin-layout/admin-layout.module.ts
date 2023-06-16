import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AdminLayoutRoutes} from './admin-layout.routing';
import {DashboardComponent} from '../../dashboard/dashboard.component';
import {IssuesComponent} from "../../issues/issues.component";
import {IssueRaisedSystemComponent} from "../../settings/issueRaisedSystem/issue-raised-system.component";
import {IssueTypeListComponent} from "../../settings/issueType/issue-type-list.component";
import {LabelsComponent} from "../../settings/labels/labels.component";
import {BasicSolutionComponent} from "../../settings/basicSolution/basic-solution.component";
import {SettingsComponent} from "../../settings/settings.component";

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule
  ],
  declarations: [
    DashboardComponent,
    IssuesComponent,
    SettingsComponent
  ]
})

export class AdminLayoutModule {
}
