import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {AdminLayoutRoutes} from "../layouts/admin-layout/admin-layout.routing";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BasicSolutionComponent} from "./basicSolution/basic-solution.component";
import {IssueRaisedSystemComponent} from "./issueRaisedSystem/issue-raised-system.component";
import {LabelsComponent} from "./labels/labels.component";
import {IssueTypeListComponent} from "./issueType/issue-type-list.component";
import {SettingsRouting} from "./settings.routing";
import {EditIssueTypeComponent} from "./issueType/editIssueType/edit-issue-type.component";
import {EditBasicSolutionComponent} from "./basicSolution/editBasicSolution/edit-basic-solution.component";
import {NgxDropzoneModule} from "ngx-dropzone";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {ViewBasicSolutionComponent} from "./basicSolution/viewBasicSolution/view-basic-solution.component";

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(SettingsRouting),
    FormsModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    MonacoEditorModule
  ],
  declarations: [
    BasicSolutionComponent,
    IssueRaisedSystemComponent,
    LabelsComponent,
    IssueTypeListComponent,
    EditIssueTypeComponent,
    EditBasicSolutionComponent,
    ViewBasicSolutionComponent
  ]
})
export class SettingsModule{

}
