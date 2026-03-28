import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {IssueTrackingService} from "../../../_Services/IssueTrackingService";
import dialog from "../../../_shared/dialog";
import swal from "sweetalert2";

@Component({
  selector: "app_showUNSCRConsolidation",
  templateUrl: "./showUNSCRConsolidation.component.html",
})
export class ShowUNSCRConsolidationComponent implements OnInit {
  @Input() public selectedId:number;
  public results:any=null;
  @Output() public closeModal = new EventEmitter();
  constructor(public issueTrackingService:IssueTrackingService) {
  }
  ngOnInit() {
    this.getUNConsolidation();
  }

  public getUNConsolidation() {
    dialog.loading();
    this.results=null;
    this.issueTrackingService.getUnscrConsolidation(this.selectedId).subscribe(res => {

      this.results = res;
      dialog.close();
    }, e => {
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }
  public close() {
    let btn = document.getElementById('close');
    btn.click();
    this.closeModal.next(true);

  }
}
