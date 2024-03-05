import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgxDropzoneModule} from "ngx-dropzone";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {IssuesRoutingModule, routes} from "./issues.routing";
import {ViewIssueComponent} from "./viewIssues/view-issue.component";
import {EditIssueComponent} from "./editIssue/edit-issue.component";
import {IssuesComponent} from "./issues.component";
import {SettingsRouting} from "../settings/settings.routing";
import {HttpClientModule} from "@angular/common/http";


@NgModule({
  declarations: [
    ViewIssueComponent,
    EditIssueComponent,
    IssuesComponent,
      
  ],

  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    MonacoEditorModule,
    HttpClientModule,
    IssuesRoutingModule
  ],

  exports: [
    EditIssueComponent
  ],
})
export class IssuesModule{

}
