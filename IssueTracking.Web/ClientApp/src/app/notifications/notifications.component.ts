import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {IssueTrackingService} from "../_Services/IssueTrackingService";
import dialog from "../_shared/dialog";
import swal from "sweetalert2";
import {PagerService} from "../_Services/pager.service";

@Component({
  selector: 'app-notification',
  templateUrl: './notifications.component.html'
})

export class NotificationsComponent implements OnInit {
  public notifications = null;
  public pager: any = {};
  pagedItems: any[];
  public status = false;

  constructor(public router: Router, public issueTrackingService: IssueTrackingService, public pagerService: PagerService) {
    //this.pager = localStorage.getItem('routerLink');
  }

  ngOnInit() {
    this.getNotifications(this.status);
  }

  public getNotifications(status) {
    dialog.loading();
    this.issueTrackingService.GetNotification(status).subscribe(res => {
      // this.totalUnRead = res.unreadNotification;
      // this.totalRead = res.readNotification;
      // this.notifications = res.notifications;
      //this.notifications=null;
      this.notifications = res;
      if (this.notifications.notifications.length > 0) {
        this.setPage(1);
      }
      dialog.close();
    }, e => {
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }

  public setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.notifications.notifications.length, page);

    //get the paged items
    this.pagedItems = this.notifications.notifications.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public showNotDetails(notId: string, issueId: any, status: boolean) {
    dialog.loading();
    if (!status)
      this.issueTrackingService.MarkReadNotification(notId).subscribe();
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['LIT/issues/view-issue/', issueId]);
    });

  }

  public showPage(status: boolean) {
    this.status = status;
    this.getNotifications(this.status);
  }
}

