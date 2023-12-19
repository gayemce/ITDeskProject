export class TicketModel{
    id: string = "";
    subject: string = "";
    appUser: any;
    userName: string = "";
    createdDate: Date = new Date();
    isOpen: boolean = false;
}