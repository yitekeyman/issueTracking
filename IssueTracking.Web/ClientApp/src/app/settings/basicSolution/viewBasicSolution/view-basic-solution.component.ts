import {Component, OnInit, ViewChild} from "@angular/core";
import {IssueTrackingService} from "../../../_Services/IssueTrackingService";
import {ActivatedRoute} from "@angular/router";
import dialog from "../../../components/dialog";
import {configs} from "../../../app-config";
import {
  MonacoEditorComponent,
  MonacoEditorConstructionOptions, MonacoEditorLoaderService,
  MonacoStandaloneCodeEditor
} from "@materia-ui/ngx-monaco-editor";

@Component({
  selector:'app-view-basic-solution',
  templateUrl:'./view-basic-solution.component.html',
  styleUrls:['./view-basic-solution.component.scss']
})
export class ViewBasicSolutionComponent implements OnInit{
public basicSolution:any=[];
public solutionId:any=null;
public issueTypeId:any=null;
public url=configs.url;
  public files: File[] = [];
  public editorOptions: MonacoEditorConstructionOptions = {
    theme: 'vs',
    language: 'sql',
    readOnly: true,
    minimap: {enabled: true}
  };
  @ViewChild(MonacoEditorComponent, {static: true})
  monacoComponent: MonacoEditorComponent;

  editorInit(editor: MonacoStandaloneCodeEditor) {
    editor.setSelection({
      startLineNumber: 1,
      startColumn: 1,
      endColumn: 50,
      endLineNumber: 3
    });
  }
constructor(public issueTrackingService:IssueTrackingService, public activeRouting:ActivatedRoute,private monacoLoaderService: MonacoEditorLoaderService) {
  if(this.activeRouting.snapshot.params['solution-id']){
    this.solutionId=this.activeRouting.snapshot.params['solution-id'];
  }else if(this.activeRouting.snapshot.params['issue-type-id']){
    this.issueTypeId=this.activeRouting.snapshot.params['issue-type-id'];
  }


}
  ngOnInit() {
   //this.loadSolutionPage();
  }

  loadSolutionPage(){
    dialog.loading();
    if(this.issueTypeId!=null){
      this.issueTrackingService.GetBasicSolutionByIssueType(this.issueTypeId).subscribe(res=>{
        this.basicSolution=res;
        dialog.close()
      },dialog.error);
    }
  }
}
