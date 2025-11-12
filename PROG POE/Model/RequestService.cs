using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{
   public class RequestService : IRequestService
    {
        private readonly List<ServiceRequest> _allRequests;
        private readonly RequestBST _requestBST;
        private readonly RequestAVLTree _requestAVL;
        private readonly RequestPriorityHeap _priorityHeap;
        private readonly RequestGraph _requestGraph;
        private readonly Dictionary<string, List<RequestUpdate>> _requestUpdates;

        public RequestService()
        {
            _allRequests = new List<ServiceRequest>();
            _requestBST = new RequestBST();
            _requestAVL = new RequestAVLTree();
            _priorityHeap = new RequestPriorityHeap();
            _requestGraph = new RequestGraph();
            _requestUpdates = new Dictionary<string, List<RequestUpdate>>();

            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            var sampleRequests = new List<ServiceRequest>
            {
                new ServiceRequest
                {
                    ReferenceNumber = "SR-1001",
                    Title = "Pothole Repair - Main Street",
                    Description = "Large pothole causing traffic issues near intersection",
                    Category = "Roads",
                    Location = "Main Street and 1st Avenue",
                    Priority = RequestPriority.High,
                    Status = RequestStatus.InProgress,
                    AssignedTo = "Road Maintenance Team A",
                    EstimatedDays = 3
                },
                new ServiceRequest
                {
                    ReferenceNumber = "SR-1002",
                    Title = "Street Light Replacement",
                    Description = "Flickering street light needs replacement",
                    Category = "Utilities",
                    Location = "Oak Street between 2nd and 3rd",
                    Priority = RequestPriority.Medium,
                    Status = RequestStatus.Submitted,
                    AssignedTo = "Electrical Department",
                    EstimatedDays = 2
                },
                new ServiceRequest
                {
                    ReferenceNumber = "SR-1003",
                    Title = "Park Bench Repair",
                    Description = "Broken park bench in Central Park",
                    Category = "Parks and Recreation",
                    Location = "Central Park, near fountain",
                    Priority = RequestPriority.Low,
                    Status = RequestStatus.Resolved,
                    AssignedTo = "Parks Maintenance",
                    EstimatedDays = 1
                },
                new ServiceRequest
                {
                    ReferenceNumber = "SR-1004",
                    Title = "Water Pipe Leak",
                    Description = "Water leak from main pipe on Elm Street",
                    Category = "Water Services",
                    Location = "Elm Street, house number 245",
                    Priority = RequestPriority.Critical,
                    Status = RequestStatus.UnderReview,
                    AssignedTo = "Water Department Emergency Team",
                    EstimatedDays = 1
                }
            };

            foreach (var request in sampleRequests)
            {
                AddRequest(request);

                // Add sample updates
                var updates = new List<RequestUpdate>
                {
                    new RequestUpdate
                    {
                        Description = "Request submitted by citizen",
                        UpdatedBy = "System",
                        PreviousStatus = RequestStatus.Submitted,
                        NewStatus = RequestStatus.Submitted
                    },
                    new RequestUpdate
                    {
                        Description = "Request assigned to team",
                        UpdatedBy = "Dispatcher",
                        PreviousStatus = RequestStatus.Submitted,
                        NewStatus = RequestStatus.InProgress
                    }
                };

                if (_requestUpdates.ContainsKey(request.ReferenceNumber))
                {
                    _requestUpdates[request.ReferenceNumber].AddRange(updates);
                }
                else
                {
                    _requestUpdates[request.ReferenceNumber] = updates;
                }
            }

            // Create some dependencies between requests
            _requestGraph.AddDependency("SR-1001", "SR-1002", "related_to");
            _requestGraph.AddDependency("SR-1004", "SR-1001", "blocks", 2);
        }

        public void AddRequest(ServiceRequest request)
        {
            _allRequests.Add(request);
            _requestBST.Insert(request);
            _requestAVL.Insert(request);
            _priorityHeap.Insert(request);
            _requestGraph.AddRequest(request);

            // Add initial update
            var initialUpdate = new RequestUpdate
            {
                Description = "Service request created and submitted",
                UpdatedBy = "System",
                PreviousStatus = RequestStatus.Submitted,
                NewStatus = RequestStatus.Submitted
            };

            if (!_requestUpdates.ContainsKey(request.ReferenceNumber))
            {
                _requestUpdates[request.ReferenceNumber] = new List<RequestUpdate>();
            }
            _requestUpdates[request.ReferenceNumber].Add(initialUpdate);
        }

        public ServiceRequest GetRequest(string referenceNumber)
        {
            // Using AVL tree for efficient search (O(log n))
            return _requestAVL.Search(referenceNumber);
        }

        public List<ServiceRequest> GetAllRequests()
        {
            // Using BST for sorted retrieval
            return _requestBST.InOrderTraversal();
        }

        public List<ServiceRequest> GetRequestsByStatus(RequestStatus status)
        {
            return _allRequests.Where(r => r.Status == status).ToList();
        }

        public List<ServiceRequest> GetRequestsByPriority(RequestPriority priority)
        {
            return _allRequests.Where(r => r.Priority == priority).ToList();
        }

        public void UpdateRequestStatus(string referenceNumber, RequestStatus newStatus, string updateDescription)
        {
            var request = GetRequest(referenceNumber);
            if (request != null)
            {
                var previousStatus = request.Status;
                request.Status = newStatus;
                request.UpdatedDate = DateTime.Now;

                var update = new RequestUpdate
                {
                    Description = updateDescription,
                    UpdatedBy = "System",
                    PreviousStatus = previousStatus,
                    NewStatus = newStatus
                };

                if (_requestUpdates.ContainsKey(referenceNumber))
                {
                    _requestUpdates[referenceNumber].Add(update);
                }
            }
        }

        public List<RequestUpdate> GetRequestUpdates(string referenceNumber)
        {
            return _requestUpdates.ContainsKey(referenceNumber)
                ? _requestUpdates[referenceNumber]
                : new List<RequestUpdate>();
        }

        public List<ServiceRequest> GetPriorityRequests()
        {
            // Using heap to get highest priority requests
            var priorityRequests = new List<ServiceRequest>();
            var tempHeap = new RequestPriorityHeap();

            // Rebuild heap (since we can't traverse heap directly)
            foreach (var request in _allRequests)
            {
                tempHeap.Insert(request);
            }

            // Extract top 5 priority requests
            for (int i = 0; i < Math.Min(5, tempHeap.Count); i++)
            {
                var request = tempHeap.ExtractMax();
                if (request != null)
                {
                    priorityRequests.Add(request);
                }
            }

            return priorityRequests;
        }

        public List<ServiceRequest> SearchRequestsBST(string startRef, string endRef)
        {
            // Using BST for range queries
            var allRequests = _requestBST.InOrderTraversal();
            return allRequests.Where(r =>
                string.Compare(r.ReferenceNumber, startRef, StringComparison.Ordinal) >= 0 &&
                string.Compare(r.ReferenceNumber, endRef, StringComparison.Ordinal) <= 0
            ).ToList();
        }

        public List<ServiceRequest> GetRelatedRequests(string referenceNumber)
        {
            // Using Graph BFS to find related requests
            var request = GetRequest(referenceNumber);
            if (request != null)
            {
                return _requestGraph.BFS(request.RequestId);
            }
            return new List<ServiceRequest>();
        }

        public List<ServiceRequest> GetDependencyChain(string referenceNumber)
        {
            // Using Graph DFS to find dependency chains
            var request = GetRequest(referenceNumber);
            if (request != null)
            {
                return _requestGraph.DFS(request.RequestId);
            }
            return new List<ServiceRequest>();
        }

        public List<ServiceRequest> GetCriticalPath()
        {
            // Using Graph MST to find critical path
            var criticalEdges = _requestGraph.FindCriticalPathMST();
            return criticalEdges.Select(e => e.FromNode.Request).Distinct().ToList();
        }

        public RequestStatistics GetStatistics()
        {
            var stats = new RequestStatistics
            {
                TotalRequests = _allRequests.Count,
                SubmittedCount = _allRequests.Count(r => r.Status == RequestStatus.Submitted),
                InProgressCount = _allRequests.Count(r => r.Status == RequestStatus.InProgress),
                ResolvedCount = _allRequests.Count(r => r.Status == RequestStatus.Resolved),
                ClosedCount = _allRequests.Count(r => r.Status == RequestStatus.Closed),
                RequestsByCategory = _allRequests.GroupBy(r => r.Category)
                    .ToDictionary(g => g.Key, g => g.Count()),
                RequestsByPriority = _allRequests.GroupBy(r => r.Priority)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            // Calculate average resolution days for resolved requests
            var resolvedRequests = _allRequests.Where(r => r.Status == RequestStatus.Resolved || r.Status == RequestStatus.Closed);
            if (resolvedRequests.Any())
            {
                stats.AverageResolutionDays = resolvedRequests
                    .Average(r =>
                    {
                        // Use UpdatedDate if available, otherwise use current time
                        DateTime endDate = r.UpdatedDate ?? DateTime.Now;
                        return (endDate - r.CreatedDate).TotalDays;
                    });
            }
            else
            {
                stats.AverageResolutionDays = 0;
            }

            return stats;
        }
    }
}

