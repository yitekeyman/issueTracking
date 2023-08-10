import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild} from "@angular/core";
import {FormBuilder, FormGroup, Validator, Validators} from "@angular/forms";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {IssueListModel, ResourceModel} from "../../_model/IssueTrackingModel";
import dialog from "../../components/dialog";
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
  selector: 'app-edit-issue',
  templateUrl: './edit-issue.component.html',
  styleUrls: ['./edit-issue.component.scss']
})
export class EditIssueComponent implements OnInit {
  @Input() public selectedIssue = 0;
  @Input() public selectedIssueType = 0;
  @Output() public loadPage = new EventEmitter();
  @Output() public closeModal = new EventEmitter();

  public priorityList = [];
  public issueTypeList = [];
  public issueList = [];
  public issueModal: IssueListModel;
  public resourceModels: ResourceModel[] = [];
  public resourceModel: ResourceModel;
  public addIssueForm: FormGroup;
  public files: File[] = [];
  public isAdd = false;
  public isEdit = false;


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
  constructor(public fb: FormBuilder, public issueTrackingService: IssueTrackingService, private monacoLoaderService: MonacoEditorLoaderService)
  {

    this.issueTrackingService.GetAllIssueType().subscribe(res => {
      this.issueTypeList = res;
    });

    this.issueTrackingService.GetAllPriorityTypes().subscribe(res => {
      this.priorityList = res;
    });

    this.issueTrackingService.GetAllIssues().subscribe(res => {
      this.issueList = res;
    });

  }
  ngOnInit() {

    this.issueModal = {
      id: 0,
      issueTypeId: this.selectedIssueType,
      issueTitle: '',
      otherIssue: '',
      policyNo: [''],
      issueDescription: '',
      issuePriority: 0,
      ticket: '',
      issueResource: this.resourceModels,
    };

    this.addIssueForm = this.fb.group({
      issueTitle: [''],
      issueTypeId: [''],
      issueType: [0, [Validators.required, Validators.min(1)]],
      otherIssue: [''],
      PolicyNo: [''],
      issueDescription: ['', Validators.required],
      issuePriority: ['', [Validators.required, Validators.min(1)]],
      ticket: [''],
      resource: [''],
    })


    this.resourceModel = {
      docRef: "",
      fileName:"",
      data: "",
      mimeType: "",
      index: 0
    };
    if (this.selectedIssue >0) {
      this.issueTrackingService.GetIssueById(this.selectedIssue).subscribe(res=>{
        this.issueModal = {
          id: res.id,
          issueTitle: res.issueTitle,
          issueTypeId: res.issueTypeId,
          otherIssue: res.otherIssue,
          policyNo: res.policyNo,
          issueDescription: res.issueDescription,
          issuePriority: res.issuePriority,
          ticket: res.ticket,
          issueResource: res.issueResource,
        };
        this.resourceModels = res.issueResource;
        for(let i=0; i<this.resourceModels.length; i++){
          let image=this.issueTrackingService.convertBase64ToFile(this.resourceModels[i]);
          this.files.push(image);
        }
      });
    }
  }

  public editIssue(id: any, issueTypeId:any) {
    this.selectedIssueType=issueTypeId;
    if (id > 0) {
      this.isAdd = false;
      this.isEdit = true;
      this.selectedIssue = id;
    } else {
      this.isEdit = false;
      this.isAdd = true;
      this.selectedIssue = null;

    }
  }

  public saveChanges() {
    dialog.loading();
    this.issueTrackingService.GetIssueById(this.selectedIssue).subscribe(res => {
      dialog.close();
      swal({
        type: 'success',
        title: 'You have Successfully saved Changes',
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
