import {RouterModule, Routes} from "@angular/router";
import {ViewIssueComponent} from "./viewIssues/view-issue.component";
import { EditIssueComponent } from './editIssue/edit-issue.component';
import {IssuesComponent} from "./issues.component";
import {NgModule} from "@angular/core";
import {DashboardComponent} from "../dashboard/dashboard.component";


export const routes:Routes=[
  {
    //path: 'issues?state=:state&q=:query&branch=:branch&type=:type&sort=:sort&labels=:labels&milestones=:milestones&assignee=:assignee',
    path:'issues',
    component: IssuesComponent,
    children: [
      {
        path: 'edit-issue',
        component: EditIssueComponent
      }
    ]
  },
  {path: 'issues/edit-issue', component:EditIssueComponent},
  {path: 'issues/view-issue/:issueId', component:ViewIssueComponent},
  ];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class IssuesRoutingModule {

}
