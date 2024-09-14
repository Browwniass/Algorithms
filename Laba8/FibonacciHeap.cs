namespace Fib_Hip
{
        


        /// <summary>
        /// Фибоначчиева куча
        /// </summary>
        public class FibonacciHeap
        {
            /// <summary>
            /// Узлы Кучи Фибоначчи
            /// </summary>
            public class Node
            {
                public int key;//ключ
                public Node right;//правый узел
                public Node left;//левый узел
                public bool mark;//был ли удален в процессе изменения ключа ребенок этой вершины)
                public int degree;//степень вершины
                public Node child;//ребенок
                public Node parent;//родитель
                public Node(int key)
                {
                    right = this;
                    left = this;
                    this.key = key;
                }

            }
        private int min_key;//значения минимума
            private Node min_roote;//стартовый(минимальный) узел
            private int size;

            /// <summary>
            /// Инициализация нового дерева кучи
            /// </summary>
            /// <param name="x">Первый ключ</param>
            public FibonacciHeap(int x)
            {
                this.min_key = x;
            }
            /// <summary>
            /// Интерфейс для вставки
            /// </summary>
            /// <param name="x">Вставляемое значение</param>
            public void Insert(int x)
            {
                Insert(new Node(x));//создание нового узла x
            }


            /// <summary>
            /// Вставка нового значения
            /// </summary>
            /// <param name="node">Вставляемое дерево</param>
            public void Insert(Node node)
            {
                // добавка данного поддерева в min_node
                if (min_roote != null)//проверка кучи на пустоту
                {
                    node.left = min_roote;
                    node.right = min_roote.right;
                    min_roote.right = node;
                    node.right.left = node;

                    if (node.key < min_roote.key)//меняем минимум, если новый ключ меньше
                    {
                        min_roote = node;
                    }
                }
                else//если да, вставляем в min_node
                {
                    min_roote = node;
                }
                size++;
            }

            /// <summary>
            /// Нахождение минимума
            /// </summary>
            /// <returns>Минимальный узел</returns>
            public Node FindMin()
            {
                return min_roote;
            }

            /// <summary>
            /// Изъятия минимума
            /// </summary>
            /// <returns>Изъятый минимум</returns>
            public Node RemoveMin()
            {
                Node min_node = this.min_roote;

                if (min_node != null)
                {
                    int num_kids = min_node.degree;//сохраняем степень
                    Node old_min_child = min_node.child;//сохраняем детей удаляемоего узла

                    // Добавляем всех детей в верхний root
                    while (num_kids > 0)
                    {
                        Node temp_right = old_min_child.right;

                      // изымаем old_min_child из списка детей, приравнивая друг-друга
                        old_min_child.left.right = old_min_child.right;
                        old_min_child.right.left = old_min_child.left;

                        // добавляем old_min_child к верхнему списку
                        old_min_child.left = this.min_roote;
                        old_min_child.right = this.min_roote.right;
                        this.min_roote.right = old_min_child;
                        old_min_child.right.left = old_min_child;

                        
                        old_min_child.parent = null;//учитываем, что родителя теперь нет
                        old_min_child = temp_right;
                        num_kids--;
                    }

                    // изымаем min_node из списка
                    min_node.left.right = min_node.right;
                    min_node.right.left = min_node.left;
                    //если один элемент
                    if (min_node == min_node.right)
                    {
                        this.min_roote = null;
                    }
                    else// пока что перекинем указатель min на правого сына, а далее consolidate() скорректирует min в процессе выполнения
                    {
                            this.min_roote = min_node.right;
                            Consolidate();
                        }
                        size--;
                    }

                return min_node;
            }
    
            /// <summary>
            /// Уплотнение кучи
            /// </summary>
            private void Consolidate()
            {
                //кол-во вершин в новой куче
                int arraySize = (int)Math.Floor(Math.Log(size) * 1.0 / Math.Log((1.0 + Math.Sqrt(5.0)) / 2.0)) + 1;

                var array = new List<Node>(arraySize);

                // Инициализция массива array
                for (var i = 0; i < arraySize; i++)
                {
                    array.Add(null);
                }

                // поиск кол-ва вершин
                var num_roots = 0;
                Node x = min_roote;

                if (x != null)
                {
                    num_roots++;
                    x = x.right;

                    while (x != min_roote)
                    {
                        num_roots++;
                        x = x.right;
                    }
                }

                // Для каждого узла в списка
                while (num_roots > 0)
                {               
                    int d = x.degree;//степень данной вершины
                    Node next = x.right;

                    // поиск других с такой же степенью
                    for (; ; )
                    {
                        Node y = array[d];
                        if (y == null)//если нет
                        {
                            break;
                        }

                        //Делаем того, кто меньше, ребенком того, кто больше
                        if (x.key > y.key)
                        {
                            (x, y) = (y, x);
                        }
                        //связываем родителя и ребенка
                        Link(y, x);

                        // Переходим к другой степени
                        array[d] = null;
                        d++;
                    }

                    // Сохраяем найденный узел
                    array[d] = x;

                    // Сдвигаемся по списку вправо.
                    x = next;
                    num_roots--;
                }

                // собираем min_roote из array[], зануляя перед этим оригинальный min_roote
                min_roote = null;

                for (var i = 0; i < arraySize; i++)
                {
                    Node y = array[i];
                    if (y == null)
                    {
                        continue;
                    }
                    
                    if (min_roote != null)
                    {
                        // Изымаем старые узлы из root
                        y.left.right = y.right;
                        y.right.left = y.left;

                        // вставляем 
                        y.left = min_roote;
                        y.right = min_roote.right;
                        min_roote.right = y;
                        y.right.left = y;

                        // Проверяем не меньше ли
                        if (y.key < min_roote.key)
                        {
                            min_roote = y;
                        }
                    }
                    else
                    {
                        min_roote = y;
                    }
                }
            }

            

            /// <summary>
            /// Присоединяем родителя к ребенку
            /// </summary>
            /// <param name="new_child">Новый ребенок</param>
            /// <param name="new_parent">Новый родитель</param>
            private static void Link(Node new_child, Node new_parent)
            {
                // изъятие new_сhild из списка детей
                new_child.left.right = new_child.right;
                new_child.right.left = new_child.left;

                // делаем new_child ребенком new_parent
                new_child.parent = new_parent;

                if (new_parent.child == null)
                {
                    new_parent.child = new_child;
                    new_child.right = new_child;
                    new_child.left = new_child;
                }
                else
                {
                    new_child.left = new_parent.child;
                    new_child.right = new_parent.child.right;
                    new_parent.child.right = new_child;
                    new_child.right.left = new_child;
                }

                new_parent.degree++;

                new_child.mark = false;
            }
        }
    
}