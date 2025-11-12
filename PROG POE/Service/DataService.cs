using PROG_POE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Service
{
    public class DataService : IDataService
    {
        private readonly List<ReportedIssue> _reportedIssues;
        private readonly List<CommunityIssue> _communityIssues;

        public DataService()
        {
            _reportedIssues = new List<ReportedIssue>();
            _communityIssues = new List<CommunityIssue>();
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _communityIssues.Add(new CommunityIssue
            {
                ReferenceNumber = "REF-1001",
                Location = "Main Street",
                Category = "Roads",
                Description = "Pothole near the intersection causing traffic issues",
                ReportDate = System.DateTime.Now.AddDays(-2),
                SupportCount = 5,
                Status = "In Progress"
            });

            _communityIssues.Add(new CommunityIssue
            {
                ReferenceNumber = "REF-1002",
                Location = "City Park",
                Category = "Parks and Recreation",
                Description = "Broken playground equipment needs repair",
                ReportDate = System.DateTime.Now.AddDays(-5),
                SupportCount = 12,
                Status = "Reported"
            });
        }

        public void AddIssue(ReportedIssue issue)
        {
            _reportedIssues.Add(issue);
            _communityIssues.Add(new CommunityIssue
            {
                ReferenceNumber = issue.ReferenceNumber,
                Location = issue.Location,
                Category = issue.Category,
                Description = issue.Description,
                ReportDate = issue.ReportDate,
                SupportCount = 0,
                Status = issue.Status
            });
        }

        public List<ReportedIssue> GetIssues()
        {
            return new List<ReportedIssue>(_reportedIssues);
        }

        public List<CommunityIssue> GetCommunityIssues()
        {
            return new List<CommunityIssue>(_communityIssues);
        }

        public void SupportIssue(string referenceNumber)
        {
            var issue = _communityIssues.Find(i => i.ReferenceNumber == referenceNumber);
            if (issue != null) issue.SupportCount++;
        }

        public int GetIssueSupportCount(string referenceNumber)
        {
            var issue = _communityIssues.Find(i => i.ReferenceNumber == referenceNumber);
            return issue?.SupportCount ?? 0;
        }
    }
}


