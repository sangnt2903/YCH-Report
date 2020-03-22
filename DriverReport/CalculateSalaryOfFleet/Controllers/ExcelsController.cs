using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CalculateSalaryOfFleet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeOpenXml;

namespace CalculateSalaryOfFleet.Controllers
{
    public class ExcelsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly FleetsTripsContext _ctx;
        public ExcelsController(IHostingEnvironment hostingEnvironment, FleetsTripsContext ctx)
        {
            _hostingEnvironment = hostingEnvironment;
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View(_ctx.Excels.ToList());
        }

        [HttpPost("Excels/UpLoadExcel")]
        public IActionResult UpLoadExcel(IFormFile fExcel)
        {
            Excels fileToImport = new Excels
            {
                ExcelUploadedDate = DateTime.Now
            };

            if ( fExcel != null && Path.GetExtension(fExcel.FileName).ToLower()!= ".xls" )
            {
                ViewBag.Error = null;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Excels", fExcel.FileName);
                using (var file = new FileStream(path, FileMode.Create))
                {
                    fExcel.CopyTo(file);
                }
                fileToImport.ExcelFileName = fExcel.FileName;

                _ctx.Add(fileToImport);
                _ctx.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Vui lòng chọn file excel hoặc định dạng file của bạn không được hỗ trợ. Lưu ý những file được hỗ trợ bao gồm : .xlsx, .csv ";
                return View("Index", _ctx.Excels.ToList());
            }

            return RedirectToAction("Index");
        }

        public void ResetDatabase()
        {
            var orders = _ctx.Orders.ToList();
            var jobs = _ctx.Jobs.ToList();
            var drivers = _ctx.Drivers.ToList();
            var trucks = _ctx.Trucks.ToList();
            var deliveries = _ctx.DeliveryCustomers.ToList();

            _ctx.Orders.RemoveRange(orders);
            _ctx.SaveChanges();
            _ctx.Jobs.RemoveRange(jobs);
            _ctx.SaveChanges();
            _ctx.Drivers.RemoveRange(drivers);
            _ctx.SaveChanges();
            _ctx.Trucks.RemoveRange(trucks);
            _ctx.SaveChanges();
            _ctx.DeliveryCustomers.RemoveRange(deliveries);
            _ctx.SaveChanges();
        }

        public IActionResult ImportData(int excelCode)
        {
            ResetDatabase();

            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = _ctx.Excels.SingleOrDefault(p=> p.ExcelCode == excelCode).ExcelFileName;
            FileInfo file = new FileInfo(Path.Combine(rootFolder, "Excels", @fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["DynamicReport"];
                if(workSheet != null)
                {
                    int totalRows = workSheet.Dimension.Rows;
                    // Tạo list chứa data để phân tích data thô rồi lưu
                    List<Trucks> trucks = new List<Trucks>();
                    List<Drivers> drivers = new List<Drivers>();
                    List<Jobs> jobs = new List<Jobs>();
                    List<Orders> orders = new List<Orders>();
                    List<DeliveryCustomers> deliveries = new List<DeliveryCustomers>();

                    for (int i = 6; i <= totalRows; i++)
                    {
                        if (workSheet.Cells[i, 40].Value != null && workSheet.Cells[i, 51].Value != null && trucks.SingleOrDefault(p => p.TruckId == workSheet.Cells[i, 40].Value.ToString()) == null)
                        {
                            trucks.Add(new Trucks
                            {
                                TruckId = workSheet.Cells[i, 40].Value.ToString(),
                                TruckType = workSheet.Cells[i, 43].Value.ToString()
                            });
                        }

                        var driverIcNo = workSheet.Cells[i, 51].Value;
                        if (driverIcNo != null && drivers.SingleOrDefault(p => p.DriverIcno == driverIcNo.ToString()) == null)
                        {
                            drivers.Add(new Drivers
                            {
                                DriverIcno = workSheet.Cells[i, 51].Value.ToString(),
                                DriverName = workSheet.Cells[i, 50].Value.ToString(),
                                DriverPhone = workSheet.Cells[i, 51].Value.ToString(),
                                TruckId = workSheet.Cells[i, 40].Value.ToString()
                            });
                        }

                        if (workSheet.Cells[i, 51].Value != null && jobs.SingleOrDefault(p => p.JobNo == workSheet.Cells[i, 41].Value.ToString()) == null)
                        {
                            jobs.Add(new Jobs
                            {
                                JobNo = workSheet.Cells[i, 41].Value.ToString(),
                                DriverIcno = workSheet.Cells[i, 51].Value.ToString()
                            });
                        }

                        string dataString = workSheet.Cells[i, 46].Value != null ? workSheet.Cells[i, 46].Value.ToString() : DateTime.Now.ToString("dd-MM-yyyy");
                        string dateString = dataString.Split('-')[0] + "/" + dataString.Split('-')[1] + "/" + dataString.Split('-')[2];
                        if (workSheet.Cells[i, 51].Value != null)
                        {
                            orders.Add(new Orders
                            {
                                OrderNo = workSheet.Cells[i, 1].Value.ToString(),
                                JobNo = workSheet.Cells[i, 41].Value.ToString(),
                                TranportAgent = workSheet.Cells[i, 45].Value.ToString(),
                                DeliveryCustCode = workSheet.Cells[i, 14].Value.ToString(),
                                AtdcompleteDate = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                            });
                        }

                        if (deliveries.SingleOrDefault(p => p.DeliveryCustCode == workSheet.Cells[i, 14].Value.ToString()) == null)
                        {
                            deliveries.Add(new DeliveryCustomers
                            {
                                DeliveryCustCode = workSheet.Cells[i, 14].Value.ToString(),
                                DeliveryAddress = workSheet.Cells[i, 17].Value.ToString(),
                                ServiceLevel = workSheet.Cells[i, 24].Value.ToString()
                            });
                        }
                    }
                    
                    _ctx.Trucks.AddRange(trucks);
                    _ctx.SaveChanges();
                    _ctx.Drivers.AddRange(drivers);
                    _ctx.SaveChanges();
                    _ctx.Jobs.AddRange(jobs);
                    _ctx.SaveChanges();
                    _ctx.DeliveryCustomers.AddRange(deliveries);
                    _ctx.SaveChanges();
                    _ctx.Orders.AddRange(orders);
                    _ctx.SaveChanges();

                    ViewBag.InfoImportData = "Import Data từ file " + fileName + " thành công";
                    return RedirectToAction("Report", "Drivers");
                } else
                {
                    ViewBag.Error = "Không tìm thấy sheet cần thiết của hệ thống để import dữ liệu! Vui lòng kiểm tra tên của Sheet theo yêu cầu của hệ thống !";
                    return View("Index", _ctx.Excels.ToList());
                }
            }
                
        }
    }
}