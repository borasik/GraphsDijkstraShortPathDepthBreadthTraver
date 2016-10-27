using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsDijkstraShortPathDepthBreadthTraver
{
    class Program
    {
        static void Main(string[] args)
        {
            var verticesList = new VerticesList {new Vertex("A"), new Vertex("B") ,new Vertex("C"), new Vertex("D"), new Vertex("E") };
            var aAdjacencyLinkedList = new LinkedList();
            aAdjacencyLinkedList.AddNode(new Node("B", 2));
            aAdjacencyLinkedList.AddNode(new Node("E", 1));            
            verticesList[0].AdjacencyLinkedList = aAdjacencyLinkedList;

            var bAdjacencyLinkedList = new LinkedList();
            bAdjacencyLinkedList.AddNode(new Node("A", 2));
            bAdjacencyLinkedList.AddNode(new Node("C", 3));
            verticesList[1].AdjacencyLinkedList = bAdjacencyLinkedList;

            var cAdjacencyLinkedList = new LinkedList();
            cAdjacencyLinkedList.AddNode(new Node("D", 4));
            cAdjacencyLinkedList.AddNode(new Node("E", 5));
            verticesList[2].AdjacencyLinkedList = cAdjacencyLinkedList;

            var dAdjacencyLinkedList = new LinkedList();            
            verticesList[3].AdjacencyLinkedList = dAdjacencyLinkedList;

            var eAdjacencyLinkedList = new LinkedList();
            eAdjacencyLinkedList.AddNode(new Node("A", 1));
            verticesList[4].AdjacencyLinkedList = eAdjacencyLinkedList;

            foreach (var vertex in verticesList)
            {
                Console.WriteLine();
                Console.WriteLine("Edge Name: {0}, Connected with: ", vertex.Name);
                vertex.AdjacencyLinkedList.PrintList();
            }

            verticesList.DepthFirstTraversal();

            Console.ReadKey();
        }
    }

    public class VerticesList: List<Vertex>
    {
        public List<string> VisitedVertices = new List<string>();

        public void DepthFirstTraversal()
        {
            Console.WriteLine("Depth First Traversal: ");
            var stack = new Stack<string>();

            var currentVertex = this[0];

            while(currentVertex != null)
            {
                if (!VisitedVertices.Contains(currentVertex.Name))
                {
                    stack.Push(currentVertex.Name);
                    VisitedVertices.Add(currentVertex.Name);
                    Console.Write("{0} ", currentVertex.Name);
                }                

                currentVertex = FindNextVertex(stack, currentVertex, this);
                if (currentVertex == null)
                {
                    if (VisitedVertices.Count != Count)
                    {
                        stack.Pop();         
                        currentVertex = Find(x => x.Name == stack.Peek());                      
                    }
                }

            }            
        }

        private Vertex FindNextVertex(Stack<string> stack, Vertex currentVertex, List<Vertex > list)
        {
            var firstNextVertexName = string.Empty;
            var currentNode = currentVertex.AdjacencyLinkedList.ReturnHead();
            while (currentNode != null)
            {
                if (!VisitedVertices.Contains(currentNode.Name))
                {
                    firstNextVertexName = currentNode.Name;
                    break;
                }

                currentNode = currentVertex.AdjacencyLinkedList.GetNextNode(currentNode);
            }

            if (firstNextVertexName != string.Empty)
            {
                return list.Find(x => x.Name == firstNextVertexName);
            }

            return null;
        }
    }

    public class Vertex
    {
        public string Name { get; set; }
        public LinkedList AdjacencyLinkedList { get; set; }

        public Vertex(string name)
        {
            Name = name;
            AdjacencyLinkedList = null;
        }
    }

    public class LinkedList
    {
        private Node Head { get; set; }

        public Node ReturnHead()
        {
            return Head;
        }

        public void AddNode(Node node)
        {
            if (Head == null)
            {
                Head = node;
                return;
            }

            var currentNode = Head; 
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            currentNode.Next = node;
        }

        public void PrintList()
        {
            if(Head == null) return;

            var current = Head;
            while (current != null)
            {
                Console.Write("[{0}, {1}]  ", current.Name, current.EdgeValue);
                current = current.Next;                
            }        
        }

        public Node GetNextNode(Node node)
        {
            if (node == null)
            {
                return null;
            }

            return node.Next;
        }
    }

    public class Node
    {
        public string Name { get; set; }
        public int EdgeValue { get; set; }
        public Node Next { get; set; }

        public Node(string name, int edgeValue)
        {
            Name = name;
            EdgeValue = edgeValue;
        }
    }
}
