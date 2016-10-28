using System.Collections.Generic;

namespace GraphsDijkstraShortPathDepthBreadthTraver
{
    public class DijkstraShortestPath
    {
        private VerticesList VerticesList { get; set; }
        private Dictionary<string, int> ShortestPaths { get; set; }
        private List<string> VisitedVertices { get; set; }

        public DijkstraShortestPath(VerticesList verticesList)
        {
            VerticesList = verticesList;
            InitVisitedAndShortestPathList();
        }

        private void InitVisitedAndShortestPathList()
        {
            ShortestPaths = new Dictionary<string, int> { { "A", 0 } };

            for (var i = 1; i < VerticesList.Count; i++)
            {
                ShortestPaths.Add(VerticesList[i].Name, int.MaxValue);
            }

            VisitedVertices = new List<string> { "A" };
        }

        public void CalculateShortestPaths()
        {
            var currentVisitingVertix = VerticesList[0];

            while (currentVisitingVertix != null)
            {
                VisitedVertices.Add(currentVisitingVertix.Name);

                var adjacencyLinkedListCurrent = FindNextEdge(currentVisitingVertix.AdjacencyLinkedList.ReturnHead());                

                while (adjacencyLinkedListCurrent != null)
                {
                    int distance;
                    int oldDistance;
                    ShortestPaths.TryGetValue(currentVisitingVertix.Name, out distance);
                    ShortestPaths.TryGetValue(adjacencyLinkedListCurrent.Name, out oldDistance);
                    var updateDistance = distance + adjacencyLinkedListCurrent.EdgeValue;
                    if (updateDistance < oldDistance)
                    {
                        ShortestPaths[adjacencyLinkedListCurrent.Name] = updateDistance;
                    }

                    adjacencyLinkedListCurrent = FindNextEdge(adjacencyLinkedListCurrent);
                }

                currentVisitingVertix = FindMinValueInShortestPaths();
            }
        }

        private Node FindNextEdge(Node currentNode)
        {
            while (currentNode != null)
            {
                if (VisitedVertices.Contains(currentNode.Name))
                {
                    currentNode = currentNode.Next;
                }
                else
                {
                    return currentNode;
                }
            }

            return null;
        }

        private Vertex FindMinValueInShortestPaths()
        {
            return null;
        }
    }
}
