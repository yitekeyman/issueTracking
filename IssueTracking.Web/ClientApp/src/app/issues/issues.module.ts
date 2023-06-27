import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgxDropzoneModule} from "ngx-dropzone";
import {MonacoEditorModule} from "@materia-ui/ngx-monaco-editor";
import {IssuesRouting} from "./issues.routing";
import {IssuesListComponent} from "./issues-list/issues-list.component";

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(IssuesRouting),
    FormsModule,
    ReactiveFormsModule,
    NgxDropzoneModule,
    MonacoEditorModule
  ],
  declarations: [
    IssuesListComponent
  ]
})
export class IssuesModule{

}
