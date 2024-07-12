import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {IssueTrackingService} from "../../../_Services/IssueTrackingService";
import {MilestoneModel} from "../../../_model/IssueTrackingModel";
import dialog from "../../../components/dialog";
import swal from "sweetalert2";
import {DatePipe} from "@angular/common";

@Component({
    selector:'app-view-milestone',
    templateUrl:'./view_milestone.component.html'
})
export class ViewMilestoneComponent implements OnInit{
    @Output() public loadPage = new EventEmitter();
    @Output() public closeModal = new EventEmitter();

    @Output() closeForm=new EventEmitter();
    @Input()  milestoneModel:MilestoneModel;
    public milestoneList = [];
    public milestoneForm:FormGroup;
    constructor(public fb:FormBuilder, public issueTrackingService:IssueTrackingService) {
    }

    ngOnInit() {
        this.getMilestone();
    }
    
    public getMilestone() {
        dialog.loading();
        this.issueTrackingService.GetMilestoneById(this.milestoneModel.id).subscribe(res => {
            this.milestoneList = res;
            dialog.close();
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

    public closeTab(){
        let btn = document.getElementById('close');
        btn.click();
        this.closeModal.next(true);
    }
}
