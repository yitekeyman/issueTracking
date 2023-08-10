import {Routes} from "@angular/router";
import {ViewIssueComponent} from "./viewIssues/view-issue.component";
import {EditIssueComponent} from "./editIssue/edit-issue.component";
import {IssuesComponent} from "./issues.component";


export const IssuesRouting:Routes=[
  {path: 'issues-list', component: ViewIssueComponent},
  {path: 'issue', component: IssuesComponent},
  {path: 'edit-issue', component: EditIssueComponent}
  ]

