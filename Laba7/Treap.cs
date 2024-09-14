namespace Treap_Lib
{
    /// <summary>
    /// Класс Декартова дерева
    /// </summary>
    public class Treap
    {
        /// <summary>
        /// Узлы Декартова дерева
        /// </summary>
        public class Node
        {
            public int x;//ключ
            public int y;//приоритет

            public Node Left;//левый потомок
            public Node Right;//правый потомок


            public Node(int x, int y, Node left = null, Node right = null)
            {
                this.x = x;
                this.y = y;
                this.Left = left;
                this.Right = right;
            }
            /// <summary>
            /// Рекурсивное Соедининие двух Поддеревьем 
            /// </summary>
            /// <param name="L">Левое</param>
            /// <param name="R">Правое</param>
            /// <returns>Соединенное дерево</returns>
            public static Node Merge(Node L, Node R)
            {
                //провекра на потомков
                if (L == null) return R;
                if (R == null) return L;
                //поиск
                if (L.y > R.y)//если левый приоритет больше
                {
                    var newR = Merge(L.Right, R);//ищем новое правое поддерево
                    return new Node(L.x, L.y, L.Left, newR);
                }
                else//если правый приоритет больше
                {
                    var newL = Merge(L, R.Left);//ищем новое левое поддерево
                    return new Node(R.x, R.y, newL, R.Right);
                }
            }
            /// <summary>
            /// Рекурсивное Разделение дерева на два поддерева, где все значения одно меньше всех знач другого
            /// </summary>
            /// <param name="x">Ключ для сравнения</param>
            /// <param name="L">Возвращаемое Левое поддерево</param>
            /// <param name="R">Возвращаемое Правое поддерево</param>
            public void Split(int x, out Node L, out Node R)
            {
                Node newTree = null;//создание нового дерева
                if (this.x <= x)//если у дерева T ключ меньше или равен x
                {
                    if (Right == null)
                        R = null;
                    else
                        Right.Split(x, out newTree, out R);
                    L = new Node(this.x, y, Left, newTree);//левое поддерево, где исключены R, превыщающие x
                }
                else//если у дерева T ключ больше x
                {
                    if (Left == null)
                        L = null;
                    else
                        Left.Split(x, out L, out newTree);
                    R = new Node(this.x, y, newTree, Right);//правое поддерево, где исключены L, превыщающие x
                }
            }
            /// <summary>
            /// Рекурсивное Добавление элемента 
            /// </summary>
            /// <param name="x">Добавляемый элемент</param>
            /// <returns>Дерево с добавленным элементом</returns>
            public Node Add(int x)
            {
                Random rand = new Random();
                Node l, r;
                Split(x, out l, out r);//разделение на две половины
                Node m = new Node(x, rand.Next());//дерево m из единственной вершины с новым ключом
                return Merge(Merge(l, m), r);//поочередное объединение с l, а потом с r
            }
            /// <summary>
            /// Рекурсивное Удаление элемента
            /// </summary>
            /// <param name="x">Удаляемый элемент</param>
            /// <param name="root">Дерево</param>
            /// <returns>Дерево с удаленным элементом</returns>
            public Node Remove(int x, Node root)
            {
                Node l, m, r;
                Split(x - 1, out l, out r);//разделение дерева на две области так, чтобы направо перенесся удаляемый элемент
                if (r != null)
                {
                    r.Split(x, out m, out r);//Разделение правой части на две, где из-за x-1, нам вернется r с удаленным ключом
                    return Merge(l, r);//соединение l и r
                }
                else return root;
            }

        }
        public Node root;
        /// <summary>
        /// Конструктор для Декартова дерева
        /// </summary>
        /// <param name="x">ключ</param>
        /// <param name="y">приоритет</param>
        /// <param name="left">левый потомок</param>
        /// <param name="right">правый потомок</param>
        public Treap(int x, int y, Node left = null, Node right = null)
        {
            this.root = new Node(x, y, left, right);
        }
        /// <summary>
        /// Добавление нового ключа в дерево
        /// </summary>
        /// <param name="x">ключ</param>
        public void Add(int x)
        {
            this.root = this.root.Add(x);
        }
        /// <summary>
        /// Удаление ключа из дерева
        /// </summary>
        /// <param name="x">ключ</param>
        public void Remove(int x)
        {
            this.root = this.root.Remove(x, this.root);
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
                //Console.Write("\n");
                depth++;
                printTreeOrder(current.Left, depth);
                Console.Write($"{depth}-({current.x}:{current.y}) ");
                printTreeOrder(current.Right, depth);

            }
        }

        /// <summary>
        /// Метод нахождения узла
        /// </summary>
        /// <param name="el">узел</param>
        public void Find(int el)
        {
            Node find = Find(root, el);
            if (find != null && find.x == el)
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
            if (target < current.x)
            {
                if (target == current.x)
                {
                    return current;
                }
                else
                    return Find(current.Left, target);
            }
            else
            {
                if (target == current.x)
                {
                    return current;
                }
                else
                    return Find(current.Right, target);
            }

        }
        /// <summary>
        /// Нахождение глубины Деррева
        /// </summary>
        /// <returns>Максимальная глубина</returns>
        public int findDepth()
        {
            if (root == null)
            {
                //Console.WriteLine("Дерево пусто");
                return -1;
            }
            int depth = 0;
            int depth_max = 0;
            depthTreeOrder(root, depth, ref depth_max);
            return depth_max;

        }
        /// <summary>
        /// Рекурсивное прохождение по массиву в поисках глубины
        /// </summary>
        /// <param name="current">текущий узел</param>
        /// <param name="depth">счётчик глубины</param>
        /// <param name="depth_max">максимальная глубина</param>
        private void depthTreeOrder(Node current, int depth, ref int depth_max )
        {
            if (current != null)
            {
                depth++;
                if (depth_max < depth) depth_max = depth;
                depthTreeOrder(current.Left, depth, ref depth_max);
                depthTreeOrder(current.Right, depth, ref depth_max);

            }
        }

        /// <summary>
        /// Нахождение глубины для каждой ветки дерева
        /// </summary>
        /// <returns>Список с найденными глубинами</returns>
        public List<int> findDepthEach()
        {
            if (root == null)
            {
                //Console.WriteLine("Дерево пусто");
                return null;
            }
            List<int> depth_arr = new List<int>() ;
            int depth = 0;
            depthEachTreeOrder(root, depth, ref depth_arr);
            return depth_arr;

        }
       /// <summary>
       /// Рекурсивное нахэождение глубины для каждой ветки дерева
       /// </summary>
       /// <param name="current">теущий узел</param>
       /// <param name="depth">счётчик глубины</param>
       /// <param name="depth_arr">список для сохранения значений</param>
        private void depthEachTreeOrder(Node current, int depth, ref List<int> depth_arr)
        {
            if (current != null)
            {
                depth++;
                depthEachTreeOrder(current.Left, depth, ref depth_arr);
                if(!depth_arr.Contains(depth)) depth_arr.Add(depth);
                depthEachTreeOrder(current.Right, depth, ref depth_arr);

            }
        }
    }
}