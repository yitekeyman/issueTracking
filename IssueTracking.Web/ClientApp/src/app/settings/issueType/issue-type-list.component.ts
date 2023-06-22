import {Component, OnInit} from "@angular/core";
import dialog from "../../components/dialog";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
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

  constructor(public issueTrackingService: IssueTrackingService, public pagerService: PagerService) {
  }

  ngOnInit() {
    this.getIssueTypes();
  }

  public getIssueTypes() {
    dialog.loading();
    this.closeModal();
    this.issueTrackingService.GetAllIssueType().subscribe(res => {
      this.issueTypeList = res;
      if (this.issueTypeList.length > 0) {
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
}
