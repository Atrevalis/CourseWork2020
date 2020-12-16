using System;
namespace CourseWork2020
{
    public class GameField
    {
        
        private int[,] originalField;//поле ответов
        private int[,] startField;//поле неизменяемого состояния хранит в себе не изменяемые поля(стартовые и подсказанные)
        private int[,] problemField;//поле основной игры изменяемой игроком 

        public GameField(int diff)//составляет изначальную сетку поля для обычного режима//ДОПИСАТЬ
        {
            originalField = new int[9,9];
            problemField = new int[9, 9];
            startField = new int[9, 9];
            for (int i = 0;i<9;i++ )
            {
                for (int j = 0; j < 9; j++)
                {
                    originalField[i,j]=(j+i/3+i*3)%9+1;
                    problemField[i,j] = originalField[i, j];
                }
            }
            Shaffle();
            CreateProblem(diff);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    startField[i, j] = problemField[i, j];
                }
            }

        }
        public GameField(int[,] field,int diff)//ДОПИСАТЬ
        {
                originalField = new int[9, 9];
                problemField = new int[9, 9];
                startField = new int[9, 9];
                bool create = SudokuSolve(field);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        originalField[i, j] = field[i, j];
                    }
                }
            CreateProblem(diff);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    startField[i, j] = problemField[i, j];
                }
            }
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
        public void CreateProblem(int diff)//создание игрового поля в соответствии со сложностью
        {
            int temp;
            int[,] look = new int[9, 9];//массив проверки посещения клетки
            int count = 0;//счетчик просмотренных клеток
            Random r = new Random();
            while (count < diff)
            {
                int i = r.Next(0, 9);//нахождение рандомной клетки
                int j = r.Next(0, 9);
                if (look[i, j] == 0)
                {
                    count += 1;
                    look[i, j] = 1;//пометка клетки
                    temp = problemField[i, j];//сохранение изначальной клетки
                    problemField[i, j] = 0;//удаление клетки
                    diff--;
                    int[,] tempArray = new int[9, 9];//временный массив
                    for (int g = 0; g < 9; g++)//копируем в временный массив problemField
                    {
                        for (int h = 0; h < 9; h++)
                        {
                            tempArray[g, h] = problemField[g, h];
                        }
                    }
                    if (SudokuSolve(tempArray))//проверка на решабельность поля
                    {
                        for (int k = 0; k < 9; k++)//сравнение решенного поля с изначальным
                        {
                            for (int f = 0; f < 9; f++)
                            {
                                if (tempArray[k, f] == originalField[k, f])
                                    continue;
                                else
                                {
                                    problemField[i, j] = temp;
                                    diff++;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        problemField[i, j] = temp;
                        diff++;
                        break;
                    }
                }
            }
        }

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
            Random l = new Random();
            var block1 = l.Next(0, 3);
            var block2 = l.Next(0, 3);
            while (block1 == block2)
                block2 = l.Next(0, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int n1 = block1 * 3 + 1;
                    int n2 = block2 * 3 + 1;
                    int temp = originalField[n1, j];
                    originalField[n1, j] = originalField[n2, j];
                    originalField[n2, j] = temp;
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
        public bool SudokuSolve(int[,] field)//вывод решения судоку
        {
           bool exit = true;
            for (int i = 0;i<9 ;i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (field[i,j]!=0) { exit = false; break; }
                }
            }
            if (exit) { return false; }//мб сюда запихнуть алгоритм постройки поля из обычного режима
            return Solver(field);
        }
        public bool Solver(int[,] field)//решатель судоку
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
