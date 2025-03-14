//using System;
//using System.Threading.Tasks;
//using System.Windows;
//using OxyPlot;
//using OxyPlot.Series;

//namespace PiCalculation
//{
//    public partial class MainWindow : Window
//    {
//        // Модели для графиков
//        public PlotModel LeibnizPlotModel { get; set; }
//        public PlotModel ArchimedesPlotModel { get; set; }
//        public PlotModel BBPPlotModel { get; set; }
//        public PlotModel MachinPlotModel { get; set; }

//        // Флаги для остановки вычислений
//        private bool isLeibnizRunning = false;
//        private bool isArchimedesRunning = false;
//        private bool isBBPRunning = false;
//        private bool isMachinRunning = false;

//        public MainWindow()
//        {
//            InitializeComponent();

//            // Инициализация графиков
//            LeibnizPlotModel = new PlotModel { Title = "Метод Лейбница" };
//            ArchimedesPlotModel = new PlotModel { Title = "Метод Архимеда" };
//            BBPPlotModel = new PlotModel { Title = "Метод BBP" };
//            MachinPlotModel = new PlotModel { Title = "Метод Мачина" };

//            DataContext = this;
//        }

//        // Метод Лейбница
//        private async void LeibnizStartButton_Click(object sender, RoutedEventArgs e)
//        {
//            isLeibnizRunning = true;
//            double pi = 0;
//            int iterations = 0;
//            int maxIterations = int.Parse(LeibnizIterationsInput.Text);
//            var series = new LineSeries();
//            LeibnizProgressBar.Maximum = maxIterations;

//            while (isLeibnizRunning && iterations < maxIterations)
//            {
//                pi += 4 * Math.Pow(-1, iterations) / (2 * iterations + 1);
//                iterations++;

//                // Обновление значения π
//                LeibnizPiValue.Text = $"π ≈ {pi:G1000}";
//                LeibnizIterationValue.Text = $"Итерация: {iterations}";
//                LeibnizProgressBar.Value = iterations;

//                // Добавление точки на график
//                series.Points.Add(new DataPoint(iterations, pi));
//                LeibnizPlotModel.Series.Clear();
//                LeibnizPlotModel.Series.Add(series);
//                LeibnizPlotModel.InvalidatePlot(true);

//                await Task.Delay(10); // Задержка для визуализации
//            }
//            isLeibnizRunning = false;
//        }

//        private void LeibnizStopButton_Click(object sender, RoutedEventArgs e)
//        {
//            isLeibnizRunning = false;
//        }

//        // Метод Архимеда
//        private async void ArchimedesStartButton_Click(object sender, RoutedEventArgs e)
//        {
//            isArchimedesRunning = true;
//            double pi = 0;
//            int sides = 6; // Начинаем с шестиугольника
//            double sideLength = 1; // Длина стороны шестиугольника
//            int maxIterations = int.Parse(ArchimedesIterationsInput.Text);
//            var series = new LineSeries();
//            ArchimedesProgressBar.Maximum = maxIterations;

//            while (isArchimedesRunning && Math.Log2(sides / 6) < maxIterations + 1)
//            {
//                // Вычисляем периметр многоугольника
//                double perimeter = sides * sideLength;

//                // Приближение для π
//                pi = perimeter / 2; // Поскольку радиус = 1

//                // Обновление значения π
//                ArchimedesPiValue.Text = $"π ≈ {pi:G1000}";
//                ArchimedesIterationValue.Text = $"Итерация: {Math.Log2(sides / 6)}";
//                ArchimedesProgressBar.Value = Math.Log2(sides / 6);
//                // Добавление точки на график
//                series.Points.Add(new DataPoint(sides, pi));
//                ArchimedesPlotModel.Series.Clear();
//                ArchimedesPlotModel.Series.Add(series);
//                ArchimedesPlotModel.InvalidatePlot(true);

//                // Удваиваем число сторон и вычисляем новую длину стороны
//                sides *= 2;
//                sideLength = Math.Sqrt(2 - 2 * Math.Sqrt(1 - Math.Pow(sideLength / 2, 2)));

//                await Task.Delay(10); // Задержка для визуализации
//            }
//            isArchimedesRunning = false;
//        }

//        private void ArchimedesStopButton_Click(object sender, RoutedEventArgs e)
//        {
//            isArchimedesRunning = false;
//        }

//        // Метод BBP
//        private async void BBPStartButton_Click(object sender, RoutedEventArgs e)
//        {
//            isBBPRunning = true;
//            double pi = 0;
//            double k = 0;
//            int maxIterations = int.Parse(BBPIterationsInput.Text);
//            var series = new LineSeries();
//            BBPProgressBar.Maximum = maxIterations;

//            while (isBBPRunning && k < maxIterations)
//            {
//                pi += 1 / Math.Pow(16, k) * (4 / (8 * k + 1) - 2 / (8 * k + 4) - 1 / (8 * k + 5) - 1 / (8 * k + 6));
//                k++;

//                // Обновление значения π
//                BBPPiValue.Text = $"π ≈ {pi:G1000}";
//                BBPIterationValue.Text = $"Итерация: {k}";
//                BBPProgressBar.Value = k;
//                // Добавление точки на график
//                series.Points.Add(new DataPoint(k, pi));
//                BBPPlotModel.Series.Clear();
//                BBPPlotModel.Series.Add(series);
//                BBPPlotModel.InvalidatePlot(true);


//                await Task.Delay(10); // Задержка для визуализации
//            }

//            isBBPRunning = false;
//        }


//        private void BBPStopButton_Click(object sender, RoutedEventArgs e)
//        {
//            isBBPRunning = false;
//        }

//        // Ряд Тейлора для арктангенса
//        private double ArctanTaylor(double x, int iterations)
//        {
//            double result = 0;
//            for (int i = 0; i < iterations; i++)
//            {
//                int exponent = 2 * i + 1;
//                double term = Math.Pow(-1, i) * Math.Pow(x, exponent) / exponent;
//                result += term;
//            }
//            return result;
//        }

//        private async void MachinStartButton_Click(object sender, RoutedEventArgs e)
//        {
//            isMachinRunning = true;
//            double pi = 0;
//            int iterations = 0;
//            int maxIterations = int.Parse(MachinIterationsInput.Text);
//            var series = new LineSeries();
//            MachinProgressBar.Maximum = maxIterations;
//            while (isMachinRunning && iterations < maxIterations + 1)
//            {
//                // Вычисляем арктангенсы с помощью ряда Тейлора
//                double arctan1_5 = ArctanTaylor(1.0 / 5, iterations);
//                double arctan1_239 = ArctanTaylor(1.0 / 239, iterations);

//                // Вычисляем π по формуле Мачина
//                pi = 16 * arctan1_5 - 4 * arctan1_239;

//                // Обновление значения π
//                MachinPiValue.Text = $"π ≈ {pi:G1000}";
//                MachinIterationValue.Text = $"Итерация: {iterations}";
//                MachinProgressBar.Value = iterations;

//                // Добавление точки на график
//                series.Points.Add(new DataPoint(iterations, pi));
//                MachinPlotModel.Series.Clear();
//                MachinPlotModel.Series.Add(series);
//                MachinPlotModel.InvalidatePlot(true);

//                iterations++;
//                await Task.Delay(10); // Задержка для визуализации
//            }
//            isMachinRunning = false;
//        }

//        private void MachinStopButton_Click(object sender, RoutedEventArgs e)
//        {
//            isMachinRunning = false;
//        }

//    }
//}


using System;
using System.Threading;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;

namespace PiCalculation
{
    public partial class MainWindow : Window
    {
        // Модели для графиков
        public PlotModel LeibnizPlotModel { get; set; }
        public PlotModel ArchimedesPlotModel { get; set; }
        public PlotModel BBPPlotModel { get; set; }
        public PlotModel MachinPlotModel { get; set; }

        // Флаги для остановки вычислений
        private bool isLeibnizRunning = false;
        private bool isArchimedesRunning = false;
        private bool isBBPRunning = false;
        private bool isMachinRunning = false;

        // Потоки для вычислений
        private Thread leibnizThread;
        private Thread archimedesThread;
        private Thread bbpThread;
        private Thread machinThread;

        // Семафоры для синхронизации потоков
        private Semaphore leibnizSemaphore = new Semaphore(0, 1);
        private Semaphore archimedesSemaphore = new Semaphore(0, 1);
        private Semaphore bbpSemaphore = new Semaphore(0, 1);
        private Semaphore machinSemaphore = new Semaphore(0, 1);

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация графиков
            LeibnizPlotModel = new PlotModel { Title = "Метод Лейбница" };
            ArchimedesPlotModel = new PlotModel { Title = "Метод Архимеда" };
            BBPPlotModel = new PlotModel { Title = "Метод BBP" };
            MachinPlotModel = new PlotModel { Title = "Метод Мачина" };

            DataContext = this;
            this.Closing += MainWindow_Closing; // Подписываемся на событие Closing
        }


        // Метод Лейбница
        private void LeibnizStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (leibnizThread != null && leibnizThread.IsAlive)
            {
                leibnizThread.Abort();
            }

            isLeibnizRunning = true;
            leibnizThread = new Thread(LeibnizCalculation);
            leibnizThread.Start();
        }

        private void LeibnizCalculation()
        {
            double pi = 0;
            int iterations = 0;
            int maxIterations = 0;

            // Используем Dispatcher для получения значения из UI-элемента
            LeibnizIterationsInput.Dispatcher.Invoke(() =>
            {
                maxIterations = int.Parse(LeibnizIterationsInput.Text);
            });

            var series = new LineSeries();
            LeibnizProgressBar.Dispatcher.Invoke(() => LeibnizProgressBar.Maximum = maxIterations);

            while (isLeibnizRunning && iterations < maxIterations)
            {
                pi += 4 * Math.Pow(-1, iterations) / (2 * iterations + 1);
                iterations++;

                // Обновление значения π
                LeibnizPiValue.Dispatcher.Invoke(() => LeibnizPiValue.Text = $"π ≈ {pi:G1000}");
                LeibnizIterationValue.Dispatcher.Invoke(() => LeibnizIterationValue.Text = $"Итерация: {iterations}");
                LeibnizProgressBar.Dispatcher.Invoke(() => LeibnizProgressBar.Value = iterations);

                // Добавление точки на график
                series.Points.Add(new DataPoint(iterations, pi));
                Dispatcher.Invoke(() =>
                {
                    LeibnizPlotModel.Series.Clear();
                    LeibnizPlotModel.Series.Add(series);
                    LeibnizPlotModel.InvalidatePlot(true);
                });

                Thread.Sleep(10);
            }
        }



        private void LeibnizStopButton_Click(object sender, RoutedEventArgs e)
        {
                isLeibnizRunning = false;
        }


        // Метод Архимеда
        private void ArchimedesStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (archimedesThread != null && archimedesThread.IsAlive)
            {
                archimedesThread.Abort();
            }

            isArchimedesRunning = true;
            archimedesThread = new Thread(ArchimedesCalculation);
            archimedesThread.Start();
        }

        private void ArchimedesCalculation()
        {
            double pi = 0;
            int sides = 6; // Начинаем с шестиугольника
            double sideLength = 1; // Длина стороны шестиугольника
            int maxIterations = 0;
            ArchimedesIterationsInput.Dispatcher.Invoke(() =>
            {
                maxIterations = int.Parse(ArchimedesIterationsInput.Text);
            });
            var series = new LineSeries();
            ArchimedesProgressBar.Dispatcher.Invoke(() =>
            {
                ArchimedesProgressBar.Maximum = maxIterations;
            });


            while (isArchimedesRunning && (Math.Log2(sides / 6) < maxIterations + 1))
            {
                // Вычисляем периметр многоугольника
                double perimeter = sides * sideLength;

                // Приближение для π
                pi = perimeter / 2; // Поскольку радиус = 1

                // Обновление значения π
                Dispatcher.Invoke(() =>
                {
                    ArchimedesPiValue.Text = $"π ≈ {pi:G1000}";
                    ArchimedesIterationValue.Text = $"Итерация: {Math.Log2(sides / 6)}, {sides} сторон";
                    ArchimedesProgressBar.Value = Math.Log2(sides / 6);
                });

                // Добавление точки на график
                series.Points.Add(new DataPoint(sides, pi));
                Dispatcher.Invoke(() =>
                {
                    ArchimedesPlotModel.Series.Clear();
                    ArchimedesPlotModel.Series.Add(series);
                    ArchimedesPlotModel.InvalidatePlot(true);
                });

                // Удваиваем число сторон и вычисляем новую длину стороны
                sides *= 2;
                sideLength = Math.Sqrt(2 - 2 * Math.Sqrt(1 - Math.Pow(sideLength / 2, 2)));

                archimedesSemaphore.WaitOne(1000);
            }
        }

        private void ArchimedesStopButton_Click(object sender, RoutedEventArgs e)
        {
            isArchimedesRunning = false;
        }


        // Метод BBP
        private void BBPStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (bbpThread != null && bbpThread.IsAlive)
            {
                bbpThread.Abort();
            }

            isBBPRunning = true;
            bbpThread = new Thread(BBPCalculation);
            bbpThread.Start();
        }

        private void BBPCalculation()
        {
            double pi = 0;
            double k = 0;
            int maxIterations = 0;
            BBPIterationsInput.Dispatcher.Invoke(() =>
            {
                maxIterations = int.Parse(BBPIterationsInput.Text);
            });

            var series = new LineSeries();
            BBPProgressBar.Dispatcher.Invoke(() =>
            {
                BBPProgressBar.Maximum = maxIterations;
            });
            while (isBBPRunning && k < maxIterations)
            {
                pi += 1 / Math.Pow(16, k) * (4 / (8 * k + 1) - 2 / (8 * k + 4) - 1 / (8 * k + 5) - 1 / (8 * k + 6));
                k++;

                // Обновление значения π
                Dispatcher.Invoke(() =>
                {
                    BBPPiValue.Text = $"π ≈ {pi:G1000}";
                    BBPIterationValue.Text = $"Итерация: {k}";
                    BBPProgressBar.Value = k;
                });

                // Добавление точки на график
                series.Points.Add(new DataPoint(k, pi));
                Dispatcher.Invoke(() =>
                {
                    BBPPlotModel.Series.Clear();
                    BBPPlotModel.Series.Add(series);
                    BBPPlotModel.InvalidatePlot(true);
                });

                bbpSemaphore.WaitOne(1000);
            }
        }

        private void BBPStopButton_Click(object sender, RoutedEventArgs e)
        {
            isBBPRunning = false;
        }

        // Ряд Тейлора для арктангенса
        private double ArctanTaylor(double x, int iterations)
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

        // Метод Мачина
        private void MachinStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (machinThread != null && machinThread.IsAlive)
            {
                machinThread.Abort();
            }

            isMachinRunning = true;
            machinThread = new Thread(MachinCalculation);
            machinThread.Start();
        }

        private void MachinCalculation()
        {
            double pi = 0;
            int iterations = 0;
            int maxIterations = 0;
            MachinIterationsInput.Dispatcher.Invoke(() =>
            {
                maxIterations = int.Parse(MachinIterationsInput.Text);
            });
            var series = new LineSeries();

            MachinProgressBar.Dispatcher.Invoke(() =>
            {
                MachinProgressBar.Maximum = maxIterations;
            });

            while (isMachinRunning && iterations < maxIterations + 1)
            {
                // Вычисляем арктангенсы с помощью ряда Тейлора
                double arctan1_5 = ArctanTaylor(1.0 / 5, iterations);
                double arctan1_239 = ArctanTaylor(1.0 / 239, iterations);

                // Вычисляем π по формуле Мачина
                pi = 16 * arctan1_5 - 4 * arctan1_239;

                // Обновление значения π
                Dispatcher.Invoke(() =>
                {
                    MachinPiValue.Text = $"π ≈ {pi:G1000}";
                    MachinIterationValue.Text = $"Итерация: {iterations}";
                    MachinProgressBar.Value = iterations;
                });

                // Добавление точки на график
                series.Points.Add(new DataPoint(iterations, pi));
                Dispatcher.Invoke(() =>
                {
                    MachinPlotModel.Series.Clear();
                    MachinPlotModel.Series.Add(series);
                    MachinPlotModel.InvalidatePlot(true);
                });

                iterations++;
                machinSemaphore.WaitOne(1000);
            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Останавливаем поток Архимеда
            if (archimedesThread != null && archimedesThread.IsAlive)
            {
                isArchimedesRunning = false; // Устанавливаем флаг остановки
                archimedesSemaphore.Release(); // Разблокируем поток, если он ожидает
                archimedesThread.Join(); // Ожидаем завершения потока
            }
            if (leibnizThread != null && leibnizThread.IsAlive)
            {
                isLeibnizRunning = false; // Устанавливаем флаг остановки
                leibnizSemaphore.Release(); // Разблокируем поток, если он ожидает
                leibnizThread.Join(); // Ожидаем завершения потока
            }
            if (bbpThread != null && bbpThread.IsAlive)
            {
                isArchimedesRunning = false; // Устанавливаем флаг остановки
                bbpSemaphore.Release(); // Разблокируем поток, если он ожидает
                bbpThread.Join(); // Ожидаем завершения потока
            }
            if (machinThread != null && machinThread.IsAlive)
            {
                isMachinRunning = false; // Устанавливаем флаг остановки
                machinSemaphore.Release(); // Разблокируем поток, если он ожидает
                machinThread.Join(); // Ожидаем завершения потока
            }

            // Добавьте аналогичные действия для других потоков, если они есть
        }
        private void MachinStopButton_Click(object sender, RoutedEventArgs e)
        {
            isMachinRunning = false;
        }

    }
}
