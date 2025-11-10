using CsvHelper;
using CsvHelper.Configuration;
using ReportSupportTool.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReportSupportTool.Services
{
    public class CsvDataService
    {
        private readonly string filePath;

        public CsvDataService()
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "jira_sample.csv");
        }

        public List<IssueModel> GetRawCsvData()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null,
                TrimOptions = TrimOptions.Trim
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<IssueModelMap>();
                var records = csv.GetRecords<IssueModel>().ToList();
                return records;
            }
        }

        public List<MemberStatusModel> GetMemberStatusData(List<IssueModel> records)
        {
            // Nhóm dữ liệu theo Assignee và tính tổng trạng thái
            var result = records
                .GroupBy(r => r.Assignee)
                .Select(g => new MemberStatusModel
                {
                    MemberName = g.Key,
                    HoldingCount = g.Count(r => r.Status == "Holding"),
                    ToDoCount = g.Count(r => r.Status == "To Do"),
                    InProgressCount = g.Count(r => r.Status.Contains("In Progres")), 
                    DoneCount = g.Count(r => r.Status == "Resolved" || r.Status == "Closed" || r.Status == "Done")
                })
                .ToList();

            return result;
        }
    }
}
