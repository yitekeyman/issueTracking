import {Component, OnInit} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import dialog from "../../components/dialog";
import {PagerService} from "../../_Services/pager.service";

@Component({
  selector:'app-issue-raised-system',
  templateUrl:'./issue-raised-system.component.html'
})
export class IssueRaisedSystemComponent implements OnInit{
  public issueRaisedSystem=[];
  public pager: any = {};
  pagedItems: any[];
  constructor(public issueTrackingService:IssueTrackingService, public pagerService:PagerService) {
  }
  ngOnInit() {
    this.getIssueRaisedSystem();
  }

  public getIssueRaisedSystem(){
    dialog.loading();
    this.issueTrackingService.GetAllRaisedSystems().subscribe(res=>{
      this.issueRaisedSystem=res;
      if (this.issueRaisedSystem.length > 0){
        if (this.pager.currentPage == 0) {
          this.setPage(1);
        } else {
          this.setPage(this.pager.currentPage);
        }
      }else{
        this.setPage(1);
      }
      dialog.close();
    }, dialog.error)
  }

  public setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.issueRaisedSystem.length, page);

    //get the paged items
    this.pagedItems = this.issueRaisedSystem.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }
}
