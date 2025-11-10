using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSupportTool.Models
{
    public class MemberStatusModel
    {
        public string MemberName { get; set; }
        public int HoldingCount { get; set; }
        public int ToDoCount { get; set; }
        public int InProgressCount { get; set; }
        public int DoneCount { get; set; }

        public int TotalCount => HoldingCount + ToDoCount + InProgressCount + DoneCount;
    }
}
