// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;
const int AMMOUNT = 10;//кол-во тстов
StreamWriter f = new StreamWriter("test1.txt");
StreamWriter f2 = new StreamWriter("test2.txt");
//тест
for (int i = 0; i < AMMOUNT; i++)
{
    Random rnd = new Random();
    var graph = RandomGraphGen((i + 1) * 100 * 10, (i + 1) * 100 * 8, (i + 1) * 100 * 14, (i + 1) * 100 * 12, (i + 1) * 100 * 4, true, (i + 1) * 100 * 5, (i + 1) * 100 * 4);
    //graph.PrintMatrix();
    //graph.PrintIncMatrix();
    //graph.PrintAdjList();
    //graph.PrintEdgeList();

    //Console.WriteLine("\nВыберите начальную точку для проверки A: ");
    Vertex a = new Vertex(rnd.Next(0, graph.vertex_count));
    //Console.WriteLine("\nВыберите начальную точку для проверки B: ");
    Vertex b = new Vertex(rnd.Next(0, graph.vertex_count));
    Stopwatch stopwatch1 = new Stopwatch();
    //замеры времени, потраченного на поиск DFS
    stopwatch1.Start();
    graph.FindShortFS(graph.GetAdjList(), a, b, 1);
    stopwatch1.Stop();
    f.WriteLine(stopwatch1.ElapsedMilliseconds);
    Stopwatch stopwatch2 = new Stopwatch();
    //замеры времени, потраченного на поиск BFS
    stopwatch2.Start();
    graph.FindShortFS(graph.GetAdjList(), a, b, 2);
    stopwatch2.Stop();
    f2.WriteLine(stopwatch2.ElapsedMilliseconds);
    //var graph = RandomGraphGen(10, 8, 14, 12, 4, true, 5, 4);
    //graph.PrintMatrix();
    //graph.PrintIncMatrix();
    //graph.PrintAdjList();
    //graph.PrintEdgeList();
    //while (true)
    //{
    //    Console.WriteLine("\nВыберите начальную точку для проверки A: ");
    //    Vertex a = new Vertex(Convert.ToInt32(Console.ReadLine()));
    //    Console.WriteLine("\nВыберите начальную точку для проверки B: ");
    //    Vertex b = new Vertex(Convert.ToInt32(Console.ReadLine()));
    //    graph.FindShortFS(graph.GetAdjList(), a, b, 1);
    //    graph.FindShortFS(graph.GetAdjList(), a, b, 2);
    //    //Console.WriteLine("\nПуть Шириной найден: " + graph.FindShortFS((graph.GetAdjList(), a, b));
    //}
}
f.Close();
f2.Close();
/// <summary>
/// Метод, возвращающий рандомно-сгенерированный граф
/// </summary>
/// <param name="max_vertex">Макс. кол-во вершин</param>
/// <param name="min_vertex">Мин. кол-во вершин</param>
/// <param name="max_edge">Макс. кол-во рёбер</param>
/// <param name="min_edge">Мин. кол-во рёбер</param>
/// <param name="max_one_edge">Макс. кол-во рёбер у одной вершины</param>
/// <param name="is_oriented">Ориентированность графа</param>
/// <param name="max_in_edge">Макс. кол-во входящих рёбер</param>
/// <param name="max_out_edge">Макс. кол-во выходящих рёбер</param>
/// <returns>Рандомно-сгенерированный граф</returns>
/// <exception cref="Exception"></exception>
Graph RandomGraphGen(int max_vertex, int min_vertex, int max_edge, int min_edge, int max_one_edge, bool is_oriented, int max_in_edge, int max_out_edge)
{
    var graph = new Graph(is_oriented);
    Random rnd = new Random();
    int rand_vert = rnd.Next(min_vertex, max_vertex + 1);//Выбор кол-ва вершин у графа в диапазоне [max_vertex,  min_vertex]
    int rand_edge = rnd.Next(min_edge, max_edge + 1);//Выбор кол-ва рёбер у графа в диапазоне [max_edge,  min_edge]
    int E = rand_vert * (rand_vert - 1) / 2;//Макс возможное кол-во ребер, при данном кол-ве вершин
    if (rand_edge <= E)//Проверка данных параметров
    {
        Vertex[] vert_arr = new Vertex[rand_vert];
        //Создание вершин 
        for (int i = 0; i < rand_vert; i++)
        {
            var v = new Vertex(i + 1);
            graph.AddVert(v);
            vert_arr[i] = new Vertex(i + 1);
        }

        vert_arr = vert_arr.OrderBy(x => rnd.Next()).ToArray();//Перемешивание элементов графа
        int count_edge = 0;
        if (is_oriented)//Если ориентированный
        {
            for (int i = 0; i < vert_arr.Length; i++)
            {
                int cur_in_edge = rnd.Next(0, max_in_edge);//Задание рандомного кол-ва входящих вершин на шаге 
                int cur_out_edge = rnd.Next(0, max_out_edge);//Задание рандомного кол-ва выходящих вершин на шаге 
                if (i == 0) cur_out_edge = max_out_edge;//Задание макс.кол-ва выход вершин для первого шага
                if (i == 1) cur_in_edge = max_in_edge;//Задание макс.кол-ва вход вершин для первого шага
                for (int j = 0; j < cur_out_edge && count_edge <= rand_edge; j++)//Инициализация вершины с максимальным кол-вом выход элементов
                {
                    if (i + j + 1 < vert_arr.Length)
                    {
                        graph.AddEdge(vert_arr[i], vert_arr[i + j + 1]);
                        count_edge++;
                    }
                }
                for (int j = 0; j < cur_in_edge - 1 && count_edge <= rand_edge; j++)//Инициализация вершины с максимальным кол-вом вход элементов
                {
                    if (i + j + 1 < vert_arr.Length && i < vert_arr.Length) graph.AddEdge(vert_arr[i + j + 1], vert_arr[i]);
                    count_edge++;
                }
            }
        }
        else//если неориентированный
        {
            for (int i = 0; i < vert_arr.Length; i++)
            {
                int cur_in_edge = rnd.Next(0, max_one_edge - 1);
                if (i == 0) cur_in_edge = max_one_edge;
                for (int j = 0; j < cur_in_edge && count_edge < rand_edge; j++)//Инициализация вершины с максимальным кол-вом выход элементов
                {
                    if (i + j + 1 < vert_arr.Length && i < vert_arr.Length)
                    {
                        graph.AddEdge(vert_arr[i], vert_arr[i + j + 1]);
                    }
                }
            }
        }
        return graph;
    }
    else
    {
        throw new Exception("Количество ребер должно быть меньше");
    }
}
/// <summary>
/// Класс:Вершина
/// </summary>
class Vertex
{
    public int number { get; set; }
    public bool visited = false;
    public Vertex(int number)
    {
        this.number = number;
    }
}
/// <summary>
/// Класс:Рёбра
/// </summary>
class Edge
{
    public Vertex from { get; set; }
    public Vertex to { get; set; }
    public int weight { get; set; }
    public Edge(Vertex from, Vertex to, int weight=1)
    {
        this.from = from;
        this.to = to;
        this.weight = weight;
    }
}
/// <summary>
/// Класс:Граф
/// </summary>
class Graph
{
    public List<Edge> edges=new List<Edge>();
    public List<Vertex> vertexes=new List<Vertex>();
    public bool oriented;
    public int vertex_count => vertexes.Count;
    public int edge_count => edges.Count;
    public Graph(bool oriented)
    {
        this.oriented = oriented;
    }
    /// <summary>
    /// Метод, возвращающий вес ребра
    /// </summary>
    /// <param name="from">Вершина От</param>
    /// <param name="to">Вершина Куда</param>
    /// <returns>Вес ребра</returns>
    public int FindWeightEdge(Vertex from, Vertex to)
    {
        int weight = 0;
        foreach(Edge e in edges)
        {
            if (e.from.number == from.number && e.to.number == to.number) weight = e.weight;
        }
        return weight;
    }
    /// <summary>
    /// Метод добавления Вершины в Граф
    /// </summary>
    /// <param name="vertex"></param>
    public void AddVert(Vertex vertex)
    {
        vertexes.Add(vertex);
    }
    /// <summary>
    /// Метод добавления Ребра в Граф
    /// </summary>
    /// <param name="from">Вершина От</param>
    /// <param name="to">Вершина Куда</param>
    public void AddEdge(Vertex from, Vertex to)
    {
        Edge edge = new Edge(from, to);
        edges.Add(edge);
    }
    /// <summary>
    /// Метод, возвращающий матрицу смежности
    /// </summary>
    /// <returns>Матрица смежности</returns>
    public int[,] GetAdjMatrix()
    {
        var matrix = new int[vertexes.Count, vertexes.Count];
        foreach(Edge edge in edges)
        {
            var row=edge.from.number - 1;
            var colm = edge.to.number - 1;
            matrix[row, colm] = edge.weight;
            if (oriented!=true) matrix[colm, row] = edge.weight;
        }
        return matrix;
    }
    /// <summary>
    /// Метод, возвращающий матрицу инцидентности
    /// </summary>
    /// <returns>Матрица инцидентности</returns>
    public int[,] GetIncMatrix()
    {
        var matrix = new int[vertexes.Count, edges.Count];

        for (int i = 0; i < vertex_count; i++)
        {
            for (int j = 0; j < edge_count; j++)
            {
                if(this.edges[j].from.number == i+1)
                {
                    matrix[i, j] = 1;
                }
                else if(this.edges[j].to.number == i+1)
                {
                    matrix[i, j] = -1;
                }
            }
        }
        return matrix;
    }
    /// <summary>
    ///Метод, возвращающий список смежности
    /// </summary>
    /// <returns>Список смежности</returns>
    public List<List<Vertex>> GetAdjList()
    {
        var list = new List<List<Vertex>>();

        for (int i = 0; i < vertex_count; i++)
        {
            var list_vert = new List<Vertex>();
            foreach(var edge in edges)
            {
                if (edge.from.number == i + 1)
                {
                    list_vert.Add(edge.to);
                    edge.to.visited = false;
                   
                }
                edge.from.visited = false;
            }
            list.Add(list_vert);
        }
        return list;
    }
    /// <summary>
    /// Метод, возвращающий список рёбер
    /// </summary>
    /// <returns>Список рёбер</returns>
    public List<Edge> GetEdgeList()
    {
        return edges;
    }
    /// <summary>
    /// Метод, ищущий кратчайщий путь от A до B
    /// </summary>
    /// <param name="adj_list">Список смежности</param>
    /// <param name="A">Точка А</param>
    /// <param name="B">Точка B</param>
    /// <param name="choice">Метод поиска</param>
    public void FindShortFS(List<List<Vertex>> adj_list, Vertex A, Vertex B, int choice)
    {   
        int sum_weight = 0;
        string string_temp = "";
        Dictionary<int, string> all_path=new Dictionary<int, string>();
        for(int i = 0; i < adj_list.Count; i++)
        {
            for(int j=0; j < adj_list[i].Count; j++)
            {
                if (adj_list[i][j].number==A.number) adj_list[i][j].visited=true;
            }
        }
        if (choice == 1)
        {
            Console.WriteLine("Поиск по DFS");
            if (DFS(adj_list, A, B, ref all_path, ref sum_weight, ref string_temp))
            {
                string result = A.number + all_path[all_path.Keys.Min()];
                Console.WriteLine(result);
            }
            else Console.WriteLine("нет пути");
        }
        else
        {
            Console.WriteLine("Поиск по BFS");
            if (BFS(adj_list, A, B, ref all_path, ref sum_weight, ref string_temp))
            {
                string result = A.number + all_path[all_path.Keys.Min()];
                //Console.WriteLine("Путь есть");
                Console.WriteLine(result);
            }
            else Console.WriteLine("нет пути");
        }
    }
    /// <summary>
    /// Метод поиска DFS
    /// </summary>
    /// <param name="adj_list">Список смежности</param>
    /// <param name="A">Точка А</param>
    /// <param name="B">Точка B</param>
    /// <param name="path">Словарь для пути</param>
    /// <param name="sum_weight">Вес</param>
    /// <param name="string_temp">Строка для содержания пути</param>
    /// <returns>bool</returns>
    public bool DFS(List<List<Vertex>> adj_list, Vertex A,Vertex B, ref Dictionary<int, string> path, ref int sum_weight, ref string string_temp)
    {
        if (A.number == B.number)
        {
            if(!path.ContainsKey(sum_weight)) path.Add(sum_weight,string_temp);
            return true;
        }
        if (A.visited) {return false; };
        A.visited = true;
        bool temp_bool=false;
        foreach(var adj in adj_list[A.number-1])
        {            
            if (!adj.visited)
            {
                sum_weight += FindWeightEdge(A, adj);
                string_temp += adj.number.ToString();
                                              
                var reach=DFS(adj_list, adj, B, ref path, ref sum_weight, ref string_temp);
                if (reach) temp_bool=reach;
                sum_weight -= FindWeightEdge(A, adj);
                if (string_temp.Length - 1 >= 0) string_temp = string_temp.Substring(0, string_temp.Length - 1);
            }
        }
        if (temp_bool) return true;       
        return false;
    }
    /// <summary>
    /// Метод поиска BFS
    /// </summary>
    /// <param name="adj_list">Список смежности</param>
    /// <param name="A">Точка А</param>
    /// <param name="B">Точка B</param>
    /// <param name="path">Словарь для пути</param>
    /// <param name="sum_weight">Вес</param>
    /// <param name="string_temp">Строка для содержания пути</param>
    /// <returns></returns>
    public bool BFS(List<List<Vertex>> adj_list, Vertex A, Vertex B, ref Dictionary<int, string> path, ref int sum_weight, ref string string_temp)
    {
        Queue<Vertex> queue=new Queue<Vertex>();
        queue.Enqueue(A);
        A.visited = true;
        while(queue.Count > 0)
        {          
                var v = queue.Dequeue();
                foreach (var adj in adj_list[v.number - 1])
                {
                    if (!adj.visited)
                    {
                        sum_weight += FindWeightEdge(A, adj);
                        string_temp += adj.number.ToString();
                        queue.Enqueue(adj);
                        adj.visited = true;
                    if (adj.number == B.number) {
                        if (!path.ContainsKey(sum_weight)) path.Add(sum_weight, string_temp);
                        return true;
                    }                    
                        sum_weight -= FindWeightEdge(A, adj);
                        if (string_temp.Length - 1 >= 0) string_temp = string_temp.Substring(0, string_temp.Length - 1);
                    }
                }            
        }
        return false;
    }
    /// <summary>
    /// Меод вывода матрицы смежности
    /// </summary>
    public void PrintMatrix()
    {
        Console.WriteLine("\nМатрица смежности: ");
        var matrix = this.GetAdjMatrix();
       
        for (int i = 0; i < this.vertex_count; i++)
        {
            Console.Write("{0,2}|", i + 1);
            for (int j = 0; j < this.vertex_count; j++)
            {
                Console.Write("{0,3}", matrix[i, j] + " ");
            }

            Console.WriteLine();
        }
        Console.Write("   ");
        for (int j = 0; j < this.vertex_count; j++)
        {
            Console.Write("{0,2}|", j + 1);
        }       
    }
    /// <summary>
    /// Метод вывода матрицы инцидентности
    /// </summary>
    public void PrintIncMatrix()
    {
        Console.WriteLine("\n\nМатрица инцидентности: ");
        var matrix_inc = this.GetIncMatrix();
        for (int i = 0; i < this.vertex_count; i++)
        {
            Console.Write("{0,2}|", i + 1);
            for (int j = 0; j < this.edge_count; j++)
            {
                Console.Write("{0,5}", matrix_inc[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.Write("   ");
        for (int j = 0; j < this.edge_count; j++)
        {
            Console.Write("{0,4}|", j + 1);
        }
        Console.WriteLine();
    }
    /// <summary>
    /// Метод вывода списка смежности
    /// </summary>
    public void PrintAdjList()
    {
        Console.WriteLine("\n\nСмежный список: ");
        var list = this.GetAdjList();
        for (int i = 0; i < this.vertex_count; i++)
        {            
            var text = $"{i + 1}: ";
            for (int j = 0; j < list[i].Count; j++)
            {
                text+=/*"{0,5}", */list[i][j].number + ", ";
            }

            Console.Write("{0,5}|", text);
        }
        Console.Write("   ");
    }
    /// <summary>
    /// Метод вывода списка рёбер
    /// </summary>
    public void PrintEdgeList()
    {
        Console.WriteLine("\n\nCписок рёбер: ");
        var list = this.GetEdgeList();
        for (int i = 0; i < this.edge_count; i++)
        {
            Console.Write("{0}: {1}-{2}|",i+1, list[i].from.number, list[i].to.number);
        }
        Console.Write("   ");
    }


}
