using System;
namespace CourseWork2020
{
    public class GameField
    {
        int[,] originalField;//поле ответов
        int[,] startField;//поле изначального состояния НЕ МЕНЯТЬ
        int[,] problemField;//поле основной игры изменяемой игроком 

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
            shaffle();
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
        public void add(int num,int x,int y)//добавляет клетку в problem
        {
            if (startField[x,y]==0)
            {
                problemField[x, y] = num;
            }
            else
            {
                //здесь возможно будет вызов всплывающего окошка
            }
        }
        public void delete(int linePos, int colPos)//удаляет клетку из problem
        {
            problemField[linePos, colPos] = 0;
        }
        private void shaffle()//перемешивает поле с помощью нижних функций
        {
            Random random = new Random();
            int rand = random.Next(10, 101);//уменьшить при долгом создании матрицы(на данный момент 9<х<101)
            for (int i = 0;i<=rand;i++)
            {
                switch(random.Next(1, 6))
                {
                    case 1:
                        this.transpos();
                        break;
                    case 2:
                        this.swapLine();
                        break;
                    case 3:
                        this.swapBigLine();
                        break;
                    case 4:
                        this.swapColumne();
                        break;
                    case 5:
                        this.swapBigColumne();
                        break;
                    default:
                        this.transpos();//на всякий
                        break;
                }
            }
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
            Random random = new Random();
            int first = random.Next(9);
            int second = random.Next(2);
            int[] swLine = new int[9];
            for (int i = 0;i<9 ;i++) {

            }

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
