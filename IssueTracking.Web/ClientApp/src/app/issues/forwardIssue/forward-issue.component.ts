import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {IssueForwardModel, ResourceModel} from "../../_model/IssueTrackingModel";
import dialog from "../../components/dialog";
import swal from "sweetalert2";

@Component({
  selector:'app-forward-to',
  templateUrl:'./forward-issue.component.html'
})
export class ForwardIssueComponent implements OnInit{
  @Input() public issueId:null;
  @Output() public loadPage = new EventEmitter();
  @Output() public closeModal = new EventEmitter();

  forwardModel:IssueForwardModel;
  forwardForm:FormGroup;
  departmentList=[];
  employeeList=[];
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public resourceModel: ResourceModel;
  public resourceModels: ResourceModel[] = [];
  public files: File[] = [];
  constructor(public fb:FormBuilder, public issueTrackingService:IssueTrackingService) {
    this.issueTrackingService.GetHeadOfficeDept().subscribe(res=>{
      this.departmentList=res;
    })
    this.getEmployees(this.ITDeptId);

    this.forwardForm=this.fb.group({
      forwardToDept:['', Validators.required],
      forwardToEmp:[''],
      remark:['', Validators.required],
      resource:['']
    })
  }
  ngOnInit() {
    this.forwardModel={
      id:"",
      forwardToDept:this.ITDeptId,
      forwardToEmp:'',
      remark:'',
      issueId:this.issueId,
      issueResource:this.resourceModels
    }

    this.resourceModel = {
      docRef: '',
      fileName: '',
      data: '',
      mimeType: '',
      index: 0
    };
  }

  getEmployees(id:string){
    dialog.loading();
    this.employeeList=[];
    this.issueTrackingService.GetAllEmployeeByBranchId(id).subscribe(res=>{
      this.employeeList=res;
      dialog.close();
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

  public resetResourceModel() {
    this.resourceModel = {
      docRef: "",
      fileName: "",
      data: "",
      mimeType: "",
      index: 0
    }
  }

  public saveChanges(){
    dialog.loading();
    this.issueTrackingService.ForwardIssue(this.forwardModel).subscribe(res=>{
      dialog.close();
      swal({
        type: 'success',
        title: 'You have Successfully Forwarded Issue',
        showConfirmButton: false,
        timer: 1500
      }).then(value => {
        this.close();
        this.loadPage.next(true);
      });
    }, e => {
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }
}


