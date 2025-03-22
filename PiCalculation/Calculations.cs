using System;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
/*
 
Абстрактный класс — это класс, у которого не реализован один или больше методов (некоторые языки требуют такие методы помечать специальными ключевыми словами).

Интерфейс — это абстрактный класс, у которого ни один метод не реализован, все они публичные и нет переменных класса.
Интерфейс нужен обычно когда описывается только интерфейс (тавтология). 
Например, один класс хочет дать другому возможность доступа к некоторым своим методам, но не хочет себя «раскрывать». Поэтому он просто реализует интерфейс.

Абстрактный класс нужен, когда нужно семейство классов, у которых есть много общего. Конечно, можно применить и интерфейс, но тогда нужно будет писать много идентичного кода.
В некоторых языках (С++) специального ключевого слова для обозначения интерфейсов нет.

Можно считать, что любой интерфейс — это уже абстрактный класс, но не наоборот.
 
 */
namespace PiCalculation
{
    //Интерфейс (Абстрактный класс без реализации методов) для вычислений значений числа Пи
    public interface IPiCalculation
    {
        double CalculateNext();

    }
    //Реализация интерфейса для вычисления числа пи методом Лейбница
    public class LeibCalculation : IPiCalculation
    {
        private double currentPi = 0;
        int iteration = 0;
        //Вычисляет следующее значение числа пи методом Лейбница
        public double CalculateNext()
        {
            currentPi += 4 * Math.Pow(-1, iteration) / (2 * iteration + 1);
            iteration++;
            return currentPi;
        }
    }
    //Реализация интерфейса для вычисления числа пи методом Архимеда
    public class ArchCalculation : IPiCalculation
    {
        private double currentPi = 0;
        private int sides = 6;
        private double sideLength = 1;
        //Вычисляет следующее значение числа пи методом Архимеда
        public double CalculateNext()
        {
            double perimeter = sides * sideLength;
            double currentPi = perimeter / 2;
            sides *= 2;
            sideLength = Math.Sqrt(2 - 2 * Math.Sqrt(1 - Math.Pow(sideLength / 2, 2)));
            return currentPi;
        }
    }
    //Реализация интерфейса для вычисления числа пи методом ВВР
    public class BBPCalculation : IPiCalculation
    {
        private double currentPi = 0;
        int iteration = 0;
        //Вычисляет следующее значение числа пи методом ВВР
        public double CalculateNext()
        {
            currentPi += 1 / Math.Pow(16, (double)iteration) * (4 / (8 * (double)iteration + 1) - 2 / (8 * (double)iteration + 4) - 1 / (8 * (double)iteration + 5) - 1 / (8 * (double)iteration + 6));
            iteration++;
            return currentPi;
        }
    }
    //Реализация интерфейса для вычисления числа пи методом Мачина
    public class MachinCalculation : IPiCalculation
    {
        private double currentPi = 0;
        int iteration = 0;

        // Ряд Тейлора для арктангенса
        private static double ArctanTaylor(double x, int iterations)
        {
            double result = 0;
            for (int i = 0; i < iterations; i++)
            {
                int exponent = 2 * i + 1;
                double term = Math.Pow(-1, i) * Math.Pow(x, exponent) / exponent;
                result += term;
            }
            return result;
        }
        //Вычисляет следующее значение числа пи методом Мачина
        public double CalculateNext()
        {
            // Вычисляем арктангенсы с помощью ряда Тейлора
            double arctan1_5 = ArctanTaylor(1.0 / 5, iteration);
            double arctan1_239 = ArctanTaylor(1.0 / 239, iteration);

            // Вычисляем π по формуле Мачина
            currentPi = 16 * arctan1_5 - 4 * arctan1_239;

            iteration++;
            return currentPi;
        }
    }


    //Статический класс для вычисления значения числа Pi с обновлением соответствующих полей
    public static class Calculations
    {
        static public void Calculation(IPiCalculation calculator, int maxIterations, ref bool isRunning, LineSeries series, TextBlock PiBlock, TextBlock ItBlock, ProgressBar PBar, PlotModel PModel, Dispatcher dispatcher, CancellationToken cancellationToken)
        {
            double pi = 0;
            int iterations = 0;



            while (isRunning && iterations < maxIterations)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return; // Прерываем вычисления
                }
                pi = calculator.CalculateNext();
                iterations++;


                // Добавление точки на график
                series.Points.Add(new DataPoint(iterations, pi));
                //Invoke -- синхронный вызов, BeginInvoke -- асинхронный. Из-за того, что я использовал первое, при выключении программы потоки попадали в состояние взаимной блокировки и не давали
                //завершить работу, так как поток ждал ответа от главного потка (отвечающего за UI), а главнфй поток ждал ответа от завершаемого. Второй следует использовать когда не нужно получать результат
                //выполнения кода в UI потоке
                dispatcher.BeginInvoke(() =>
                {
                    PiBlock.Text = $"π ≈ {pi:G1000}";
                    ItBlock.Text = $"Итерация: {iterations}";
                    PBar.Value = iterations;
                    PModel.Series.Clear();
                    PModel.Series.Add(series);
                    PModel.InvalidatePlot(true);
                });

                Thread.Sleep(10);
            }
            isRunning = false;
        }

    }



}