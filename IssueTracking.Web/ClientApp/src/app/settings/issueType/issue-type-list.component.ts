import {Component, OnInit} from "@angular/core";
import dialog from "../../components/dialog";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
import {Router} from "@angular/router";
@Component({
  selector: 'app-issue-type-list',
  templateUrl: './issue-type-list.component.html'
})
export class IssueTypeListComponent implements OnInit {
  public issueTypeList = [];
  public pager: any = {};
  pagedItems: any[];
  public isEdit = false;
  public isAdd = false;
  public selectedIssueType: any;
  public selectedSolution: any;
  public isAddSolution = false;
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public loggedInEmployeeId: string;
  public loggedInDepartmentId: string | "";
  public issueRaisedSystemList=[];
  public raisedSystem=-1;
  constructor(public issueTrackingService: IssueTrackingService, public pagerService: PagerService, public router:Router) {
    this.loggedInEmployeeId = localStorage.getItem('userId');
    this.loggedInDepartmentId = localStorage.getItem("departmentId");
    this.issueTrackingService.GetAllIssueRaisedSystems().subscribe(res=>{
      this.issueRaisedSystemList=res;
    })
  }

  ngOnInit() {
    this.getIssueTypes();
  }

  public getIssueTypes() {
    dialog.loading();
    this.closeModal();
    this.issueTrackingService.GetAllIssueType(this.raisedSystem).subscribe(res => {
      this.issueTypeList = res;
      if (this.issueTypeList.length > 0 && this.raisedSystem==-1) {
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
    // if (page < 1 || page > this.pager.totalPages) {
    //   return;
    // }

    this.pager = this.pagerService.getPager(this.issueTypeList.length, page);

    //get the paged items
    this.pagedItems = this.issueTypeList.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public editIssueType(index: any, id: any) {
    if (index == '1') {
      this.isAdd = false;
      this.isEdit = true;
      this.selectedIssueType = id;
    } else {
      this.isEdit = false;
      this.isAdd = true;
      this.selectedIssueType = null;
    }
  }

  public addSolution(id:any) {
    this.isAddSolution = true;
    this.selectedSolution = null;
    this.selectedIssueType=id;
  }

  public closeModal() {
    this.isEdit = false;
    this.isAdd = false;
    this.isAddSolution = false;
    this.selectedIssueType = null;
  }

  public seeSolutionDetails(id: any) {
    this.router.navigate(['LIT/settings/basic-solution-issue-type', id]);

  }
}
