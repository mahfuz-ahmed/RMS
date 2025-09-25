using AspNetCore.Reporting;
using FinancialManagementSystem.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FinancialManagementSystem.UI.Controllers
{
    public class COAReportController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISender _sender;

        public COAReportController(IWebHostEnvironment env, ISender sender)
        {
            _env = env;
            _sender = sender;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public async Task<IActionResult> ChartOfAccountReport()
        {
            string rdlcFilePath = Path.Combine(_env.ContentRootPath, "Reports", "ChartOfAccount.rdlc");
            if (!System.IO.File.Exists(rdlcFilePath))
                throw new FileNotFoundException("Report file not found: " + rdlcFilePath);

            var result = await _sender.Send(new ChartOfAccountGetAllDataQuery());

            if (result.IsError)
            {
                return BadRequest(result.FirstError.Description);
            }

            var accounts = result.Value; 

            DataTable dt = new DataTable("dbChartOfAccount");
            dt.Columns.Add("AccountCode", typeof(string));
            dt.Columns.Add("AccountName", typeof(string));
            dt.Columns.Add("AccountType", typeof(string));
            dt.Columns.Add("IsActive", typeof(bool));
            dt.Columns.Add("ParentAccountName", typeof(string));

            foreach (var acc in accounts)
            {
                dt.Rows.Add(acc.AccountCode, acc.AccountName, acc.AccountType, acc.IsActive,acc.ParentAccountName);
            }

            var localReport = new LocalReport(rdlcFilePath);
            localReport.AddDataSource("dbChartOfAccount", dt);

            var pdfResult = localReport.Execute(RenderType.Pdf, 1, null, null);

            return File(pdfResult.MainStream, "application/pdf", "ChartOfAccountReport.pdf");
        }
    }
}
