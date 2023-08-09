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
import {SettingsModule} from "../../settings/settings.module";
import {IssuesListComponent} from "../../issues/issues-list/issues-list.component";
import {IssuesModule} from "../../issues/issues.module";
import {IssuesRouting} from "../../issues/issues.routing";

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(AdminLayoutRoutes),
        FormsModule,
        SettingsModule,

    ],
  declarations: [
    DashboardComponent,
    SettingsComponent,

  ]
})

export class AdminLayoutModule {
}
