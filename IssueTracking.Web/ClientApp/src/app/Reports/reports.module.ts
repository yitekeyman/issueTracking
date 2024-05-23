import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {NgxPrintModule} from "ngx-print";
import {ReportsComponent} from "./reports.component";
import {RouterModule} from "@angular/router";
import {IssuePriorityWithStatusComponent} from "./statisticalReport/issuePriorityWithStatus/issuePriorityWithStatus.component";
import {
    IssuePriorityWithBranchComponent
} from "./statisticalReport/issuePriorityWithBranch/issuePriorityWithBranch.component";
import {
    IssueRaisedWithStatusComponent
} from "./statisticalReport/issueRaisedWithStatus/issueRaisedWithStatus.component";
import {CancelledIssuesListComponent} from "./cancelledReport/cancelledIssuesList/cancelledIssuesList.component";

@NgModule({
    declarations: [
        ReportsComponent,
        IssuePriorityWithStatusComponent,
        IssuePriorityWithBranchComponent,
        IssueRaisedWithStatusComponent,
        CancelledIssuesListComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgxPrintModule,
        RouterModule
    ],
    exports: []
})
export class ReportsModule {

}
