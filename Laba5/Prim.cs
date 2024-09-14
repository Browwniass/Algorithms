// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;
const int AMMOUNT = 1;//кол-во тстовC
int[] EDGE_COUNT = { 3, 4, 10, 20 };//вероятные рёбра графа
int[] VERT_COUNT = { 10, 20, 50, 100 };//все вершины графа
StreamWriter f = new StreamWriter("test1.txt");
Stopwatch stopwatch1 = new Stopwatch();
//тест
for (int k = 0; k < VERT_COUNT.Length; k++)
{
    f.WriteLine("Вершин: " + VERT_COUNT[k]);
    for (int i = 0; i < AMMOUNT; i++)
    {       
        var graph = RandomGraphGen(VERT_COUNT[k], k);
        Console.WriteLine("\nМатрица смежности: ");
        graph.PrintMatrix();
        stopwatch1.Start();
        var tree = graph.Prim(VERT_COUNT[k]);
        stopwatch1.Stop();
        Console.WriteLine("\nМинимальное островное дерево: ");
        tree.PrintMatrix();
        f.WriteLine(stopwatch1.ElapsedMilliseconds.ToString());
    }
}
f.Close();

/// <summary>
/// Метод, возвращающий рандомно-сгенерированный граф
/// </summary>
/// <param name="vert">Кол-во вершин</param>
/// <param name="test_number">Номер теста</param>
/// <returns>Рандомно-сгенерированный граф</returns>
/// <exception cref="Exception"></exception>
Graph RandomGraphGen(int vert, int test_number)
{
    var graph = new Graph(false, vert);//создание экземпляра графа
    Random rnd = new Random();
    int j_temp = 0;//подсчет последнего j-того в строке
    for (int i = 0; i < vert; i++)//прохождение по каждой вершине, где на каждой вершине будет создано n-ное связей ребер и каждая новая вершина будет сдвигаться вправо относительно прошлой, чтобы у всех элементов был доступ к друг другу
    {
        int edge = rnd.Next(EDGE_COUNT[test_number], vert+1);//Выбор кол-ва рёбер у графа в диапазоне от минимального кол-ва ребер до кол-ва вершин
        int count_edge = 0; //подсчет ребёр
        for (int j = j_temp; j < vert && count_edge < edge; j++)//прохождение по строке, где новая вершина стартует с того же места, где была последняя прошлая
        {
            if (graph.adj_matrix[i, j] == 0 && j != i)//если пустая и не равна самой себе
            {
                graph.AddEdge(i+1,j+1, rnd.Next(1, 21));
                count_edge++;
                j_temp = j;
            }
        }
        if (count_edge < edge)//если вправо больше нельзя, но кол-во вершин недонабрано
        {
            for (int j = 0; j < vert && count_edge < edge; j++)
            {
                if (graph.adj_matrix[i, j] == 0 && j != i)
                {
                    graph.AddEdge(i + 1, j + 1, rnd.Next(1, 21));
                    count_edge++;
                }
            }
        }
    }
    return graph;    
}
/// <summary>
/// Класс:Граф
/// </summary>
class Graph
{
    public int vertex_count = 0;
    public int edge_count = 0;
    public int[,] adj_matrix;
    public bool oriented;
    public Graph(bool oriented, int vertex_count)
    {
        this.oriented = oriented;
        this.vertex_count = vertex_count;
        //this.edge_count = edge_count;
        this.adj_matrix = new int[vertex_count, vertex_count];
    }


    /// <summary>
    /// Метод добавления связи в Граф
    /// </summary>
    /// <param name="from">Вершина От</param>
    /// <param name="to">Вершина Куда</param>
    /// <param name="weight">Вec</param>
    public void AddEdge(int from, int to, int weight)
    {
        adj_matrix[from-1,to-1] = weight;
        if (!oriented) adj_matrix[to - 1, from - 1] = weight;
    }
    /// <summary>
    /// Метод минимального островного дерева. Прим
    /// </summary>
    /// <param name="vert">Кол-во вершин</param>
    /// <returns>Дерево</returns>
    public Graph Prim(int vert)
    {
        var tree = new Graph(false, vert);
        List<int> f_vert = new List<int>();//список для хранения уже найденных вершин
        f_vert.Add(0);//добавление первой вершины
        while (f_vert.Count < vert)
        {
            int min_i = -1;
            int min_j = -1;
            int min_weight=1000;
            foreach (var v in f_vert)
            {
                for(int j=0; j<vert; j++)
                {
                    if (adj_matrix[v, j] > 0 && adj_matrix[v,j] < min_weight && adj_matrix[v,j]>0 && !f_vert.Contains(j))//поиск минимального ребра
                    {
                        min_weight = adj_matrix[v, j];
                        min_i = v;
                        min_j = j;  
                    }
                }
            }
            Console.Write($"\nI={min_i+1}, J={min_j+1} weight={min_weight}");
            f_vert.Add(min_j);//добавление вершины с минимальным ребром
            tree.AddEdge(min_i+1, min_j+1, min_weight);//добавление связи по минимальному ребру
        }
        return tree;
    }
    /// <summary>
    /// Меод вывода матрицы смежности
    /// </summary>
    public void PrintMatrix()
    {
        for (int i = 0; i < this.vertex_count; i++)
        {
            Console.Write("{0,2}|", i + 1);
            for (int j = 0; j < this.vertex_count; j++)
            {
                Console.Write("{0,3}", adj_matrix[i, j] + " ");
            }

            Console.WriteLine();
        }
        Console.Write("   ");
        for (int j = 0; j < this.vertex_count; j++)
        {
            Console.Write("{0,2}|", j + 1);
        }
    }
}
