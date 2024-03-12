import {Component, OnInit} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
import dialog from "../../components/dialog";

@Component({
  selector:'app-labels',
  templateUrl:'./labels.component.html'
})
export class LabelsComponent implements OnInit{
  public labelList=[];
  public pager: any = {};
  pagedItems: any[];

  constructor(public issueTrackingService:IssueTrackingService, public pagerService:PagerService) {
  }
  ngOnInit() {
    this.getLabelList();
  }

  public getLabelList(){
    this.setPage(1);
  }

  public setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.labelList.length, page);

    //get the paged items
    this.pagedItems = this.labelList.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }
}
