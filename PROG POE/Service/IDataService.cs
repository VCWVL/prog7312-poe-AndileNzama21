using PROG_POE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Service
{

    public interface IDataService
    {
        void AddIssue(ReportedIssue issue);
        List<ReportedIssue> GetIssues();
        List<CommunityIssue> GetCommunityIssues();
        void SupportIssue(string referenceNumber);
        int GetIssueSupportCount(string referenceNumber);
    }
}

