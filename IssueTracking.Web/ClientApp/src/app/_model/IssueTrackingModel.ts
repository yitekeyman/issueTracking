export interface LoginUser {
  username: string;
  password: string;
}

export interface IssueTypeModel{
  id:number;
  name:string;
  description:string;
  raisedSystemId:number;
}

export interface IssueRaisedSystemModel{
  id:number;
  name:string;
  description:string;
}

export interface ResourceModel{
  docRef:string;
  data:string;
  mimeType:string;
  index:number;
}
export interface BasicSolutionModel{
  id:number;
  issueTypeId:number;
  solutionQuery:string;
  solutionDescription:string;
  solutionResource:ResourceModel[];
}
