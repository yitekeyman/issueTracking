import {Component, OnInit} from "@angular/core";
import {ReportService} from "../../../_Services/report.service";
import dialog from "../../../_Shared/dialog";
import swal from "sweetalert2";
import {DatePipe} from "@angular/common";
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
// @ts-ignore

@Component({
    selector:'app-issue-priority-branch-stat',
    templateUrl:'./issuePriorityWithBranch.component.html'
})
//@ts-ignore
export class IssuePriorityWithBranchComponent implements OnInit{
    public dateFrom = null;
    public dateTo = null;
    public reportResult: any = null;

    constructor(public reportService: ReportService) {
    }

    ngOnInit(): void {
        this.dateFrom = null;
        this.dateTo = null;
    }
    hasTotalTitle(): boolean {
        return this.reportResult?.reportList?.some((rep: any) => rep.title === 'Total');
    }
    
    public generateReport() {
        dialog.loading();
        this.reportService.BranchIssuePriorityStatistics(this.dateFrom, this.dateTo).subscribe(res => {
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

    public exportToExcel() {
        const workbook = XLSX.utils.book_new();
        const worksheet = XLSX.utils.table_to_sheet(document.getElementById('print-section-all'));

        // Process the worksheet to format percentage cells
        const range = XLSX.utils.decode_range(worksheet['!ref']);
        for (let rowNum = range.s.r; rowNum <= range.e.r; rowNum++) {
            for (let colNum = range.s.c; colNum <= range.e.c; colNum++) {
                const cellAddress = XLSX.utils.encode_cell({ r: rowNum, c: colNum });
                const cell = worksheet[cellAddress];
                if (cell && cell.v && typeof cell.v === 'string' && cell.v.endsWith('%')) {
                    const percentageValue = parseFloat(cell.v.replace('%', '').trim());
                    if (!isNaN(percentageValue)) {
                        cell.v = percentageValue; // Update cell value to the numeric percentage
                    }
                }
            }
        }

        XLSX.utils.book_append_sheet(workbook, worksheet, 'Report');
        const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        const excelData = new Blob([excelBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        this.saveAsExcelFile(excelBuffer, 'report');
    }
    private saveAsExcelFile(buffer: any, fileName: string) {
        const data: Blob = new Blob([buffer], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8'
        });

        const options = { timeZone: 'Europe/Moscow', hour12: false };
        const dateString = new Date().toLocaleString('en-US', options).replace(/[^\d]/g, '');
        saveAs(data, fileName + '_export_' + dateString + '.xlsx');
    }
}
