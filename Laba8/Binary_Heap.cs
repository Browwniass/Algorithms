namespace Binar_Hip
{
    public class Bin_Hip
    {
        private List<int> root;
        /// <summary>
        /// Упорядочивание при инициализации
        /// </summary>
        /// <param name="el"></param>
        public Bin_Hip(int[] el = null)
        {
            if (el != null)
            {
                this.root = new List<int>(el);
                for (int i = el.Length / 2; i >= 0; i--)
                {
                    this.SiftDown(i);
                }
            }
            else
            {
                this.root = new List<int>();
            }
        }

        /// <summary>
        /// Изъятие минимального
        /// </summary>
        /// <returns>Изъятиый минимальный элемент</returns>
        public int RemoveMin()
        {
            var min = this.root[0];//Сохраняем первое значение
            if (this.root.Count - 1 > 0)
            {
                this.root[0] = this.root[this.root.Count - 1];//Устанавливаем на первое значение последнее
                this.root.RemoveAt(this.root.Count - 1);//извлекаем последнее

                if (this.root.Count > 0)
                {
                    this.SiftDown(0);//упорядочиваем
                }
            }
            return min;
        }
        /// <summary>
        /// Нахождение минимума
        /// </summary>
        /// <returns>Минимум</returns>
        public int FindMin()
        {
            var min = this.root[0];

            return min;
        }
        /// <summary>
        /// Вставка элемента
        /// </summary>
        /// <param name="x">Вставляемый элемент</param>
        public void Insert(int x)
        {
            this.root.Add(x);//добавляем в конец новый элемент

            this.SiftUp(this.root.Count - 1);//сдвигаем его вверх
        }
        /// <summary>
        /// Упорядочивание
        /// </summary>
        /// <param name="i">индекс, с которого стартует упорядочивание</param>
        private void SiftDown(int i)
        {
            var leftChild = (i * 2) + 1;//левый потомок
            var rightChild = (i * 2) + 2;//правый потомок
            var least = i;//текущий назначается меньшим
            //если левый потомок меньше текущего, назначаем меньшим потомка
            if (leftChild < this.root.Count && this.root[leftChild] < this.root[least])
            {
                least = leftChild;
            }
            //если правый потомок меньше текущего, назначем меньшим потомка
            if (rightChild < this.root.Count && this.root[rightChild] < this.root[least])
            {
                least = rightChild;
            }
            //пока текущий не стал меньшим 
            if (least != i)
            { 
                (this.root[i], this.root[least]) = (this.root[least], this.root[i]);
                this.SiftDown(least);
            }
        }
        /// <summary>
        /// Сдвиг вверх
        /// </summary>
        /// <param name="i">Индекс, с которого стартует сдвиг</param>
        private void SiftUp(int i)
        {
            var parent = (i - 1) / 2;//находим родителя
            while (i > 0 && this.root[i] < this.root[parent])//проверяем меньше ли родитель, чем сравниваемый элемент
            {
                (this.root[parent], this.root[i]) = (this.root[i], this.root[parent]);//меняем местами
                i = parent;//сдвигаем наш индекс на наш элемент наверх
                parent = (i - 1) / 2;//выбираем нового родителя
            }
        }
    }

}