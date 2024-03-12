import {Component, OnInit} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
import dialog from "../../components/dialog";

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
  public selectedMilestone: any;
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public loggedInEmployeeId: string;
  public loggedInDepartmentId: string | "";

  constructor(public issueTrackingServices:IssueTrackingService, public pagerService: PagerService) {
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

  public editMilestone(milestone) {
    if (milestone !=null) {
      this.isAdd = false;
      this.isEdit = true;
      this.selectedMilestone = milestone;
    } else {
      this.isEdit = false;
      this.isAdd = true;
      this.selectedMilestone = null;

    }
  }
  public closeModal() {
    this.isEdit = false;
    this.isAdd = false;
    this.selectedMilestone=null;
  }

}
