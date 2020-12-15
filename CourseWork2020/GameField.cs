using System;
namespace CourseWork2020
{
    public class GameField
    {
        int[,] originalField;//поле ответов
        int[,] startField;//поле изначального состояния НЕ МЕНЯТЬ
        int[,] problemField;//поле основной игры изсменяемой игроком 

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
        }
        public bool check()//сравнивает поля ориджинал и проблем
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
        public void add()//добавляет клетку в problem
        {

        }
        public void delete(int linePos, int colPos)//удаляет клетку из problem
        {
            problemField[linePos, colPos] = 0;
        }
        private void shaffle()//перемешивает поле с помощью нижних функций
        {

        }
        private void transpos()//транспонирует матрицу
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
        private void swapLine()//свапает одну линию с другой
        {

        }
        private void swapBigLine()//свапает большую линию из 3 с другой 
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
        private void swapColumne()//свапает одну колонку с другой
        {

        }
        private void swapBigColumne()//свапает большую колонку из 3 с другой 
        {
            transpos();
            swapBigLine();
            transpos();
        }
    }
}
