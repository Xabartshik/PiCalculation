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
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Series;
//Необходимо передать число итераций и делегат????
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
        private volatile bool isLeibnizRunning = false;
        private volatile bool isArchimedesRunning = false;
        private volatile bool isBBPRunning = false;
        private volatile bool isMachinRunning = false;

        // Потоки для вычислений
        private Thread leibnizThread;
        private Thread archimedesThread;
        private Thread bbpThread;
        private Thread machinThread;

        //Различные токены для отмены работы потоков
        private CancellationTokenSource LeibnizCancellationTokenSource;
        private CancellationTokenSource ArchCancellationTokenSource;
        private CancellationTokenSource BBPCancellationTokenSource;
        private CancellationTokenSource MachinCancellationTokenSource;


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
            LeibnizCancellationTokenSource = new CancellationTokenSource();
            if (leibnizThread != null && leibnizThread.IsAlive)
            {
                LeibnizCancellationTokenSource.Cancel();
            }
            int maxIterations = int.Parse(LeibnizIterationsInput.Text);
            var series = new LineSeries();

            isLeibnizRunning = true;

            LeibnizProgressBar.Maximum = maxIterations;
            leibnizThread = new Thread(()=>Calculations.Calculation(new LeibCalculation(), maxIterations, ref isLeibnizRunning, series, LeibnizPiValue, LeibnizIterationValue, LeibnizProgressBar, LeibnizPlotModel, this.Dispatcher, LeibnizCancellationTokenSource.Token));
            leibnizThread.Start();
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
            int maxIterations = int.Parse(ArchimedesIterationsInput.Text);
            var series = new LineSeries();

            isArchimedesRunning = true;

            ArchimedesProgressBar.Maximum = maxIterations;
            archimedesThread = new Thread(() => Calculations.Calculation(new ArchCalculation(), maxIterations, ref isArchimedesRunning, series, ArchimedesPiValue, ArchimedesIterationValue, ArchimedesProgressBar, ArchimedesPlotModel, this.Dispatcher, ArchCancellationTokenSource.Token));
            archimedesThread.Start();
        }
        //Остановка вычисления для Архимеда
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
            int maxIterations = int.Parse(ArchimedesIterationsInput.Text);
            var series = new LineSeries();

            isBBPRunning = true;

            BBPProgressBar.Maximum = maxIterations;
            bbpThread = new Thread(() => Calculations.Calculation(new BBPCalculation(), maxIterations, ref isBBPRunning, series, BBPPiValue, BBPIterationValue, BBPProgressBar, BBPPlotModel, this.Dispatcher, BBPCancellationTokenSource.Token));
            bbpThread.Start();
        }


        //Остановка вычисления для ВВр
        private void BBPStopButton_Click(object sender, RoutedEventArgs e)
        {
            isBBPRunning = false;
        }



        //Остановка вычисления для Мачина
        private void MachinStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (machinThread != null && machinThread.IsAlive)
            {
                machinThread.Abort();
            }

            int maxIterations = int.Parse(ArchimedesIterationsInput.Text);
            var series = new LineSeries();

            isMachinRunning = true;

            MachinProgressBar.Maximum = maxIterations;
            machinThread = new Thread(() => Calculations.Calculation(new MachinCalculation(), maxIterations, ref isMachinRunning, series, MachinPiValue, MachinIterationValue, MachinProgressBar, MachinPlotModel, this.Dispatcher, MachinCancellationTokenSource.Token));
            machinThread.Start();
        }
        //Метод для остановки потока
        private void StopThread(Thread thread, bool isRunning, CancellationTokenSource cancellationTokenSource)
        {
            if (thread != null && thread.IsAlive)
            {
                cancellationTokenSource.Cancel();
                isRunning = false; // Устанавливаем флаг остановки
                thread.Join(); // Ожидаем завершения потока
            }
        }
        //Завершение всех потоков при закрытии окна
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopThread(archimedesThread, isArchimedesRunning, ArchCancellationTokenSource);
            StopThread(leibnizThread, isLeibnizRunning, LeibnizCancellationTokenSource);
            StopThread(bbpThread, isBBPRunning, BBPCancellationTokenSource);
            StopThread(machinThread, isMachinRunning, MachinCancellationTokenSource);
        }
        //Остановка вычисления для Мачина
        private void MachinStopButton_Click(object sender, RoutedEventArgs e)
        {
            isMachinRunning = false;
        }

    }
}
