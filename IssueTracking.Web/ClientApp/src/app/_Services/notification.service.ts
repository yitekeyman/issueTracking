import {Injectable} from "@angular/core";
import {ApiServices} from "./api.service";

@Injectable()
export class NotificationService {
    constructor(public apiService:ApiServices){}
    
    public changeNotificationState(id:string){
        return this.apiService.post(`Notification/ChangeNotificationState?id=${id}`, null);
    }
    
    public getUnreadNotification(){
        return this.apiService.get('Notification/GetUnreadNotification');
    }

    public getReadNotification(){
        return this.apiService.get('Notification/GetReadNotification');
    }
}