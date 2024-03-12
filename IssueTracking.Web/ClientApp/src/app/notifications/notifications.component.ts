import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {IssueTrackingService} from "../_Services/IssueTrackingService";
import dialog from "../_shared/dialog";
import swal from "sweetalert2";
import {PagerService} from "../_Services/pager.service";

@Component({
  selector:'app-notification',
  templateUrl:'./notifications.component.html'
})

export class NotificationsComponent implements OnInit{
  public notifications=[];
  public pager: any = {};
  pagedItems: any[];
  constructor(public router:Router, public issueTrackingService:IssueTrackingService, public pagerService:PagerService) {
    this.pager=localStorage.getItem('routerLink');
  }
  ngOnInit() {
    this.getNotifications();
  }

  public getNotifications(){
    dialog.loading();
    this.issueTrackingService.GetNotification().subscribe(res=>{
      this.notifications=res.notifications;
      if(this.notifications.length>0){
        this.setPage(1);
      }
      dialog.close();
    }, e=>{
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }

  public setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.notifications.length, page);

    //get the paged items
    this.pagedItems = this.notifications.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public showNotDetails(notId:string, issueId:any){
    dialog.loading();
    this.issueTrackingService.MarkReadNotification(notId);
    this.router.navigate(['/LIT/issues/view-issue/', issueId]);
  }
}

