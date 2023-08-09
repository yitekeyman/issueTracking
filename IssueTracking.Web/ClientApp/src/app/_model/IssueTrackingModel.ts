export interface LoginUser {
  username: string;
  password: string;
}

export interface IssueTypeModel {
  id: number;
  name: string;
  description: string;
  raisedSystemId: number;
}

export interface IssueRaisedSystemModel {
  id: number;
  name: string;
  description: string;
}

export interface ResourceModel {
  docRef: string;
  fileName: string;
  data: string;
  mimeType: string;
  index: number;
}

export interface BasicSolutionModel {
  id: number;
  issueTypeId: number;
  solutionQuery: string;
  solutionDescription: string;
  solutionResource: ResourceModel[];
}

export interface IssueListModel {
  id: number;
  issueTitle: string
  issueTypeId: number;
  otherIssue: string;
  policyNo: string[];
  branchId: number;
  issueDescription: string;
  issue_respond_by: string;
  issue_respond_date: bigint;
  issue_closed_by: string;
  issue_closed_date: bigint;
  issue_requested_by: string;
  issue_requested_date: bigint;
  issuePriority: number;
  issueStatus: string;
  ticket: string;
  issueResource: ResourceModel[];
}

export interface IssueFilterParameter {
  priority: number;
  raisedSystem: number;
  issueType: number;
  sort: number;
  branch: string;
  userId: string;
}

export interface IssueListReturnModel{
  opened:any[];
  closed:any[];
  upperSideBarStats:any[];
  lowerSideBarStats:any[];
}
