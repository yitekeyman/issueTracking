import {Component, OnInit,} from "@angular/core";
import {IssueFilterParameter, IssueListModel, IssueListReturnModel, ResourceModel} from "../_model/IssueTrackingModel";
import {IssueTrackingService} from "../_Services/IssueTrackingService";
import {PagerService} from "../_Services/pager.service";
import {Router} from "@angular/router";
import dialog from "../components/dialog";
import {FormBuilder} from "@angular/forms";


@Component({
  selector:'app-issues',
  templateUrl:'./issues.component.html'
})
export class IssuesComponent implements OnInit{

  public filterParameter: IssueFilterParameter;

  public issuesList: [];
  public issueListReturn: IssueListReturnModel;
  public pager: any = {};
  public addIssueModalId = 'addIssueModal';
  public isAdd = false;
  public isEdit = false;
  public selectedIssue = 0;
  public selectedIssueType = 0;
  pagedItems: any[];
  public pager1: any = {};
  pagedItems1: any[];

  constructor(public fb: FormBuilder, public issueTrackingService: IssueTrackingService, public pagerService: PagerService, public router:Router) {


  }

  ngOnInit() {
    this.getAllIssues();
  }
  public openAddIssueModal() {
    this.router.navigate(['/LIT/issues/edit-issue']);
  }
  public getAllIssues(){
    dialog.loading();
    this.closeModal();
    this.issueTrackingService.GetAllIssues().subscribe(res => {
      this.issuesList = res;
      if (this.issuesList.length > 0) {
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

    this.pager = this.pagerService.getPager(this.issuesList.length, page);

    //get the paged items
    this.pagedItems = this.issuesList.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public setOpenPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.issueListReturn.opened?.length, page);

    //get the paged items
    this.pagedItems = this.issueListReturn.opened.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public setClosedPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager1 = this.pagerService.getPager(this.issueListReturn.closed?.length, page);

    //get the paged items
    this.pagedItems1 = this.issueListReturn.closed.slice(this.pager1.startIndex, this.pager1.endIndex + 1);

  }

  public editIssue(id: any, issueTypeId:any) {
    this.selectedIssueType=issueTypeId;
    if (id > 0) {
      this.isAdd = false;
      this.isEdit = true;
      this.selectedIssue = id;
    } else {
      this.isEdit = false;
      this.isAdd = true;
      this.selectedIssue = null;

    }
  }
  public seeIssue(id:any){

  }

  public closeModal() {
    this.isEdit = false;
    this.isAdd = false;
    this.selectedIssue=null;
    this.selectedIssueType=0;
  }

}
