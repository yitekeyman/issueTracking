import {Component, OnInit} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
import {Router} from "@angular/router";
import {IssueFilterParameter, IssueListReturnModel} from "../../_model/IssueTrackingModel";
import dialog from "../../components/dialog";

@Component({
  selector: 'app-issues-list',
  templateUrl: './issues-list.component.html'
})
export class IssuesListComponent implements OnInit {

  public filterParameter: IssueFilterParameter;
  public userId = '';
  public departmentId = '';
  public issuesList: IssueListReturnModel;
  public pager: any = {};
  pagedItems: any[];
  public pager1: any = {};
  pagedItems1: any[];

  constructor(public issueTrackingService: IssueTrackingService, public pagerService: PagerService, public router: Router) {
    if (localStorage.getItem('role') == '6') {
      this.userId = localStorage.getItem('userId');
      this.departmentId = localStorage.getItem('departmentId');
    }
  }

  ngOnInit() {
    this.filterParameter = {
      userId: this.userId,
      branch: this.departmentId,
      priority: 0,
      issueType: 0,
      raisedSystem: 0,
      sort: 1
    };
    this.getAllIssues();
  }

  public getAllIssues() {
    dialog.loading();
    this.issueTrackingService.GetAllIssues(this.filterParameter).subscribe(res => {
      this.issuesList = res;
      if (this.issuesList.opened.length > 0) {
        if (this.pager.currentPage == 0) {
          this.setOpenPage(1);
        } else {
          this.setOpenPage(this.pager.currentPage);
        }
      } else {
        this.setOpenPage(1);
      }
      if (this.issuesList.closed.length > 0) {
        if (this.pager1.currentPage == 0) {
          this.setClosedPage(1);
        } else {
          this.setClosedPage(this.pager1.currentPage);
        }
      } else {
        this.setClosedPage(1);
      }
      dialog.close();
    },dialog.error)
  }

  public setOpenPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.issuesList.opened?.length, page);

    //get the paged items
    this.pagedItems = this.issuesList.opened.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public setClosedPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager1 = this.pagerService.getPager(this.issuesList.closed?.length, page);

    //get the paged items
    this.pagedItems1 = this.issuesList.closed.slice(this.pager1.startIndex, this.pager1.endIndex + 1);

  }
}
