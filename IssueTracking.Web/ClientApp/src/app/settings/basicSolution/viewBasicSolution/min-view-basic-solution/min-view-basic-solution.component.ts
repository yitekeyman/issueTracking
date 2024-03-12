import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from "@angular/core";
import {IssueTrackingService} from "../../../../_Services/IssueTrackingService";
import {
  MonacoEditorComponent,
  MonacoEditorConstructionOptions,
  MonacoEditorLoaderService, MonacoStandaloneCodeEditor
} from "@materia-ui/ngx-monaco-editor";
import dialog from "../../../../components/dialog";
import swal from "sweetalert2";
import {configs} from "../../../../app-config";
import {Router} from "@angular/router";

@Component({
  selector: 'app-min-view-basic-soltion',
  templateUrl: './min-view-basic-solution.component.html',
  styleUrls:['./min-view-basic-solution-component.css']
})
export class MinViewBasicSolutionComponent implements OnInit {
  @Input() basicSolutionId:any= null;
  @Input() issueTypeId:any=null;
  @Output() public closeModals = new EventEmitter();
  public basicSolution:any=[];
  public files: File[] = [];
  public isEdit=false;
  public selectedSolution=null;
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public loggedInEmployeeId: string;
  public loggedInDepartmentId: string | "";
  public editorOptions: MonacoEditorConstructionOptions = {
    theme: 'vs-light',
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
  constructor(public issueTrackingService:IssueTrackingService, private monacoLoaderService: MonacoEditorLoaderService, public router:Router) {
    this.loggedInEmployeeId = localStorage.getItem('userId');
    this.loggedInDepartmentId = localStorage.getItem("departmentId");
  }
  ngOnInit() {
    this.loadSolutionPage();
  }

  loadSolutionPage() {
    dialog.loading();
    if (this.basicSolutionId != null) {
      this.basicSolution=[];
      this.issueTrackingService.GetBasicSolutionById(this.basicSolutionId).subscribe(res => {
        this.basicSolution.push(res);

        dialog.close()
      }, error => {
        swal({
          type: 'error', title: 'Oops...', text: error.message
        })
      });
    }else if(this.issueTypeId!=null){
      this.issueTrackingService.GetBasicSolutionByIssueType(this.issueTypeId).subscribe(res => {
        this.basicSolution=res;

        dialog.close()
      }, error => {
        swal({
          type: 'error', title: 'Oops...', text: error.message
        })
      });
    }
  }

  setLink(doc) {
    return !doc.id && doc.mimeType && doc.Data ?
      `javascript:;` :
      `${configs.url}IssueTracking/GetFilePath?fileName=${doc.docRef}&mimeType=${doc.mimeType}`;
  }

  showImage(doc) {
    window.open(`data:${doc.mimeType};base64,${doc.data}`);
  }

  public editBasicSolution(id: any) {
    this.isEdit=true;
    this.selectedSolution=id;
  }
  public closeModal() {
    this.isEdit = false;
    this.selectedSolution=null;
  }

  public seeSolutionDetails(id: any) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['LIT/settings/view-basic-solution', id]);
    });
  }
}
