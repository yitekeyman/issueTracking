import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgxDropzoneModule} from "ngx-dropzone";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {IssuesRouting} from "./issues.routing";
import {ViewIssueComponent} from "./viewIssues/view-issue.component";
import {EditIssueComponent} from "./editIssue/edit-issue.component";

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(IssuesRouting),
    FormsModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    MonacoEditorModule,
  ],
  exports: [
    EditIssueComponent
  ],
  declarations: [
    ViewIssueComponent,
    EditIssueComponent
  ]
})
export class IssuesModule{

}
