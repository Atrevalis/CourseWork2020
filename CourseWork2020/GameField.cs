using System;
namespace CourseWork2020
{
    public class GameField
    {
        private int[,] originalField;//поле ответов
        private int[,] startField;//поле неизменяемого состояния НЕ МЕНЯТЬ
        private int[,] problemField;//поле основной игры изменяемой игроком 

        public GameField()//составляет изначальную сетку поля
        {
            originalField = new int[9,9];
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
                    //вставить обращение к UI
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
                if (problemField[x, y] == problemField[x, i]) { return true; }
            }
            for (int i = 0; i < 9; i++)
            {
                if (problemField[x, y] == problemField[i, y]) { return true; }
            }
            return false;
        }
        public void Hint(int x,int y)//подсказчик по указанию 
        {
            problemField[x, y] = originalField[x,y];
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
                        this.Transpos();
                        break;
                    case 2:
                        this.SwapLine();
                        break;
                    case 3:
                        this.SwapBigLine();
                        break;
                    case 4:
                        this.SwapColumne();
                        break;
                    case 5:
                        this.SwapBigColumne();
                        break;
                    default:
                        this.Transpos();//на всякий
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
            int first = random.Next(9);
            int second = random.Next(2);
            int[] swLine = new int[9];
            for (int i = 0;i<9 ;i++) {

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
            int n = 3;
            Random r = new Random();
            var block = r.Next(0, n);
            var col1 = r.Next(0, n);
            var tow1 = block * n + col1;
            var col2 = r.Next(0, n);
            while (col1 == col2)
                col2 = r.Next(0, n);
            var tow2 = block * n + col2;
            for(int i = 0; i < n * n; i++)
            {
                var temp = originalField[i, tow1];
                originalField[i, tow1] = originalField[i, tow2];
                originalField[i, tow2] = temp;
            }
        }
        private void SwapBigColumne()//свапает большую колонку из 3 с другой 
        {
            Transpos();
            SwapBigLine();
            Transpos();
        }
        private void SudokuSolve(int[,] field)//вывод решения судоку
        {
            if (field == null || field.Length == 0)
                return;
            Solver(field);
        }
        private bool Solver(int[,] field)//решатель судоку
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if(field[i,j] == 0)
                    {
                        for(int c = 1; c <= 9; c++)
                        {
                            if (isValid(field, i, j, c))
                            {
                                field[i, j] = c;
                                if (Solver(field))
                                    return true;
                                else
                                    field[i, j] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        private bool isValid(int[,] field, int line, int col, int c)//проверяет допустимо ли добавление числа в клетку
        {
            for(int i = 0; i < 9; i++)
            {
                if (field[i, col] != 0 && field[i, col] == c)
                    return false;
                if (field[line, i] != 0 && field[line, i] == c)
                    return false;
                if (field[3 * (line / 3) + i / 3, 3 * (col / 3) + i % 3] != 0 && field[3 * (line / 3) + i / 3, 3 * (col / 3) + i % 3] == c)
                    return false;
            }
            return true;
        }
    }
}
