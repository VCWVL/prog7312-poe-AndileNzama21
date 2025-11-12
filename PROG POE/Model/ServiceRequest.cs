using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{


    public enum RequestPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    public enum RequestStatus
    {
        Submitted = 1,
        InProgress = 2,
        UnderReview = 3,
        Resolved = 4,
        Closed = 5
    }

    [Serializable]
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public string RequestId { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public RequestPriority Priority { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string AssignedTo { get; set; }
        public int EstimatedDays { get; set; }
        public List<RequestUpdate> Updates { get; set; }

        public ServiceRequest()
        {
            RequestId = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            Status = RequestStatus.Submitted;
            Updates = new List<RequestUpdate>();
        }

        public int CompareTo(ServiceRequest other)
        {
            return CreatedDate.CompareTo(other.CreatedDate);
        }

        public override string ToString()
        {
            return $"{ReferenceNumber}: {Title} ({Status})";
        }
    }

    [Serializable]
    public class RequestUpdate
    {
        public string UpdateId { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
        public RequestStatus PreviousStatus { get; set; }
        public RequestStatus NewStatus { get; set; }

        public RequestUpdate()
        {
            UpdateId = Guid.NewGuid().ToString();
            UpdateDate = DateTime.Now;
        }
    }

    // Graph Node for Service Request Dependencies
    [Serializable]
    public class RequestNode
    {
        public ServiceRequest Request { get; set; }
        public List<RequestEdge> Edges { get; set; }

        public RequestNode(ServiceRequest request)
        {
            Request = request;
            Edges = new List<RequestEdge>();
        }
    }

    // Graph Edge for Request Relationships
    [Serializable]
    public class RequestEdge
    {
        public RequestNode FromNode { get; set; }
        public RequestNode ToNode { get; set; }
        public string Relationship { get; set; } // "depends_on", "related_to", "blocks"
        public int Weight { get; set; }

        public RequestEdge(RequestNode from, RequestNode to, string relationship, int weight = 1)
        {
            FromNode = from;
            ToNode = to;
            Relationship = relationship;
            Weight = weight;
        }
    }
}