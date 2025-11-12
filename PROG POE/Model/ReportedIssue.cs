using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{

    public class ReportedIssue
    {
        private static int _referenceCounter = 1000;

        public ReportedIssue()
        {
            ReferenceNumber = "REF-" + _referenceCounter++;
            ReportDate = DateTime.Now;
            Status = "Submitted";
        }

        public string ReferenceNumber { get; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime ReportDate { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"{ReferenceNumber}: {Category} issue at {Location}";
        }
    }
}


