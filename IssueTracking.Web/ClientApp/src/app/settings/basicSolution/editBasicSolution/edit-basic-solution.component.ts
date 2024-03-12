import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild} from "@angular/core";
import {FormBuilder, FormGroup, Validator, Validators} from "@angular/forms";
import {IssueTrackingService} from "../../../_Services/IssueTrackingService";
import {BasicSolutionModel, ResourceModel} from "../../../_model/IssueTrackingModel";
import dialog from "../../../components/dialog";
import swal from "sweetalert2";
import {
  MonacoEditorComponent, MonacoEditorConstructionOptions,
  MonacoEditorLoaderService,
  MonacoStandaloneCodeEditor
} from "@materia-ui/ngx-monaco-editor";
import {Observable, ReplaySubject, take} from "rxjs";
import {filter} from "rxjs/operators";
import {MonacoDiffEditorConstructionOptions} from "@materia-ui/ngx-monaco-editor/lib/interfaces";

@Component({
  selector: 'app-edit-basic-solution',
  templateUrl: './edit-basic-solution.component.html',
  styleUrls: ['./edit-basic-solution.component.scss']
})
export class EditBasicSolutionComponent implements OnInit {
  @Input() public selectedSolution = 0;
  @Input() public selectedIssueType = 0;
  @Output() public loadPage = new EventEmitter();
  @Output() public closeModal = new EventEmitter();

  public issueTypeList = [];
  public solutionModal: BasicSolutionModel;
  public resourceModels: ResourceModel[] = [];
  public resourceModel: ResourceModel;
  public solutionEditorForm: FormGroup;
  public files: File[] = [];

  public editorOptions: MonacoEditorConstructionOptions = {
    theme: 'vs-dark',
    language: 'sql',
    readOnly: false,
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
  constructor(public fb: FormBuilder, public issueTrackingService: IssueTrackingService, private monacoLoaderService: MonacoEditorLoaderService) {

    this.issueTrackingService.GetAllIssueType(-1).subscribe(res => {
      this.issueTypeList = res;
    });
    this.solutionEditorForm = this.fb.group({
      issueType: ['0', [Validators.required, Validators.min(1)]],
      query: [''],
      description: ['', Validators.required],
      resource: ['']
    })
  }
  ngOnInit() {
  this.solutionModal = {
      id: 0,
      issueTypeId: this.selectedIssueType,
      solutionDescription: '',
      solutionQuery: '',
      solutionResource: this.resourceModels
    };
    this.resourceModel = {
      docRef: "",
      fileName:"",
      data: "",
      mimeType: "",
      index: 0
    };
    if (this.selectedSolution >0) {
      this.issueTrackingService.GetBasicSolutionById(this.selectedSolution).subscribe(res=>{
        this.solutionModal = {
          id: res.id,
          issueTypeId: res.issueTypeId,
          solutionDescription: res.solutionDescription,
          solutionQuery: res.solutionQuery,
          solutionResource: res.solutionResource
        };
        this.resourceModels = res.solutionResource;
        for(let i=0; i<this.resourceModels.length; i++){
          let image=this.issueTrackingService.convertBase64ToFile(this.resourceModels[i]);
          this.files.push(image);
        }
      });
    }
  }

  public saveChanges() {
    dialog.loading();
    this.issueTrackingService.EditBasicIssueSolution(this.solutionModal).subscribe(res => {
      dialog.close();
      swal({
        type: 'success',
        title: 'You have Successfully Save Changes',
        showConfirmButton: false,
        timer: 1500
      }).then(value => {
        this.loadPage.next(true);
        this.close();
      });
    }, e => {
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }

  public close() {
    let btn = document.getElementById('close');
    btn.click();
    this.closeModal.next(true);
  }

  onSelect(event) {
    let files = event.addedFiles;
    if (files) {
      for (let i = 0; i < files.length; i++) {
        //this.resetResourceModel();
        let file = files[i];
        if (file) {
          this.issueTrackingService.convertFileToBase64(file).subscribe(base64 => {
            this.resourceModel.data = base64;
            this.resourceModel.mimeType = file.type;
            this.resourceModel.fileName=file.name;
            let resMod=this.resourceModel;
            this.resourceModels.push(resMod);
            this.resetResourceModel();
          })
        }
      }
    }
    this.files.push(...event.addedFiles);
  }

  onRemove(event) {
    this.files.splice(this.files.indexOf(event), 1);
    this.resourceModels.splice(this.resourceModels.indexOf(event), 1);
  }

  public resetResourceModel() {
    this.resourceModel = {
      docRef: "",
      fileName:"",
      data: "",
      mimeType: "",
      index: 0
    }
  }
}
