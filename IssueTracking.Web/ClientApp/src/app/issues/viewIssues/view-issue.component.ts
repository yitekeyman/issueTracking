import {Component, OnInit, ViewChild} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {ActivatedRoute} from "@angular/router";
import dialog from "../../components/dialog";
import {configs} from "../../app-config";
import {
  MonacoEditorComponent,
  MonacoEditorConstructionOptions, MonacoEditorLoaderService,
  MonacoStandaloneCodeEditor
} from "@materia-ui/ngx-monaco-editor";

@Component({
  selector:'app-view-issue',
  templateUrl:'./view-issue.component.html',
  styleUrls:['./view-issue.component.scss']
})
export class ViewIssueComponent implements OnInit{
  public issue:any=null;
  public issueId:any=null;
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
    if(this.activeRouting.snapshot.params['issue-id']){
      this.issueId=this.activeRouting.snapshot.params['issue-id'];
    }else if(this.activeRouting.snapshot.params['issue-type-id']){
      this.issueTypeId=this.activeRouting.snapshot.params['issue-type-id'];
    }


  }
  ngOnInit() {
    this.loadIssuePage();
  }

  loadIssuePage(){
    dialog.loading();
    if(this.issueId!=null){
      this.issueTrackingService.GetIssueById(this.issueId).subscribe(res=>{
        this.issue=res;
        for(let i=0; i<this.issue.IssueResource.length; i++){
          let image=this.issueTrackingService.convertBase64ToFile(this.issue.solutionResource[i]);
          this.files.push(image);

        }
        dialog.close()
      },dialog.error);
      /* }else if(this.issueTypeId!=null){
       this.issueTrackingService.GetBasicSolutionByIssueType(this.issueTypeId).subscribe(res=>{
         this.issue=res;
         dialog.close()
       },dialog.error);
     } */}
  }
}
