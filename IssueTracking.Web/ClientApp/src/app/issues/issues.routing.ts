import {Routes} from "@angular/router";
import {IssuesListComponent} from "./issues-list/issues-list.component";
import {EditIssueComponent} from "./editIssue/edit-issue.component";
import {IssuesComponent} from "./issues.component";


export const IssuesRouting:Routes=[
  {path: 'issues-list', component: IssuesListComponent},
  {path: 'issue', component: IssuesComponent},
  {path: 'edit-issue', component: EditIssueComponent}
  ]

