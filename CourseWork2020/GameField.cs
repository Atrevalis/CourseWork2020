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
        public void check()//сравнивает поля ориджинал и проблем
        {

        }
        public void add()//добавляет клетку в problem
        {

        }
        public void delete()//удаляет клетку из problem
        {

        }
        private void shaffle()//перемешивает поле с помощью нижних функций
        {

        }
        private void transpos()//транспонирует матрицу
        {

        }
        private void swapLine()//свапает одну линию с другой
        {

        }
        private void swapBigLine()//свапает большую линию из 3 с другой 
        {

        }
        private void swapColumne()//свапает одну колонку с другой
        {

        }
        private void swapBigColumne()//свапает большую колонку из 3 с другой 
        {

        }
    }
}
