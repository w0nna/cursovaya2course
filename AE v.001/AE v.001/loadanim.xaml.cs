using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AE_v._001
{
    /// <summary>
    /// Логика взаимодействия для loadanim.xaml
    /// </summary>
    public partial class loadanim : Window
    {
        public loadanim()
        {
            InitializeComponent();
        }
        private void AnimationCompleted(object sender, EventArgs e)
        {
            ChangeWindow();
        }
        private void ChangeWindow()
        {
            Thread.Sleep(2000);
            MainWindow mmav = new MainWindow();
            Window lw = Application.Current.MainWindow;
            lw.Close();
            mmav.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartAnimation();
        }

        private void StartAnimation()
        {
            // Создание анимации
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = TimeSpan.FromSeconds(2);

            // Запуск анимации на вашем элементе
            logoImage.BeginAnimation(OpacityProperty, animation);
        }


    }
}
