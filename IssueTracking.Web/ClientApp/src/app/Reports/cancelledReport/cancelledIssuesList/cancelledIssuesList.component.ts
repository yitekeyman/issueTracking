import {Component, OnInit} from "@angular/core";
import {ReportService} from "../../../_Services/report.service";
import dialog from "../../../_Shared/dialog";
import swal from "sweetalert2";
import {DatePipe} from "@angular/common";
// @ts-ignore

@Component({
    selector:'app-cancelled-issues-stat',
    templateUrl:'./cancelledIssuesList.component.html'
})
//@ts-ignore
export class CancelledIssuesListComponent implements OnInit{
    public dateFrom = null;
    public dateTo = null;
    public reportResult: any = null;

    constructor(public reportService: ReportService) {
    }

    ngOnInit(): void {
        this.dateFrom = null;
        this.dateTo = null;
    }
    public generateReport() {
        dialog.loading();
        this.reportService.CancelledIssuesListStatistics(this.dateFrom, this.dateTo).subscribe(res => {
            this.reportResult = res;
            dialog.close();
        }, e => {
            swal({
                type: 'error', title: 'Oops...', text: e.message
            });
        })
    }

    public convertDate(date: any) {
        let dateTimePipe = new DatePipe("en-US");
        return dateTimePipe.transform(date, 'MMM-dd-yyyy');
    }
}
