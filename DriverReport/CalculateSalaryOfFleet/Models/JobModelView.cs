using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateSalaryOfFleet.Models
{
    public class JobModelView
    {
        public string DriverICNo { get; set; }
        public string JobNo { get; set; }
        public DateTime? ATDComleteDate { get; set; }
        public int NumberOfDropPoint { get; set; }
        public double NumberOfTrips { get; set; }
    }
}
