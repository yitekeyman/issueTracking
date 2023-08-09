import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgxDropzoneModule} from "ngx-dropzone";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {IssuesRouting} from "./issues.routing";
import {IssuesListComponent} from "./issues-list/issues-list.component";
import {SettingsModule} from "../settings/settings.module";
import {IssuesComponent} from "./issues.component";
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
    IssuesListComponent,
    EditIssueComponent
  ]
})
export class IssuesModule{

}
