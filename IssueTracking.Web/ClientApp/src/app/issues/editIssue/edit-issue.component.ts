import {AfterViewInit, Component, EventEmitter, Input, NgModule, OnInit, Output, ViewChild} from "@angular/core";
import {FormBuilder, FormControl, FormGroup, Validators, ValidatorFn} from "@angular/forms";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {
  DepartmentSchemaModel,
  EmployeeModel,
  IssueListModel,
  ResourceModel
} from "../../_model/IssueTrackingModel";
import dialog from "../../components/dialog";
import swal from "sweetalert2";
import {
  MonacoEditorComponent, MonacoEditorConstructionOptions,
  MonacoEditorLoaderService,
  MonacoStandaloneCodeEditor
} from "@materia-ui/ngx-monaco-editor";
import {Router} from "@angular/router";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-edit-issue',
  templateUrl: './edit-issue.component.html',
  styleUrls: ['./edit-issue.component.scss']
})
export class EditIssueComponent implements OnInit {
  @Input() public selectedIssue: any;
  @Input() public selectedIssueType = null;

  @Output() public loadPage = new EventEmitter();
  @Output() public closeModal = new EventEmitter();

  public priorityList = [];
  public issueTypeList = [];
  public issueList = [];
  public issueModal: IssueListModel;
  public resourceModels: ResourceModel[] = [];
  public department: DepartmentSchemaModel;
  public employee: EmployeeModel;
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

  constructor(public fb: FormBuilder, public issueTrackingService: IssueTrackingService, private monacoLoaderService: MonacoEditorLoaderService, public router: Router)
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

    function policyNoFormatValidator(): ValidatorFn {
      return (control: FormControl) => {
        const policyNos: string = control.value.split(',').map((value: string) => value.trim()); // Split the comma-separated values into an array
        const pattern: RegExp = /^P\/\d{1,3}\/\d{1,4}\/\d{4}\/\d{5}$/;

        // Check each policy number against the pattern
        for (const policyNo of policyNos) {
          if (!pattern.test(policyNo)) {
            return { policyNoFormat: true }; // Return validation error if the format is invalid
          }
        }

        // Check for duplicate policy numbers
        if (new Set(policyNos).size !== policyNos.length) {
          return { duplicatePolicyNo: true }; // Return validation error if there are duplicate policy numbers
        }

        return null; // Return null if all policy numbers are valid
      };
    }
    this.addIssueForm = this.fb.group({
      issueTitle: [''],
      issueType: [-1, [Validators.required, Validators.min(1)]],
      otherIssue: [''],
      policyNo: ['', [Validators.required, Validators.minLength(1), policyNoFormatValidator()]],
      issueDescription: ['', Validators.required],
      issuePriority: [0, Validators.required],
      resource: [''],
    })

  }
  ngOnInit() {
    this.issueModal = {
      id: "",
      issueTitle: '',
      issueTypeId: 0,
      otherIssue: '',
      policyNo: '',
      issueDescription:'',
      issuePriority: 0,
      issueResource: this.resourceModels,
    }
    this.resourceModel = {
      docRef: '',
      fileName: '',
      data: '',
      mimeType: '',
      index: 0
    };
    if (this.selectedIssue != null) {
      this.issueTrackingService.AddIssue(this.selectedIssue).subscribe(res=> {
        this.issueModal = {
          id: res.id,
          issueTitle: res.issueTitle,
          issueTypeId: res.issueTypeId,
          otherIssue: res.otherIssue,
          policyNo: res.policyNo,
          issueDescription: res.issueDescription,
          issuePriority: res.issuePriority,
          issueResource: res.issueResource
        }

        this.resourceModels = res.issueResource;
        for (let i = 0; i < this.resourceModels.length; i++) {
          let image = this.issueTrackingService.convertBase64ToFile(this.resourceModels[i]);
          this.files.push(image);
        }
      });
    }
  }

  sanitizedUrl: any;
  public saveChanges() {
    dialog.loading();
    this.issueTrackingService.AddIssue(this.issueModal).subscribe(res => {
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
