import {Routes} from '@angular/router';

import {DashboardComponent} from '../../dashboard/dashboard.component';
import {IssuesComponent} from "../../issues/issues.component";
import {BasicSolutionComponent} from "../../settings/basicSolution/basic-solution.component";
import {IssueRaisedSystemComponent} from "../../settings/issueRaisedSystem/issue-raised-system.component";
import {LabelsComponent} from "../../settings/labels/labels.component";
import {IssueTypeListComponent} from "../../settings/issueType/issue-type-list.component";
import {AdminLayoutComponent} from "./admin-layout.component";
import {AdminLayoutModule} from "./admin-layout.module";
import {SettingsComponent} from "../../settings/settings.component";
import {SettingsModule} from "../../settings/settings.module";

export const AdminLayoutRoutes: Routes = [
  {path: 'dashboard', component: DashboardComponent},
  {path: 'issues', component: IssuesComponent},
  {
    path: 'settings',
    redirectTo: 'settings/issue-raised-system',
    pathMatch: 'full'
  },
  {
    path: 'settings',
    component: SettingsComponent,
    children: [
      {
        path: '',
        loadChildren: () => SettingsModule
      }]},
];
