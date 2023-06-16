import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {FormBuilder, FormGroup, Validator, Validators} from "@angular/forms";
import {IssueTrackingService} from "../../../_Services/IssueTrackingService";
import {IssueRaisedSystemModel, IssueTypeModel} from "../../../_model/IssueTrackingModel";
import swal from "sweetalert2";
import dialog from "../../../components/dialog";

@Component({
  selector:'app-edit-issue-type',
  templateUrl:'./edit-issue-type.component.html'
})
export class EditIssueTypeComponent implements OnInit{
  @Input() public selectedIssueType=null;
  @Output() loadPage=new EventEmitter();
  @Output() closeModal=new EventEmitter();

public issueTypeEditorForm:FormGroup;
public issueTypeModel:IssueTypeModel;
public issueRaisedSystems=[];
  constructor(public fb:FormBuilder, public issueTrackingService:IssueTrackingService){
    this.issueTrackingService.GetAllIssueRaisedSystems().subscribe(res=>{
      this.issueRaisedSystems=res;
    });
    this.issueTypeEditorForm=this.fb.group({
      name:['', Validators.required],
      description:[''],
      raisedSystem:[0]
    });
  }
  ngOnInit() {
    this.issueTypeModel={
      id:0,
      name:'',
      description:'',
      raisedSystemId:0
    }

    if(this.selectedIssueType!=null){
      this.issueTypeModel={
        id:this.selectedIssueType.id,
        name:this.selectedIssueType.name,
        description:this.selectedIssueType.description,
        raisedSystemId:this.selectedIssueType.raisedSystem.id
      }
    }
  }

  public saveChanges(){
    dialog.loading();
    this.issueTrackingService.EditIssueType(this.issueTypeModel).subscribe(res => {
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

  public close(){
    let btn=document.getElementById('close');
    btn.click();
    this.closeModal.next(true);
  }
}
