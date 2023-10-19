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

export interface DepartmentSchemaModel {
  Id: string;
  branchId: number;
  departmentId: number;
  branchName: string;
  departmentName: string;
}

export interface EmployeeModel {
  id: string;
  appellation: string;
  firstName: string;
  fatherName: string;
  grFatherName: string;
  empIdNo: string;
  title: string;
  position: string;
  phone: string;
  email: string;
  username: string;
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
  id: string;
  issueTitle: string;
  issueTypeId: number;
  otherIssue: string;
  policyNo: string;
  issueDescription: string;
  issuePriority: number;
  issueResource: ResourceModel[];

}

export interface IssueListRetModel {
  id: number;
  issueTitle: string;
  issueTypeId: number | null;
  otherIssue: string;
  policyNo: string;
  issueDescription: string;
  branchId: DepartmentSchemaModel;
  issueRequestedBy: EmployeeModel;
  issueRequestedDate: any;
  issueRespondBy: EmployeeModel;
  issueRespondDate: any;
  issueClosedBy: EmployeeModel;
  issueClosedDate: any;
  issuePriority: number;
  issueStatus: number;
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

export interface IssueListReturnModel {
  opened: any[];
  closed: any[];
  upperSideBarStats: any[];
  lowerSideBarStats: any[];
}
