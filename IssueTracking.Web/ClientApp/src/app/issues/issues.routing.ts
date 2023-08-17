import {RouterModule, Routes} from "@angular/router";
import {ViewIssueComponent} from "./viewIssues/view-issue.component";
import { EditIssueComponent } from './editIssue/edit-issue.component';
import {IssuesComponent} from "./issues.component";
import {NgModule} from "@angular/core";


export const routes:Routes=[
  {
    path: 'issues',
    component: IssuesComponent,
    children: [
      {
        path: 'edit-issue',
        component: EditIssueComponent
      }
    ]
  },
  {path: 'issues/edit-issue', component:EditIssueComponent},
  {path: 'issues/view-issue', component:ViewIssueComponent},
  ];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class IssuesRoutingModule {

}
