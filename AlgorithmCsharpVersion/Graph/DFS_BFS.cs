using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCsharpVersion.Graph
{
    class DFS_BFS : ITest
    {
        AdjacencyList<int> m_adjacencyList = new AdjacencyList<int>(5);

        int funCount = 0;//方法调用次数
        int judgeVisit = 0;//判断是否访问次数

        AdjacencyList<string> stringAdjList = new AdjacencyList<string>();

        private void DFS<T>(AdjacencyList<T>.Vertex<T> vertex)
        {
            funCount++;
            vertex.visited = true;
            Console.WriteLine(vertex.ToString() + " 访问");
            AdjacencyList<T>.Node node = vertex.firstEdge;
            while (node != null)
            {
                judgeVisit++;
                if (!node.adjvex.visited)
                {
                    DFS(node.adjvex);
                }
                node = node.next;
            }
        }

        public DFS_BFS()
        {
            for (int i = 0; i < 5; i++)
            {
                m_adjacencyList.AddVertex(i);
                m_adjacencyList.Find(i).visited = false;
            }
            m_adjacencyList.AddEdge(0, 1);
            m_adjacencyList.AddEdge(0, 2);
            m_adjacencyList.AddEdge(0, 3);
            m_adjacencyList.AddEdge(1, 3);
            m_adjacencyList.AddEdge(3, 4);

            ///////////////////////////////
            stringAdjList.AddVertex("V1");
            stringAdjList.AddVertex("V2");
            stringAdjList.AddVertex("V3");
            stringAdjList.AddVertex("V4");
            stringAdjList.AddVertex("V5");
            stringAdjList.AddVertex("V6");
            stringAdjList.AddVertex("V7");
            stringAdjList.AddVertex("V8");
            stringAdjList.AddEdge("V1", "V2");
            stringAdjList.AddEdge("V1", "V3");
            stringAdjList.AddEdge("V2", "V4");
            stringAdjList.AddEdge("V2", "V5");
            stringAdjList.AddEdge("V3", "V6");
            stringAdjList.AddEdge("V3", "V7");
            stringAdjList.AddEdge("V4", "V8");
            stringAdjList.AddEdge("V5", "V8");
            stringAdjList.AddEdge("V6", "V8");
            stringAdjList.AddEdge("V7", "V8");
        }

        public void AlgorithmTest()
        {
            //DFS(m_adjacencyList.Find(1));
            DFS(stringAdjList.Find("V1"));
            Console.WriteLine("方法调用次数 " + funCount);
            Console.WriteLine("判断是否访问次数 " + judgeVisit);

        }

        public void BruteForceTest()
        {
        }
    }
}
