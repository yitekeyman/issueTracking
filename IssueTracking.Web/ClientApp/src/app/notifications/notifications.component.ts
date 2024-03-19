import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {IssueTrackingService} from "../_Services/IssueTrackingService";
import dialog from "../_shared/dialog";
import swal from "sweetalert2";
import {PagerService} from "../_Services/pager.service";
import {FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PatchAction} from "../_model/IssueTrackingModel";

@Component({
  selector: 'app-notification',
  templateUrl: './notifications.component.html'
})

export class NotificationsComponent implements OnInit {
  public notifications = null;
  public pager: any = {};
  pagedItems: any[];
  public status: boolean;
  public selectedNotificationLists: PatchAction;
  public memberGroup: FormGroup;
  public isMasterSel: boolean;
  public haveSelected: boolean;
  public memberForm: FormGroup;

  constructor(public fb: FormBuilder, public router: Router, public issueTrackingService: IssueTrackingService, public pagerService: PagerService) {

    this.memberGroup = this.fb.group({
      checkUncheckAll: [''],
      listOfMem: this.fb.array([], [Validators.required])
    });
    this.isMasterSel = false;
    this.haveSelected = false;
    this.status = false;

    this.selectedNotificationLists = {
      caseList: [],
      remark: "Notification Mark Read by Patch Action"
    }
  }

  get memberArray() {
    return this.memberGroup.get('listOfMem') as FormArray;
  }

  ngOnInit() {
    this.getNotifications(this.status);
  }

  public getNotifications(status) {
    dialog.loading();
    this.selectedNotificationLists.caseList = [];
    this.isMasterSel=false;
    this.haveSelected=false;
    this.issueTrackingService.GetNotification(status).subscribe(res => {
      // this.totalUnRead = res.unreadNotification;
      // this.totalRead = res.readNotification;
      // this.notifications = res.notifications;
      //this.notifications=null;
      this.notifications = res;
      for (const z of res.notifications) {
        this.memberForm = this.fb.group({memberId: ['']});
        this.memberArray.push(this.memberForm);
      }
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
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.router.navigate(['LIT/issues/view-issue/', issueId]);
    });

  }

  public showPage(status: boolean) {
    this.status = status;
    this.getNotifications(this.status);
  }

  checkUncheckAll() {
    for (let i = 0; i < this.memberArray.length; i++) {
      if (this.isMasterSel) {
        const m = this.memberGroup.controls.listOfMem as FormArray;
        m.controls[i].get("memberId").setValue(true);
        this.haveSelected = true;
      } else {
        const m = this.memberGroup.controls.listOfMem as FormArray;
        m.controls[i].get("memberId").setValue(false);
        this.haveSelected = false;
      }
    }

  }

  isAllSelected() {
    let isSelected = true;
    let hasSelected = true;
    for (let i = 0; i < this.memberArray.length; i++) {
      const m = this.memberGroup.controls.listOfMem as FormArray;

      if (m.controls[i].get("memberId").value == false) {
        isSelected = false;
      } else {
        hasSelected = false;
      }
    }
    if (hasSelected) {
      this.haveSelected = false;
    } else {
      this.haveSelected = true
    }
    if (isSelected) {
      this.isMasterSel = true;
      this.checkUncheckAll();
    } else {
      this.memberGroup.get("checkUncheckAll").setValue(false);
    }
  }

  public patchMarkRead() {
    this.buildSelectedData();
    swal({
      title: 'Are you sure?',
      text: "You want to Mark as Read ",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Mark as Read!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.PatchMakeReadNotification(this.selectedNotificationLists).subscribe(res => {
          swal("Success!", res, "success").then(value => {
            this.getNotifications(this.status);
            dialog.close();
          })

        }, e => {
          swal({
            type: 'error',
            title: 'Oops...',
            text: e.message
          });

        });
      }
    })
  }

  public buildSelectedData() {
    dialog.loading();
    let issues = this.memberGroup.value;
    this.selectedNotificationLists.caseList = issues.listOfMem.map((mem, i) => mem.memberId === true ? this.notifications.notifications[i].id : null).filter(a => a !== null);
    dialog.close();
  }

  public mark_as_Read(id:any){
    dialog.loading();
    this.issueTrackingService.MarkReadNotification(id).subscribe(res => {
      swal("Success!", "You have successfully mark as read", "success").then(value => {
        this.getNotifications(this.status);
        dialog.close();
      })

    }, e => {
      swal({
        type: 'error',
        title: 'Oops...',
        text: e.message
      });

    });
  }
}

