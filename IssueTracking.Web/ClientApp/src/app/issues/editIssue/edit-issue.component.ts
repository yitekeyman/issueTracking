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
import {ActivatedRoute, Router} from "@angular/router";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-edit-issue',
  templateUrl: './edit-issue.component.html',
  styleUrls: ['./edit-issue.component.scss']
})
export class EditIssueComponent implements OnInit {
  @Input() public selectedIssue = null;
  @Input() public selectedIssueType = 0;
  @Output() public loadPage = new EventEmitter();
  @Output() public closeModal = new EventEmitter();

  public priorityList = [];
  public issueTypeList = [];
  public issueRaisedSystemList = [];
  public selectIssueTypeList = [];
  public issueRaisedSystem: any = 1;
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
  public actRouting = null;
  public headOffice=[];
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";

  constructor(public fb: FormBuilder, public issueTrackingService: IssueTrackingService, private monacoLoaderService: MonacoEditorLoaderService, public router: Router, public activeRouting: ActivatedRoute) {
    if (this.activeRouting.snapshot.params['issueId']) {
      this.actRouting = this.activeRouting.snapshot.params['issueId'];
    }
    this.issueTrackingService.GetAllIssueType(-1).subscribe(res => {
      this.issueTypeList = res;
      this.changeIssueRaisedSystem();
    });

    this.issueTrackingService.GetAllPriorityTypes().subscribe(res => {
      this.priorityList = res;
    });

    this.issueTrackingService.GetAllIssueRaisedSystems().subscribe(res => {
      this.issueRaisedSystemList = res;
    })
    // this.issueTrackingService.GetAllIssues().subscribe(res => {
    //   this.issueList = res;
    // });
    this.issueTrackingService.GetHeadOfficeDept().subscribe(res=>{
      this.headOffice=res;
    })
    function policyNoFormatValidator(): ValidatorFn {
      return (control: FormControl) => {
        if (control.value != "") {
          const policyNos: string = control.value.split(',').map((value: string) => value.trim()); // Split the comma-separated values into an array
          const pattern: RegExp = /^P\/\d{1,3}\/\d{1,4}\/\d{4}\/\d{5}$/;
          const pattern2: RegExp = /^C\/\d{1,3}\/\d{1,4}\/\d{4}\/\d{6}$/;

          // Check each policy number against the pattern
          for (const policyNo of policyNos) {
            if (!pattern.test(policyNo) && !pattern2.test(policyNo)) {
              return {policyNoFormat: true}; // Return validation error if the format is invalid
            }
          }

          // Check for duplicate policy numbers
          if (new Set(policyNos).size !== policyNos.length) {
            return {duplicatePolicyNo: true}; // Return validation error if there are duplicate policy numbers
          }
        }
        return null; // Return null if all policy numbers are valid
      };
    }

    this.addIssueForm = this.fb.group({
      issueTitle: ['', Validators.required],
      issueRaisedSystem: ['1', Validators.required],
      issueType: [-1, [Validators.required, Validators.min(0)]],
      policyNo: ['', [policyNoFormatValidator()]],
      issueDescription: ['', Validators.required],
      issuePriority: [1, Validators.required],
      resource: [''],
      forwardTo:['']
    })

  }

  ngOnInit() {
    this.issueModal = {
      id: "",
      issueTitle: '',
      issueTypeId: -1,
      otherIssue: '',
      policyNo: '',
      issueDescription: '',
      issuePriority: 1,
      issueResource: this.resourceModels,
      forwardTo:this.ITDeptId
    }
    this.issueRaisedSystem = 1;
    this.resourceModel = {
      docRef: '',
      fileName: '',
      data: '',
      mimeType: '',
      index: 0
    };
    if (this.selectedIssue != null) {
      dialog.loading();
      this.issueTrackingService.GetIssueById(this.selectedIssue).subscribe(res => {
        this.issueModal = {
          id: res.id,
          issueTitle: res.issueTitle,
          issueTypeId: res.issueTypeId,
          otherIssue: res.otherIssue,
          policyNo: res.policyNo,
          issueDescription: res.issueDescription,
          issuePriority: res.issuePriority.id,
          issueResource: res.issueResource,
          forwardTo:res.forwardTo
        };
        this.issueRaisedSystem = res.issueType.raisedSystem.id;
        if(res.issueTypeId==0)
          this.issueRaisedSystem=res.otherIssue;
        this.changeIssueRaisedSystem();
        this.resourceModels = res.issueResource;
        for (let i = 0; i < this.resourceModels.length; i++) {
          let image = this.issueTrackingService.convertBase64ToFile(this.resourceModels[i]);
          this.files.push(image);
        }
        dialog.close();
      });
    } else {
      this.changeIssueRaisedSystem();
    }
  }

  public changeIssueType() {
    for (let issueType of this.selectIssueTypeList) {
      if (this.issueModal.issueTypeId == issueType.id) {
        this.issueModal.issueTitle = issueType.name;
      }
    }
  }

  public changeIssueRaisedSystem() {
    this.selectIssueTypeList = [];
    for (let issueType of this.issueTypeList) {
      if (this.issueRaisedSystem == issueType.raisedSystem.id) {
        this.selectIssueTypeList.push(issueType);
      }
    }
    for (let issueType of this.issueTypeList) {
      if (issueType.id == 0) {
        this.selectIssueTypeList.push(issueType);
      }
    }
  }

  //sanitizedUrl: any;
  public saveChanges() {
    dialog.loading();
    if(this.issueModal.issueTypeId==0)
      this.issueModal.otherIssue=this.issueRaisedSystem;
    if (this.issueModal.id == '') {
      this.issueTrackingService.AddIssue(this.issueModal).subscribe(res => {
        dialog.close();
        swal({
          type: 'success',
          title: 'You have Successfully saved Changes',
          showConfirmButton: false,
          timer: 1500
        }).then(value => {
          this.close();
          this.seeIssueDetails(res);
          //this.loadPage.next(true);

        });
      }, e => {
        swal({
          type: 'error', title: 'Oops...', text: e.message
        });
      })
    } else {
      this.issueTrackingService.EditIssue(this.issueModal).subscribe(res => {
        dialog.close();
        swal({
          type: 'success',
          title: 'You have Successfully saved Changes',
          showConfirmButton: false,
          timer: 1500
        }).then(value => {
          //this.loadPage.next(true);
          this.close();
          this.seeIssueDetails(this.issueModal.id);
        });
      }, e => {
        swal({
          type: 'error', title: 'Oops...', text: e.message
        });
      })
    }
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
            this.resourceModel.fileName = file.name;
            let resMod = this.resourceModel;
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

  public seeIssueDetails(id: any) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['LIT/issues/view-issue', id]);
    });
  }

  public resetResourceModel() {
    this.resourceModel = {
      docRef: "",
      fileName: "",
      data: "",
      mimeType: "",
      index: 0
    }
  }


}
