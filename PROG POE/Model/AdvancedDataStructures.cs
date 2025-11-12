using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{
    // Binary Search Tree for efficient request searching
    public class RequestBST
    {
        private class BSTNode
        {
            public ServiceRequest Request { get; set; }
            public BSTNode Left { get; set; }
            public BSTNode Right { get; set; }

            public BSTNode(ServiceRequest request)
            {
                Request = request;
            }
        }

        private BSTNode root;

        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        private BSTNode InsertRec(BSTNode node, ServiceRequest request)
        {
            if (node == null)
            {
                return new BSTNode(request);
            }

            int compareResult = string.Compare(request.ReferenceNumber, node.Request.ReferenceNumber, StringComparison.Ordinal);
            if (compareResult < 0)
            {
                node.Left = InsertRec(node.Left, request);
            }
            else if (compareResult > 0)
            {
                node.Right = InsertRec(node.Right, request);
            }

            return node;
        }

        public ServiceRequest Search(string referenceNumber)
        {
            return SearchRec(root, referenceNumber);
        }

        private ServiceRequest SearchRec(BSTNode node, string referenceNumber)
        {
            if (node == null) return null;

            int compareResult = string.Compare(referenceNumber, node.Request.ReferenceNumber, StringComparison.Ordinal);
            if (compareResult == 0)
            {
                return node.Request;
            }
            else if (compareResult < 0)
            {
                return SearchRec(node.Left, referenceNumber);
            }
            else
            {
                return SearchRec(node.Right, referenceNumber);
            }
        }

        public List<ServiceRequest> InOrderTraversal()
        {
            var result = new List<ServiceRequest>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(BSTNode node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Request);
                InOrderRec(node.Right, result);
            }
        }
    }

    // AVL Tree for balanced request storage
    public class RequestAVLTree
    {
        private class AVLNode
        {
            public ServiceRequest Request { get; set; }
            public AVLNode Left { get; set; }
            public AVLNode Right { get; set; }
            public int Height { get; set; }

            public AVLNode(ServiceRequest request)
            {
                Request = request;
                Height = 1;
            }
        }

        private AVLNode root;

        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        private AVLNode InsertRec(AVLNode node, ServiceRequest request)
        {
            if (node == null)
                return new AVLNode(request);

            int compareResult = string.Compare(request.ReferenceNumber, node.Request.ReferenceNumber, StringComparison.Ordinal);
            if (compareResult < 0)
                node.Left = InsertRec(node.Left, request);
            else if (compareResult > 0)
                node.Right = InsertRec(node.Right, request);
            else
                return node; // Duplicate not allowed

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            int balance = GetBalance(node);

            // Left Left Case
            if (balance > 1 && string.Compare(request.ReferenceNumber, node.Left.Request.ReferenceNumber, StringComparison.Ordinal) < 0)
                return RightRotate(node);

            // Right Right Case
            if (balance < -1 && string.Compare(request.ReferenceNumber, node.Right.Request.ReferenceNumber, StringComparison.Ordinal) > 0)
                return LeftRotate(node);

            // Left Right Case
            if (balance > 1 && string.Compare(request.ReferenceNumber, node.Left.Request.ReferenceNumber, StringComparison.Ordinal) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && string.Compare(request.ReferenceNumber, node.Right.Request.ReferenceNumber, StringComparison.Ordinal) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private int GetHeight(AVLNode node)
        {
            return node?.Height ?? 0;
        }

        private int GetBalance(AVLNode node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }

        private AVLNode RightRotate(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        private AVLNode LeftRotate(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        public ServiceRequest Search(string referenceNumber)
        {
            return SearchRec(root, referenceNumber)?.Request;
        }

        private AVLNode SearchRec(AVLNode node, string referenceNumber)
        {
            if (node == null) return null;

            int compareResult = string.Compare(referenceNumber, node.Request.ReferenceNumber, StringComparison.Ordinal);
            if (compareResult == 0)
                return node;
            else if (compareResult < 0)
                return SearchRec(node.Left, referenceNumber);
            else
                return SearchRec(node.Right, referenceNumber);
        }

        public List<ServiceRequest> InOrderTraversal()
        {
            var result = new List<ServiceRequest>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(AVLNode node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Request);
                InOrderRec(node.Right, result);
            }
        }
    }

    // Max Heap for priority-based request management
    public class RequestPriorityHeap
    {
        private List<ServiceRequest> heap;

        public RequestPriorityHeap()
        {
            heap = new List<ServiceRequest>();
        }

        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        public ServiceRequest ExtractMax()
        {
            if (heap.Count == 0) return null;

            ServiceRequest max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);

            return max;
        }

        public ServiceRequest PeekMax()
        {
            return heap.Count > 0 ? heap[0] : null;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (ComparePriority(heap[index], heap[parent]) > 0)
                {
                    Swap(index, parent);
                    index = parent;
                }
                else
                {
                    break;
                }
            }
        }

        private void HeapifyDown(int index)
        {
            int lastIndex = heap.Count - 1;
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int largest = index;

                if (leftChild <= lastIndex && ComparePriority(heap[leftChild], heap[largest]) > 0)
                    largest = leftChild;

                if (rightChild <= lastIndex && ComparePriority(heap[rightChild], heap[largest]) > 0)
                    largest = rightChild;

                if (largest != index)
                {
                    Swap(index, largest);
                    index = largest;
                }
                else
                {
                    break;
                }
            }
        }

        private int ComparePriority(ServiceRequest a, ServiceRequest b)
        {
            int priorityCompare = ((int)b.Priority).CompareTo((int)a.Priority);
            if (priorityCompare != 0) return priorityCompare;

            return a.CreatedDate.CompareTo(b.CreatedDate);
        }

        private void Swap(int i, int j)
        {
            ServiceRequest temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;
    }

    // Graph for request relationships and dependencies
    public class RequestGraph
    {
        private Dictionary<string, RequestNode> nodes;
        private List<RequestEdge> edges;

        public RequestGraph()
        {
            nodes = new Dictionary<string, RequestNode>();
            edges = new List<RequestEdge>();
        }

        public void AddRequest(ServiceRequest request)
        {
            if (!nodes.ContainsKey(request.RequestId))
            {
                nodes[request.RequestId] = new RequestNode(request);
            }
        }

        public void AddDependency(string fromRequestId, string toRequestId, string relationship, int weight = 1)
        {
            if (nodes.ContainsKey(fromRequestId) && nodes.ContainsKey(toRequestId))
            {
                var edge = new RequestEdge(nodes[fromRequestId], nodes[toRequestId], relationship, weight);
                nodes[fromRequestId].Edges.Add(edge);
                edges.Add(edge);
            }
        }

        // Breadth-First Search for related requests
        public List<ServiceRequest> BFS(string startRequestId, int maxDepth = 3)
        {
            var result = new List<ServiceRequest>();
            if (!nodes.ContainsKey(startRequestId)) return result;

            var visited = new HashSet<string>();
            var queue = new Queue<Tuple<RequestNode, int>>();
            queue.Enqueue(new Tuple<RequestNode, int>(nodes[startRequestId], 0));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                RequestNode node = current.Item1;
                int depth = current.Item2;

                if (depth > maxDepth) continue;

                if (!visited.Contains(node.Request.RequestId))
                {
                    visited.Add(node.Request.RequestId);
                    result.Add(node.Request);

                    foreach (var edge in node.Edges)
                    {
                        if (!visited.Contains(edge.ToNode.Request.RequestId))
                        {
                            queue.Enqueue(new Tuple<RequestNode, int>(edge.ToNode, depth + 1));
                        }
                    }
                }
            }

            return result;
        }

        // Depth-First Search for dependency chains
        public List<ServiceRequest> DFS(string startRequestId)
        {
            var result = new List<ServiceRequest>();
            if (!nodes.ContainsKey(startRequestId)) return result;

            var visited = new HashSet<string>();
            DFSRec(nodes[startRequestId], visited, result);
            return result;
        }

        private void DFSRec(RequestNode node, HashSet<string> visited, List<ServiceRequest> result)
        {
            if (visited.Contains(node.Request.RequestId)) return;

            visited.Add(node.Request.RequestId);
            result.Add(node.Request);

            foreach (var edge in node.Edges)
            {
                DFSRec(edge.ToNode, visited, result);
            }
        }

        // Prim's Algorithm for Minimum Spanning Tree (showing critical request paths)
        public List<RequestEdge> FindCriticalPathMST()
        {
            if (edges.Count == 0) return new List<RequestEdge>();

            var mst = new List<RequestEdge>();
            var visited = new HashSet<string>();
            var priorityQueue = new SortedSet<RequestEdge>(
                Comparer<RequestEdge>.Create((a, b) =>
                {
                    int weightCompare = a.Weight.CompareTo(b.Weight);
                    if (weightCompare != 0) return weightCompare;
                    return string.Compare(a.FromNode.Request.RequestId, b.FromNode.Request.RequestId, StringComparison.Ordinal);
                }));

            // Start with first node
            var firstNode = nodes.Values.First();
            visited.Add(firstNode.Request.RequestId);

            // Add all edges from first node to priority queue
            foreach (var edge in firstNode.Edges)
            {
                priorityQueue.Add(edge);
            }

            while (priorityQueue.Count > 0 && visited.Count < nodes.Count)
            {
                var minEdge = priorityQueue.Min;
                priorityQueue.Remove(minEdge);

                if (!visited.Contains(minEdge.ToNode.Request.RequestId))
                {
                    visited.Add(minEdge.ToNode.Request.RequestId);
                    mst.Add(minEdge);

                    // Add all edges from the new node to priority queue
                    foreach (var edge in minEdge.ToNode.Edges)
                    {
                        if (!visited.Contains(edge.ToNode.Request.RequestId))
                        {
                            priorityQueue.Add(edge);
                        }
                    }
                }
            }

            return mst;
        }

        public RequestNode GetNode(string requestId)
        {
            return nodes.ContainsKey(requestId) ? nodes[requestId] : null;
        }

        public int NodeCount => nodes.Count;
        public int EdgeCount => edges.Count;
    }
}

