import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgxDropzoneModule} from "ngx-dropzone";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {IssuesRoutingModule, routes} from "./issues.routing";
import {ViewIssueComponent} from "./viewIssues/view-issue.component";
import {EditIssueComponent} from "./editIssue/edit-issue.component";

@NgModule({
  declarations: [
    ViewIssueComponent,
    EditIssueComponent,
  ],

  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    MonacoEditorModule,
    IssuesRoutingModule
  ],

  exports: [
    EditIssueComponent

  ],
})
export class IssuesModule{

}
