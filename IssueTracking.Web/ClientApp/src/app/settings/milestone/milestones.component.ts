import {Component, OnInit} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
import dialog from "../../components/dialog";
import {Router} from "@angular/router";
import {MilestoneModel} from "../../_model/IssueTrackingModel";
import swal from "sweetalert2";

@Component({
  selector:'app-milestones',
  templateUrl:'./milestones.component.html'
})
export class MilestonesComponent implements OnInit{
  public milestoneList = [];
  public pager: any = {};
  pagedItems: any[];
  public isEdit = false;
  public isAdd = false;
  public isView=false;
  public selectedMilestone: MilestoneModel|any;
  public editedMilestoneId=null;
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public loggedInEmployeeId: string;
  public loggedInDepartmentId: string | "";

  constructor(public issueTrackingServices:IssueTrackingService, public pagerService: PagerService,public router: Router ) {
    this.loggedInEmployeeId = localStorage.getItem('userId');
    this.loggedInDepartmentId = localStorage.getItem("departmentId");
  }
  ngOnInit() {
    this.getAllMilestones();
  }

  getAllMilestones(){
    dialog.loading();
    this.closeModal();
    this.issueTrackingServices.GetAllMilestones().subscribe(res => {
      this.milestoneList = res;
      if (this.milestoneList.length > 0) {
        if (this.pager.currentPage == 0) {
          this.setPage(1);
        } else {
          this.setPage(this.pager.currentPage);
        }
      } else {
        this.setPage(1);
      }
      dialog.close();
    }, dialog.error)

  }

  public setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.milestoneList.length, page);

    //get the paged items
    this.pagedItems = this.milestoneList.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public editMilestone(id:any) {
    if (id !=null) {
      this.isAdd = false;
      this.isEdit = true;
      this.editedMilestoneId = id;
    } else {
      this.isEdit = false;
      this.isAdd = true;
      this.editedMilestoneId = null;
      this.selectedMilestone = {};
    }
  }
  public viewMilestone(milestone:any){
    this.closeModal();
    this.selectedMilestone = milestone;
    this.isView = true;
  }


  removeMilestone(index: number) {
    swal({
      title: 'Are you sure?',
      text: 'You want to remove this milestone',
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Remove!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingServices.DeleteMilestone(this.pagedItems[index].id).subscribe(
            () => {
              this.pagedItems.splice(index, 1);
              swal({
                type: 'success',
                title: 'You have Successfully Deleted Milestone',
                showConfirmButton: false,
                timer: 1500
              })
            },
            (error) => {
              swal({
                type: 'error', title: 'Oops...', text: 'Failed to delete milestone.'
              })
            }
        );
      }
    });
  }

  public closeModal() {
    this.isEdit = false;
    this.isAdd = false;
    this.isView= false;
    this.selectedMilestone=null;
  }

}
