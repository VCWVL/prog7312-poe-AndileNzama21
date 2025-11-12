using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{


    public class CommunityIssue
    {
        public string ReferenceNumber { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; }
        public int SupportCount { get; set; }
        public string Status { get; set; }
        public bool IsSupportedByCurrentUser { get; set; }

        public CommunityIssue()
        {
            ReportDate = DateTime.Now;
            SupportCount = 0;
            Status = "Reported";
        }
    }
}


