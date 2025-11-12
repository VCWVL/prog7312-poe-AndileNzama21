using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{

    public interface IRequestService
    {
        void AddRequest(ServiceRequest request);
        ServiceRequest GetRequest(string referenceNumber);
        List<ServiceRequest> GetAllRequests();
        List<ServiceRequest> GetRequestsByStatus(RequestStatus status);
        List<ServiceRequest> GetRequestsByPriority(RequestPriority priority);
        void UpdateRequestStatus(string referenceNumber, RequestStatus newStatus, string updateDescription);
        List<RequestUpdate> GetRequestUpdates(string referenceNumber);

        // Advanced data structure operations
        List<ServiceRequest> GetPriorityRequests(); // Using Heap
        List<ServiceRequest> SearchRequestsBST(string startRef, string endRef); // Using BST range search
        List<ServiceRequest> GetRelatedRequests(string referenceNumber); // Using Graph BFS
        List<ServiceRequest> GetDependencyChain(string referenceNumber); // Using Graph DFS
        List<ServiceRequest> GetCriticalPath(); // Using Graph MST

        // Statistics
        RequestStatistics GetStatistics();
    }

    public class RequestStatistics
    {
        public int TotalRequests { get; set; }
        public int SubmittedCount { get; set; }
        public int InProgressCount { get; set; }
        public int ResolvedCount { get; set; }
        public int ClosedCount { get; set; }
        public double AverageResolutionDays { get; set; }
        public Dictionary<string, int> RequestsByCategory { get; set; }
        public Dictionary<RequestPriority, int> RequestsByPriority { get; set; }
    }

}
