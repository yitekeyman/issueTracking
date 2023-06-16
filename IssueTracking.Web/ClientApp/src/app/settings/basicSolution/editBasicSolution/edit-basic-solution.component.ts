import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {FormBuilder, FormGroup, Validator, Validators} from "@angular/forms";
import {IssueTrackingService} from "../../../_Services/IssueTrackingService";
import {BasicSolutionModel, ResourceModel} from "../../../_model/IssueTrackingModel";
import dialog from "../../../components/dialog";
import swal from "sweetalert2";

@Component({
  selector:'app-edit-basic-solution',
  templateUrl:'./edit-basic-solution.component.html'
})
export class EditBasicSolutionComponent implements OnInit{
  @Input() public selectedSolution=null;
  @Input() public selectedIssueType=0;
  @Output() public loadPage=new EventEmitter();
  @Output() public closeModal=new EventEmitter();

  public issueTypeList=[];
  public solutionModal:BasicSolutionModel;
  public resourceModel:ResourceModel[];
  public solutionEditorForm:FormGroup;
  constructor(public fb:FormBuilder, public issueTrackingService:IssueTrackingService) {
    this.issueTrackingService.GetAllIssueType().subscribe(res=>{
      this.issueTypeList=res;
    });
    this.solutionEditorForm=this.fb.group({
      issueType:['0', [Validators.required, Validators.min(1)]],
      query:[''],
      description:[''],
      resource:['']
    })
  }
  ngOnInit() {
    this.solutionModal={
      id:0,
      issueTypeId:this.selectedIssueType,
      solutionDescription:'',
      solutionQuery:'',
      solutionResource:this.resourceModel
    };

    if(this.selectedSolution!=null){
      this.solutionModal={
        id:this.selectedSolution.id,
        issueTypeId:this.selectedSolution.issueTypeId,
        solutionDescription:this.selectedSolution.solutionDescription,
        solutionQuery:this.selectedSolution.solutionQuery,
        solutionResource:this.selectedSolution.solutionResource
      };
      this.resourceModel=this.selectedSolution.solutionResource;
    }
  }

  public saveChanges(){
    dialog.loading();
    this.solutionModal.solutionResource=this.resourceModel;
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
  public close(){
    let btn=document.getElementById('close');
    btn.click();
    this.closeModal.next(true);
  }
}
