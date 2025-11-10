using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSupportTool.Models
{
    public class IssueModel
    {
        public string Assignee { get; set; }
        public string Status { get; set; }
        public string Summary { get; set; } // task's title
        public string Sprint { get; set; }
        public string IssueType { get; set; }
        public double StoryPoints { get; set; }
    }

    public class  IssueModelMap : ClassMap<IssueModel>
    {
        public IssueModelMap()
        {
            Map(m => m.Assignee).Name("Assignee");
            Map(m => m.Status).Name("Status");
            Map(m => m.Summary).Name("Summary");
            Map(m => m.Sprint).Name("Sprint");
            Map(m => m.IssueType).Name("Issue Type");
            Map(m => m.StoryPoints).Name("Custom field (Story Points)");
        }
    }
}
