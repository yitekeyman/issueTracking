import {Component, OnInit} from "@angular/core";
import {ReportService} from "../_Services/report.service";

@Component({
    selector: 'app-reports',
    templateUrl: './reports.component.html'
})
export class ReportsComponent implements OnInit {
    public dateFrom = null;
    public dateTo = null;
    public stat_one = false;
    public stat_two = false;
    public stat_three = false;
    public stat_four = false;


    constructor(public reportService: ReportService) {
    }

    ngOnInit(): void {
    }

    openReport(report: number) {
        this.offAllReport();
        if (report == 1) {
            this.stat_one = true;
        }
        if (report == 2) {
            this.stat_two = true;
        }
        if (report == 3) {
            this.stat_three = true;
        }
        if (report == 4) {
            this.stat_four = true;
        }
    }

    offAllReport() {
        this.stat_one = false;
        this.stat_two = false;
        this.stat_three = false;
        this.stat_four = false;
    }

}
