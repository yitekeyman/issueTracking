import {Injectable} from "@angular/core";
import {ApiServices} from "./api.service";

@Injectable()
export class ReportService {

    constructor(public apiService: ApiServices) {
    }

    public IssueStatusIssuePriorityStatistics(dateFrom:any, dateTo:any){
        return this.apiService.get(`Report/StatusIssuePriorityReport?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    }

    public BranchIssuePriorityStatistics(dateFrom:any, dateTo:any){
        return this.apiService.get(`Report/BranchIssuePriorityReport?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    }
    
    public IssueRaisedIssueStatusStatistics(dateFrom:any, dateTo:any){
        return this.apiService.get(`Report/IssueRaiseIssueStatusReport?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    }
    
    public CancelledIssuesListStatistics(dateFrom:any, dateTo:any){
        return this.apiService.get(`Report/CancelledIssuesListStatReport?dateFrom=${dateFrom}&dateTo=${dateTo}`);
    }
}