#pragma checksum "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bf876e5e2af039801754aa3317d9dea542d859fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Drivers_GetAllJobs), @"mvc.1.0.view", @"/Views/Drivers/GetAllJobs.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Drivers/GetAllJobs.cshtml", typeof(AspNetCore.Views_Drivers_GetAllJobs))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\_ViewImports.cshtml"
using CalculateSalaryOfFleet;

#line default
#line hidden
#line 2 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\_ViewImports.cshtml"
using CalculateSalaryOfFleet.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf876e5e2af039801754aa3317d9dea542d859fa", @"/Views/Drivers/GetAllJobs.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"10124c27bed95f0672c9b1204070b69b0b35ba99", @"/Views/_ViewImports.cshtml")]
    public class Views_Drivers_GetAllJobs : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CalculateSalaryOfFleet.Models.JobModelView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(64, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
  
    Layout = "~/Views/Shared/_frontEnd.cshtml";
    ViewData["Title"] = "Danh sách JOBS";

#line default
#line hidden
            BeginContext(166, 86, true);
            WriteLiteral("    \r\n<div style=\"text-align:center\"><h2>Danh sách Jobs của tài xế </h2></div>\r\n\r\n");
            EndContext();
#line 10 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
   
    FleetsTripsContext _ctx = new FleetsTripsContext();
    var sumOfNumberDropPoint = 0;
    var sumOfNumberTrip = 0.0;
    foreach (var i in Model)
    {
        sumOfNumberDropPoint += i.NumberOfDropPoint;
        sumOfNumberTrip += i.NumberOfTrips;
    }
    var driverId = ViewBag.driver.Split("-")[0];
    var driverName = ViewBag.driver.Split("-")[1];

#line default
#line hidden
            BeginContext(629, 593, true);
            WriteLiteral(@"<div class=""row"">
    <table style=""width: 70%;text-align: center; margin: auto; margin-bottom: 10px;"" class=""table table-bordered table-responsive"">
        <thead>
            <tr style=""text-align: center"">
                <th style=""text-align: center"">Mã tài xế</th>
                <th style=""text-align: center"">Tên tài xế</th>
                <th style=""text-align: center"">Tổng số drop point trong tháng</th>
                <th style=""text-align: center"">Tổng số trip</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>");
            EndContext();
            BeginContext(1223, 8, false);
#line 34 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
               Write(driverId);

#line default
#line hidden
            EndContext();
            BeginContext(1231, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1259, 10, false);
#line 35 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
               Write(driverName);

#line default
#line hidden
            EndContext();
            BeginContext(1269, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1297, 20, false);
#line 36 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
               Write(sumOfNumberDropPoint);

#line default
#line hidden
            EndContext();
            BeginContext(1317, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(1345, 15, false);
#line 37 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
               Write(sumOfNumberTrip);

#line default
#line hidden
            EndContext();
            BeginContext(1360, 68, true);
            WriteLiteral("</td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n</div>\r\n\r\n");
            EndContext();
#line 43 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
 if (Model.Count() > 0)
{

#line default
#line hidden
            BeginContext(1456, 408, true);
            WriteLiteral(@"    <div>
        <table class=""table table-responsive table-bordered"">
            <thead class=""thead-dark"">
                <tr>
                    <th scope=""col"">#</th>
                    <th scope=""col"">Mã chuyến</th>
                    <th scope=""col"">Số drop point</th>
                    <th scope=""col"">Số trip</th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 56 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
                  
                    var i = 1;
                    foreach (var item in Model)
                    {

#line default
#line hidden
            BeginContext(1988, 74, true);
            WriteLiteral("                        <tr>\r\n                            <th scope=\"row\">");
            EndContext();
            BeginContext(2063, 1, false);
#line 61 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
                                       Write(i);

#line default
#line hidden
            EndContext();
            BeginContext(2064, 39, true);
            WriteLiteral("</th>\r\n                            <td>");
            EndContext();
            BeginContext(2104, 10, false);
#line 62 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
                           Write(item.JobNo);

#line default
#line hidden
            EndContext();
            BeginContext(2114, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(2154, 22, false);
#line 63 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
                           Write(item.NumberOfDropPoint);

#line default
#line hidden
            EndContext();
            BeginContext(2176, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(2216, 18, false);
#line 64 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
                           Write(item.NumberOfTrips);

#line default
#line hidden
            EndContext();
            BeginContext(2234, 38, true);
            WriteLiteral("</td>\r\n                        </tr>\r\n");
            EndContext();
#line 66 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
                        i++;
                    }
                

#line default
#line hidden
            BeginContext(2344, 52, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n");
            EndContext();
#line 72 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(2408, 392, true);
            WriteLiteral(@"    <table class=""table table-responsive table-bordered"">
        <thead class=""thead-dark"">
            <tr>
                <th scope=""col"">#</th>
                <th scope=""col"">Mã chuyến</th>
                <th scope=""col"">Số drop point</th>
                <th scope=""col"">Số trip</th>
            </tr>
        </thead>
        <tbody>   
        </tbody>
    </table>
");
            EndContext();
#line 87 "C:\Users\ADMIN\Desktop\CalculateSalaryFleet\CalculateSalaryOfFleet\CalculateSalaryOfFleet\Views\Drivers\GetAllJobs.cshtml"
}

#line default
#line hidden
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CalculateSalaryOfFleet.Models.JobModelView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
