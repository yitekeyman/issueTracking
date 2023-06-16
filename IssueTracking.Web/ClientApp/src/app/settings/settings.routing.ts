import {Routes} from "@angular/router";
import {BasicSolutionComponent} from "./basicSolution/basic-solution.component";
import {IssueRaisedSystemComponent} from "./issueRaisedSystem/issue-raised-system.component";
import {LabelsComponent} from "./labels/labels.component";
import {IssueTypeListComponent} from "./issueType/issue-type-list.component";

export const SettingsRouting: Routes = [
  {path: 'basic-solution', component: BasicSolutionComponent},
  {path: 'issue-raised-system', component: IssueRaisedSystemComponent},
  {path: 'labels', component: LabelsComponent},
  {path: 'issue-types', component: IssueTypeListComponent}
]


