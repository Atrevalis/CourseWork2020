using System;
namespace CourseWork2020
{
    public class GameField
    {
        private int[,] originalField;//поле ответов
        private int[,] startField;//поле неизменяемого состояния хранит в себе не изменяемые поля(стартовые и подсказанные)
        private int[,] problemField;//поле основной игры изменяемой игроком 

        public GameField()//составляет изначальную сетку поля
        {
            originalField = new int[9,9];
            problemField = new int[9, 9];
            startField = new int[9, 9];
            for (int i = 0;i<9;i++ )
            {
                for (int j = 0; j < 9; j++)
                {
                    originalField[i,j]=(j+i*3)%9;
                }
            }
            Shaffle();

        }
        public bool Check()//сравнивает поля ориджинал и проблем
        {
            for(int i=0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (originalField[i, j] == problemField[i, j])
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }
        public void Add(int num,int x,int y)//добавляет клетку в problem
        {
            
                problemField[x, y] = num;
                if (Checker(x, y) == true)
                {
                    //вставить обращение к UI(Скорее всего в анализаторе будет это)
                }
           
        }

        public void Delete(int linePos, int colPos)//удаляет клетку из problem
        {
            problemField[linePos, colPos] = 0;
        }

        public bool Checker(int x, int y)//проверяет наличие конфликтующих элементов
        {
            for (int i = 0; i < 9; i++)
            {
                if (problemField[x, y] == problemField[x, i] && i!=y) { return true; }
            }
            for (int i = 0; i < 9; i++)
            {
                if (problemField[x, y] == problemField[i, y] && i != x) { return true; }
            }
            int l, c;
            if (x < 3) { l = 0; } else { if (x < 6) { l = 1; } else { l = 2; } }
            if (y < 3) { c = 0; } else { if (y < 6) { c = 1; } else { c = 2; } }
            for (int i = 0+3*l; i<=2+3*l;i++)
            {
                for (int j = 0 + 3 * c; j <= 2 + 3 * c; j++)
                {
                    if (problemField[x, y] == problemField[i, j] && i != x && j!=y) { return true; }
                }
            }
            return false;
        }
        public void Hint(int x,int y)//подсказчик по указанию 
        {
            problemField[x, y] = originalField[x,y];
            startField[x,y] = originalField[x, y];
            //Здесь обращение к  UI блокирующее поле с координатами x,y(скорее всего будет по другому и блокировать будет в анализаторе )
        }
       /* public void Hint()
        {
            Random random = new Random();
            int x = random.Next(1,10);
            int y = random.Next(1,10);
            problemField[x, y] = originalField[x, y];    
        }*///не эффективно на поздних стадиях игры, будет добавлено если найдем более быстрый алгоритм
        
        private void Shaffle()//перемешивает поле с помощью нижних функций
        {
            Random random = new Random();
            int rand = random.Next(10, 101);//уменьшить при долгом создании матрицы(на данный момент 9<х<101)
            for (int i = 0;i<=rand;i++)
            {
                switch(random.Next(1, 6))
                {
                    case 1:
                        Transpos();
                        break;
                    case 2:
                        SwapLine();
                        break;
                    case 3:
                        SwapBigLine();
                        break;
                    case 4:
                        SwapColumne();
                        break;
                    case 5:
                        SwapBigColumne();
                        break;
                    default:
                        Transpos();//на всякий
                        break;
                }
            }
        }
        private void Transpos()//транспонирует матрицу
        {
            int[,] tempArray = new int[9, 9];
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    tempArray[i, j] = originalField[j, i];
                }
            }
            originalField = tempArray;
        }
        private void SwapLine()//свапает одну линию с другой
        {

            Random random = new Random();
            int area = random.Next(3);
            int non = random.Next(2);
            int[] arr = new int[2];
            int j = 0;
            for (int i = 0; i<3;i++)
            {
                if (i!=non) { arr[j] = 3 * area + i; j++; }
            }
            for (int i = 0;i < 9 ;i++)
            {
                int temp = originalField[arr[0],i];
                originalField[arr[0], i] = originalField[arr[1], i];
                originalField[arr[1], i] = originalField[arr[0], i];
            }


        }
        private void SwapBigLine()//свапает большую линию из 3 с другой 
        {
            int n = 3;
            Random l = new Random();
            var block1 = l.Next(0, n);
            var block2 = l.Next(0, n);
            while (block1 == block2)
                block2 = l.Next(0, n);
            block1 *= n;
            block2 *= n;
            for(int i =0; i < n * n; i++)
            {
                var t = block2;
                for(int j = block1; j < block1 + n; j++)
                {
                    var k = originalField[j, i];
                    originalField[j, i] = originalField[k, i];
                    originalField[j, i] = k;
                    t++;
                }
            }
        }
        private void SwapColumne()//свапает одну колонку с другой
        {
            Transpos();
            SwapLine();
            Transpos();
        }
        private void SwapBigColumne()//свапает большую колонку из 3 с другой 
        {
            Transpos();
            SwapBigLine();
            Transpos();
        }
    }
}
