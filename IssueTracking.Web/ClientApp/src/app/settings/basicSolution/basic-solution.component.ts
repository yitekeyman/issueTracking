import {Component, OnInit} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
import dialog from "../../components/dialog";
import {Router} from "@angular/router";

@Component({
  selector:'app-basic-solution',
  templateUrl:'./basic-solution.component.html'
})
export class BasicSolutionComponent implements OnInit{
  public basicSolutionList = [];
  public pager: any = {};
  pagedItems: any[];
  public isEdit = false;
  public isAdd = false;
  public selectedSolution: any;
  public selectedIssueType:any;
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public loggedInEmployeeId: string;
  public loggedInDepartmentId: string | "";
  constructor(public issueTrackingService: IssueTrackingService, public pagerService: PagerService, public router:Router) {
    this.loggedInEmployeeId = localStorage.getItem('userId');
    this.loggedInDepartmentId = localStorage.getItem("departmentId");
  }
  ngOnInit() {
    this.getAllBasicSolutions();
  }

  getAllBasicSolutions(){
    dialog.loading();
    this.closeModal();
    this.issueTrackingService.GetAllBasicSolution().subscribe(res => {
      this.basicSolutionList = res;
      if (this.basicSolutionList.length > 0) {
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

    this.pager = this.pagerService.getPager(this.basicSolutionList.length, page);

    //get the paged items
    this.pagedItems = this.basicSolutionList.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public editBasicSolution(id: any, issueTypeId:any) {
    this.selectedIssueType=issueTypeId;
    if (id > 0) {
      this.isAdd = false;
      this.isEdit = true;
      this.selectedSolution = id;
    } else {
      this.isEdit = false;
      this.isAdd = true;
      this.selectedSolution = null;

    }
  }
  public closeModal() {
    this.isEdit = false;
    this.isAdd = false;
    this.selectedSolution=null;
    this.selectedIssueType=0;
  }

  public seeBasicSolution(id:any){
    this.router.navigate(['/LIT/settings/view-basic-solution',id]);
  }
}
