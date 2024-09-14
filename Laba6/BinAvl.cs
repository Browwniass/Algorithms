using System.Diagnostics;
using System.Threading;

const int TEST_AMMOUNT = 7;
const int SUBTEST_AMMOUNT = 10;
int stackSize = 1000000000;
long freq = Stopwatch.Frequency; //частота таймера
Thread T = new Thread(Testing, stackSize);//запуск потока
T.Start();
/// <summary>
/// Метод для тестирования
/// </summary>
void Testing()
{
    Stopwatch stopwatch1 = new Stopwatch();
    //подключение файлов для вывода замеров
    StreamWriter bin_sort_log = new StreamWriter("bin_sort.txt");
    StreamWriter bin_unsort_log = new StreamWriter("bin_unsort.txt");
    StreamWriter avl_sort_log = new StreamWriter("avl_sort.txt");
    StreamWriter avl_unsort_log = new StreamWriter("avl_unsort.txt");
    StreamWriter array_sort_log = new StreamWriter("array_sort.txt");
    StreamWriter array_unsort_log = new StreamWriter("array_unsort.txt");
    for (int t = 0; t < TEST_AMMOUNT; t++)
    {
        bin_sort_log.WriteLine($"Test - {t + 1}");
        bin_unsort_log.WriteLine($"Test - {t + 1}");
        avl_sort_log.WriteLine($"Test - {t + 1}");
        avl_unsort_log.WriteLine($"Test - {t + 1}");
        array_sort_log.WriteLine($"Test - {t + 1}");
        array_unsort_log.WriteLine($"Test - {t + 1}");

        Random rnd = new Random();

        for (int r = 0; r < SUBTEST_AMMOUNT; r++)
        {
            int[] unsort_array = new int[(int)Math.Pow(2, 10 + t)];//несортированный массив
            int[] sort_array = new int[(int)Math.Pow(2, 10 + t)];//сортированный массив
            
            //Опеределение Деревьев
            AVL avl_unsort_tree = new AVL();
            BinTree bin_unsort_tree = new BinTree();
            AVL avl_sort_tree = new AVL();
            BinTree bin_sort_tree = new BinTree();

            for (int i = 0; i < unsort_array.Length; i++)//заполнение массива
            {
                unsort_array[i] = rnd.Next(0, unsort_array.Length + 1);
                sort_array[i] = i;
            }

            bin_sort_log.Write($"\nTimeGeneration: ");
            bin_unsort_log.Write($"\nTimeGeneration: ");
            avl_sort_log.Write($"\nTimeGeneration: ");
            avl_unsort_log.Write($"\nTimeGeneration: ");
            array_sort_log.Write($"\nTimeGeneration: ");
            array_unsort_log.Write($"\nTimeGeneration: ");

            //Заполнение Бинарного дерева несортированным массивом
            stopwatch1.Start();
            for (int i = 0; i < unsort_array.Length; i++)
            {
                bin_unsort_tree.Add(unsort_array[i]);
            }
            stopwatch1.Stop();
            bin_unsort_log.WriteLine((double)stopwatch1.ElapsedTicks/freq);
            //Заполнение AVL-дерева несортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < unsort_array.Length; i++)
            {
                avl_unsort_tree.Add(unsort_array[i]);
            }
            stopwatch1.Stop();
            avl_unsort_log.WriteLine((double)stopwatch1.ElapsedTicks / freq);
            //Заполнение Бинарного дерева сортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < unsort_array.Length; i++)
            {
                bin_sort_tree.Add(sort_array[i]);
            }
            stopwatch1.Stop();
            bin_sort_log.WriteLine((double)stopwatch1.ElapsedTicks / freq);
            //Заполнение AVL-дерева сортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < unsort_array.Length; i++)
            {
                avl_sort_tree.Add(sort_array[i]);
            }
            stopwatch1.Stop();
            avl_sort_log.WriteLine((double)stopwatch1.ElapsedTicks / freq);

            bin_sort_log.Write("\nTimeFind: ");
            bin_unsort_log.Write("\nTimeFind: ");
            avl_sort_log.Write("\nTimeFind: ");
            avl_unsort_log.Write("\nTimeFind: ");
            array_sort_log.Write("\nTimeFind: ");
            array_unsort_log.Write("\nTimeFind: ");
            
            //Поиск случайного узла в Бинарном дереве, заполненном несортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)//заполнение деревьев
            {
                bin_unsort_tree.Find(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            bin_unsort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq)/1000);
            //Поиск случайного узла в AVL-дереве, заполненном несортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)
            {
                avl_unsort_tree.Find(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            avl_unsort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);
            //Поиск случайного узла в Бинарном дереве, заполненном сортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)
            {
                bin_sort_tree.Find(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            bin_sort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);
            //Поиск случайного узла в AVL-дереве, заполненном сортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)
            {
                avl_sort_tree.Find(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            avl_sort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);
            //Поиск случайного узла несортированном массиве
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)//заполнение деревьев
            {
                sort_array.Contains(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            array_sort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);
            //Поиск случайного узла в сортированном массиве
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)//заполнение деревьев
            {
                unsort_array.Contains(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            array_unsort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);


            bin_sort_log.Write("\nTimeDelete: ");
            bin_unsort_log.Write("\nTimeDelete: ");
            avl_sort_log.Write("\nTimeDelete: ");
            avl_unsort_log.Write("\nTimeDelete: ");
            array_sort_log.Write("\nTimeDelete: ");
            array_unsort_log.Write("\nTimeDelete: ");
           
            //Удаление случайного узла в Бинарном дереве, заполненном несортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)//заполнение деревьев
            {
                bin_unsort_tree.Delete(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            bin_unsort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);
            //Удаление случайного узла в AVL-дереве, заполненном несортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)
            {
                avl_unsort_tree.Delete(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            avl_unsort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);
            //Удаление случайного узла в Бинарном дереве, заполненном сортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)
            {
                bin_sort_tree.Delete(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            bin_sort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);
            //Удаление случайного узла в AVL-дереве, заполненном сортированным массивом
            stopwatch1.Restart();
            for (int i = 0; i < 1000; i++)
            {
                avl_sort_tree.Delete(rnd.Next(0, unsort_array.Length + 1));
            }
            stopwatch1.Stop();
            avl_sort_log.WriteLine(((double)stopwatch1.ElapsedTicks / freq) / 1000);
            stopwatch1.Reset();

        }
    }
    avl_sort_log.Close();
    avl_unsort_log.Close();
    bin_sort_log.Close();
    bin_unsort_log.Close();
    array_sort_log.Close();
    array_unsort_log.Close();
    //AVL avl = new AVL();
    //BinTree tree = new BinTree();
    //avl.Add(6);
    //avl.Add(9);
    //avl.Add(7);
    //avl.Add(10);
    //avl.Add(8);
    //avl.Add(4);
    //avl.Add(2);
    //avl.Add(1);
    //avl.Add(3);
    //avl.Add(5);
    //avl.Delete(4);
    //avl.PrintTree();
}


/// <summary>
/// Бинарное дерево
/// </summary>
class BinTree
{
    public class Node
    {
        public int d_value;
        public Node left;
        public Node right;
        public Node(int value)
        {
            this.d_value = value;
        }
    }
    Node root;//узлы
    //public BinTree()
    //{
    //}
    /// <summary>
    /// Метод добавления элемента в дерево
    /// </summary>
    /// <param name="value">Добавляемый узел</param>
    public void Add(int value)
    {
        Node new_item = new Node(value);
        if (root == null)//если элемент первый
        {
            root = new_item;
        }
        else
        {
            root = insertRoot(root, new_item);
        }
    }
    /// <summary>
    /// Рекурентное добавление нового узла в дерево
    /// </summary>
    /// <param name="current">Текущий узел</param>
    /// <param name="n">Новый узел</param>
    /// <returns>Дерево с новым узлом</returns>
    public virtual Node insertRoot(Node current, Node n)
    {
        if (current == null)//новый узел, если ещё нет
        {
            current = n;
            return current;
        }
        else if (n.d_value < current.d_value)
        {
            current.left = insertRoot(current.left, n);

        }
        else if (n.d_value > current.d_value)
        {
            current.right = insertRoot(current.right, n);

        }
        return current;
    }
    /// <summary>
    /// Удаление корня
    /// </summary>
    /// <param name="target"></param>
    public void Delete(int target)
    {
        root = Delete(root, target);
    }
    /// <summary>
    /// Рекурентное удаление выбранного узла 
    /// </summary>
    /// <param name="current">Текущий дерево</param>
    /// <param name="target">Удаляемый корень</param>
    /// <returns>Дерево с удаленным корнем</returns>
    public virtual Node Delete(Node current, int target)
    {
        Node parent;
        if (current == null) return null;
        else
        {
            //при поиске узла меньше
            if (target < current.d_value)
            {
                current.left = Delete(current.left, target);

            }
            //при поиске узел больше
            else if (target > current.d_value)
            {
                current.right = Delete(current.right, target);

            }
            //когда узел найден найден
            else
            {
                if (current.right != null)
                {
                    //если есть правый потомок, заменяем самым левым
                    parent = current.right;
                    while (parent.left != null)
                    {
                        parent = parent.left;
                    }
                    current.d_value = parent.d_value;
                    current.right = Delete(current.right, parent.d_value);

                }
                //если есть левый потомок, заменяем им
                else if (current.left != null)
                {   //if current.left != null
                    return current.left;
                }
                //если потомков нет, так и оставляем
                else
                {
                    return null;
                }
            }
        }
        return current;
    }
    /// <summary>
    /// Метод нахождения узла
    /// </summary>
    /// <param name="el">узел</param>
    public void Find(int el)
    {
        Node find = Find(root, el);
        if (find != null && find.d_value == el)
        {
            //Console.WriteLine("{0} was found!", el);
        }
        else
        {
            //Console.WriteLine("Nothing found!");
        }
    }
    /// <summary>
    /// Рекурсивный метод поиска узла в дереве
    /// </summary>
    /// <param name="current">Текущий узел</param>
    /// <param name="target">Разыскиваемый узел</param>
    /// <returns></returns>
    private Node Find(Node current, int target)
    {
        if (current == null) return null;
        if (target < current.d_value)
        {
            if (target == current.d_value)
            {
                return current;
            }
            else
                return Find(current.left, target);
        }
        else
        {
            if (target == current.d_value)
            {
                return current;
            }
            else
                return Find(current.right, target);
        }

    }
    /// <summary>
    /// Вывод дерева в консоль 
    /// </summary>
    public void PrintTree()
    {
        if (root == null)
        {
            Console.WriteLine("Дерево пусто");
            return;
        }
        int depth = 0;
        printTreeOrder(root, depth);
        Console.WriteLine();
    }
    /// <summary>
    /// Вывод дерева слева-направо
    /// </summary>
    /// <param name="current"></param>
    private void printTreeOrder(Node current, int depth)
    {
        if (current != null)
        {
            depth++;
            printTreeOrder(current.left, depth);
            Console.Write($"{depth}-({current.d_value})\n");
            printTreeOrder(current.right, depth);
        }
    }
}

/// <summary>
/// AVL-дерево
/// </summary>
class AVL : BinTree
{
    /// <summary>
    /// Рекурентное добавление нового узла в дерево с балансировкой
    /// </summary>
    /// <param name="current">Текущий узел</param>
    /// <param name="n">Новый узел</param>
    /// <returns>Дерево с новым узлом</returns>
    public override Node insertRoot(Node current, Node n)
    {
        if (current == null)
        {
            current = n;
            return current;
        }
        else if (n.d_value < current.d_value)
        {
            current.left = insertRoot(current.left, n);
            current = balance(current);//балансировка
        }
        else if (n.d_value > current.d_value)
        {
            current.right = insertRoot(current.right, n);
            current = balance(current);
        }
        return current;
    }
    /// <summary>
    /// Балансировка Бинарного дерева
    /// </summary>
    /// <param name="current">Текущий узел</param>
    /// <returns>Сбалансированный узел</returns>
    public Node balance(Node current)
    {
        int b_factor = BalanceFactor(current);
        if (b_factor > 1)//если по левой
        {
            if (BalanceFactor(current.left) > 0)//если по левой и левое поддерево больше
            {
                current = rotateSR(current);
            }
            else//если по левой и правое поддерево больше
            {
                current = rotateBL(current);
            }
        }
        else if (b_factor < -1)//если по правой
        {
            if (BalanceFactor(current.right) > 0)
            {
                current = rotateBR(current);//если по правой и левое поддерево больше
            }
            else
            {
                current = rotateSL(current);////если по правой и правое поддерево больше
            }
        }
        return current;
    }
    /// <summary>
    /// Рекурентное удаление выбранного узла с балансировкой
    /// </summary>
    /// <param name="current">Текущий дерево</param>
    /// <param name="target">Удаляемый корень</param>
    /// <returns>Дерево с удаленным корнем</returns>
    public override Node Delete(Node current, int target)
    {
        Node parent;
        if (current == null) return null;
        else
        {
            //при поиске узла меньше
            if (target < current.d_value)
            {
                current.left = Delete(current.left, target);
                current = balance(current);
            }
            //при поиске узла больше
            else if (target > current.d_value)
            {
                current.right = Delete(current.right, target);
                current = balance(current);
            }
            //когда узел найден
            else
            {
                if (current.right != null)
                {
                    //если есть правый потомок, заменяем самым левым
                    parent = current.right;
                    while (parent.left != null)
                    {
                        parent = parent.left;
                    }
                    current.d_value = parent.d_value;
                    current.right = Delete(current.right, parent.d_value);
                    current = balance(current);
                }
                else if (current.left != null)//если есть левый потомок, заменяем им
                {   //if current.left != null
                    return current.left;
                }
                else//если потомков нет, так и оставляем
                {
                    return null;
                }
            }
        }
        return current;
    }
    /// <summary>
    /// Получение высоты
    /// </summary>
    /// <param name="current">Текущий узел</param>
    /// <returns>Величина высоты</returns>
    private int getHeight(Node current)
    {
        int height = 0;
        if (current != null)
        {
            int l = getHeight(current.left);//по левой стороне
            int r = getHeight(current.right);//по правой стороне
            int m = Math.Max(l, r);
            height = m + 1;
        }
        return height;
    }
    /// <summary>
    /// Фактор балансировки
    /// </summary>
    /// <param name="current">Текущий узел</param>
    /// <returns>Значения фактора балансировки</returns>
    private int BalanceFactor(Node current)
    {
        int l = getHeight(current.left);
        int r = getHeight(current.right);
        int b_factor = l - r;
        return b_factor;
    }
    /// <summary>
    /// Меод малого левого поворота
    /// </summary>
    /// <param name="parent">Узел-Родитель</param>
    /// <returns>Дерево</returns>
    private Node rotateSL(Node parent)
    {
        Node pivot = parent.right;
        parent.right = pivot.left;
        pivot.left = parent;
        return pivot;
    }
    /// <summary>
    /// Малый правый поворот
    /// </summary>
    /// <param name="parent">Узел-родитель</param>
    /// <returns>Дерево</returns>
    private Node rotateSR(Node parent)
    {
        Node pivot = parent.left;
        parent.left = pivot.right;
        pivot.right = parent;
        return pivot;
    }
    /// <summary>
    /// Большой левый поворот
    /// </summary>
    /// <param name="parent">Узел-родитель</param>
    /// <returns>Дерево</returns>
    private Node rotateBL(Node parent)
    {
        Node pivot = parent.left;
        parent.left = rotateSL(pivot);
        return rotateSR(parent);
    }
    /// <summary>
    /// Большой правый поворот
    /// </summary>
    /// <param name="parent">Узел-родитель</param>
    /// <returns>Дерево</returns>
    private Node rotateBR(Node parent)
    {
        Node pivot = parent.right;
        parent.right = rotateSR(pivot);
        return rotateSL(parent);
    }
}
