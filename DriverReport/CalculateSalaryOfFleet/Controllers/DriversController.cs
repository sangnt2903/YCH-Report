using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CalculateSalaryOfFleet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.Style;

namespace CalculateSalaryOfFleet.Controllers
{
    public class DriversController : Controller
    {
        private readonly FleetsTripsContext _ctx;
        public DriversController(FleetsTripsContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View(_ctx.Drivers.ToList());
        }

        public double CalculateTrip(int numberOfOrders)
        {
            if(numberOfOrders != 0)
            {
                if (numberOfOrders > 20)
                    return 3;
                else if (numberOfOrders > 15 && numberOfOrders <= 20)
                    return 2.5;
                else if (numberOfOrders > 10 && numberOfOrders <= 15)
                    return 2;
                else if (numberOfOrders > 5 && numberOfOrders <= 10)
                    return 1.5;
                else 
                    return 1;
            } else
            {
                return 0;
            }
        }

        public IActionResult GetAllJobs(string driverICNo, int FilterDateMonth, int FilterDateYear)
        {
            var results = from ord in _ctx.Orders
                          join jb in _ctx.Jobs on ord.JobNo equals jb.JobNo
                          where jb.DriverIcno == driverICNo
                          group ord by ord.JobNo into ordJobNo
                          select new JobModelView
                          {
                              JobNo = ordJobNo.Key,
                              NumberOfDropPoint = ordJobNo.Select(p => p.DeliveryCustCode).Distinct().Count(),
                              NumberOfTrips = CalculateTrip(ordJobNo.Select(p => p.DeliveryCustCode).Distinct().Count()),
                          };

            Drivers driver = _ctx.Drivers.SingleOrDefault(p => p.DriverIcno == driverICNo);
            string driverString = driverICNo + "-" + driver.DriverName;
            ViewBag.driver = driverString;
            return View(results);
        }

        public double CalculateTripInTotalJobs(string driverIcNo)
        {
            double totalTrip = 0.0;
            var results = from ord in _ctx.Orders
                          join jb in _ctx.Jobs on ord.JobNo equals jb.JobNo
                          where jb.DriverIcno == driverIcNo
                          group ord by ord.JobNo into ordJobNo
                          select new JobModelView
                          {
                              JobNo = ordJobNo.Key,
                              NumberOfDropPoint = ordJobNo.Select(p => p.DeliveryCustCode).Distinct().Count(),
                              NumberOfTrips = CalculateTrip(ordJobNo.Select(p => p.DeliveryCustCode).Distinct().Count()),
                          };
            totalTrip = results.Select(p => p.NumberOfTrips).Sum();
            return totalTrip;
        }

        public IActionResult Report()
        {
            return View(GetDataReport());
        }

        public List<ResultModelView> GetDataReport()
        {
            var D1 = from dr in _ctx.Drivers
                     join jb in _ctx.Jobs on dr.DriverIcno equals jb.DriverIcno
                     select new D1
                     {
                         DriverIcNo = dr.DriverIcno,
                         JobNo = jb.JobNo,
                         DriverName = dr.DriverName
                     };

            var D2 = from jb in _ctx.Jobs
                     join ord in _ctx.Orders on jb.JobNo equals ord.JobNo
                     group ord by ord.JobNo into JobOrdGroup
                     select new D2
                     {
                         JobNo = JobOrdGroup.Key,
                         NumberOfDropPointOnJob = JobOrdGroup.Select(p => p.DeliveryCustCode).Distinct().Count()
                     };

            var D12 = from l1 in D1
                      join l2 in D2 on l1.JobNo equals l2.JobNo
                      select new D12
                      {
                          DriverIcNo = l1.DriverIcNo,
                          DriverName = l1.DriverName,
                          JobNo = l1.JobNo,
                          NumberOfDropPointOnJob = l2.NumberOfDropPointOnJob
                      };



            var results = from l12 in D12
                          group l12 by new { l12.DriverIcNo, l12.DriverName } into driverJobsGroup
                          select new ResultModelView
                          {
                              DriverIcNo = driverJobsGroup.Key.DriverIcNo,
                              DriverName = driverJobsGroup.Key.DriverName,
                              TotalJobsInMonth = driverJobsGroup.Select(p => p.JobNo).Count(),
                              TotalDropPointOnTotalJobs = driverJobsGroup.Select(p => p.NumberOfDropPointOnJob).Sum(),
                              TotalTrip = 0
                          };

            List<ResultModelView> res = results.ToList();

            for (int i = 0; i < res.Count(); i++)
            {
                res[i].TotalTrip = CalculateTripInTotalJobs(res[i].DriverIcNo);
            }

            return res;
        }

        public IActionResult ExportReportToExcel()
        {
            List<ResultModelView> dataReport = GetDataReport();
            //xuất ra excel dùng eplus
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("TripsReport");

                //custome size
                worksheet.Row(4).Height = 20;
                worksheet.Column(1).Width = 10;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;
                worksheet.Column(5).Width = 15;
                worksheet.Column(6).Width = 15;

                //custom text
                worksheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //custom color
                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#108f14");
                for (int i = 1; i <= 6; i++)
                {
                    worksheet.Cells[4, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[4, i].Style.Fill.BackgroundColor.SetColor(colFromHex);
                }

                //custom format
                worksheet.Row(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(4).Style.Font.Bold = true;

                worksheet.Cells[4, 1].Value = "STT";
                worksheet.Cells[4, 2].Value = "Số điện thoại";
                worksheet.Cells[4, 3].Value = "Tên tài xế";
                worksheet.Cells[4, 4].Value = "Tổng số Jobs";
                worksheet.Cells[4, 5].Value = "Tổng số điểm giao/ Tổng số Jobs";
                worksheet.Cells[4, 6].Value = "Tổng Trip";

                //body of table  
                //  
                int recordindex = 5;
                int idx = 1;
                foreach (var data in dataReport)
                {
                    worksheet.Cells[recordindex, 1].Value = idx;
                    worksheet.Cells[recordindex, 2].Value = data.DriverIcNo;
                    worksheet.Cells[recordindex, 3].Value = data.DriverName;
                    worksheet.Cells[recordindex, 4].Value = data.TotalJobsInMonth;
                    worksheet.Cells[recordindex, 5].Value = data.TotalDropPointOnTotalJobs;
                    worksheet.Cells[recordindex, 6].Value = data.TotalTrip;

                    recordindex++;
                    idx++;
                }

                package.Save();
            }
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DriversReport.xlsx");
        }
    }
}   