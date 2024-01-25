import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {IssueTrackingService} from "../../../_Services/IssueTrackingService";
import {MilestoneModel} from "../../../_model/IssueTrackingModel";
import dialog from "../../../components/dialog";
import swal from "sweetalert2";
import {DatePipe} from "@angular/common";

@Component({
  selector:'app-edit-milestone',
  templateUrl:'./edit_milestone.component.html'
})
export class EditMilestoneComponent implements OnInit{
  @Input() public selectedMilestone=null;
  @Output() public loadPage = new EventEmitter();
  @Output() public closeModal = new EventEmitter();

  public milestoneModel:MilestoneModel;
  public milestoneForm:FormGroup;
  constructor(public fb:FormBuilder, public issueTrackingService:IssueTrackingService) {
    this.milestoneForm=this.fb.group({
      name:['', Validators.required],
      description:[''],
      dueDate:['']
    })
  }

  ngOnInit() {
    this.milestoneModel={
      id:'',
      name:'',
      description:'',
      dueDate:null
    }
    let dateTimePipe = new DatePipe("en-US");
    if(this.selectedMilestone!=null){
      this.milestoneModel={
        id:this.selectedMilestone.id,
        name:this.selectedMilestone.name,
        description:this.selectedMilestone.description,
        dueDate:this.selectedMilestone.dueDate
      }

      if(this.milestoneModel.dueDate!='0001-01-01T00:00:00')
        this.milestoneModel.dueDate=dateTimePipe.transform(this.milestoneModel.dueDate,'yyyy-MM-dd');
    }
  }

  public saveChanges() {
    dialog.loading();
    // if(this.milestoneModel.dueDate==null || this.milestoneModel.dueDate=='')
    //   this.milestoneModel.dueDate='0001-01-01';
    this.issueTrackingService.EditMileStone(this.milestoneModel).subscribe(res => {
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
}
