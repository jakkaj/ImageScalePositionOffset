using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Drawing.Image image;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.SizeChanged += MainWindow_SizeChanged;

            image =
                System.Drawing.Image.FromFile(
                    @"C:\Users\jak\Documents\visual studio 2017\Projects\WpfApp2\WpfApp2\Assets\IMG_1404.JPG");
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _doImage();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _doImage();
        }

        void _doImage()
        {
            

            var imageWidth = Convert.ToDouble(image.Width);
            var imageHeight = Convert.ToDouble(image.Height);

            double actualHeight = 0;
            double actualWidth = 0;

            var uiWidth = MainGrid.ActualWidth;
            var uiHeight = MainGrid.ActualHeight;
            
            var windowAspect = uiWidth / uiHeight;

            var imageAspect = imageWidth / imageHeight;
            
            (double, double, double, double) result;

            if (imageAspect > windowAspect)
            {
                result = _hw(imageHeight, imageWidth, uiHeight, uiWidth);
            }
            else
            {
                result = _wh(imageHeight, imageWidth, uiHeight, uiWidth);
            }

            //if (imageAspect > 1 && windowAspect > 1)
            //{
            //    Debug.WriteLine("A");
            //    result = _hw(imageHeight, imageWidth, uiHeight, uiWidth);

            //}
            //else if(imageAspect > 1 && windowAspect < 1)
            //{
            //    Debug.WriteLine("B");
            //    result = _hw(imageHeight, imageWidth, uiHeight, uiWidth);

            //}else if (imageAspect < 1 && windowAspect > 1)
            //{
            //    Debug.WriteLine("C");
            //    result = _wh(imageHeight, imageWidth, uiHeight, uiWidth);
            //}
            //else
            //{
            //    Debug.WriteLine("D");
            //    result = _hw(imageHeight, imageWidth, uiHeight, uiWidth);
            //}

            var left = result.Item1;
            var top = result.Item2;
            var  scalex = result.Item3;
            var scaley = result.Item4;

            Debug.WriteLine($"Scale: {scalex}, {scaley}");

            double offsetLeft = left + (1300 * scalex);
            double offsetTop = top + (1700 * scaley);

            Canvas.SetLeft(MyBorder, offsetLeft);
            Canvas.SetTop(MyBorder, offsetTop);
        }

        (double, double, double, double) _wh(double imageHeight, double imageWidth, double uiHeight, double uiWidth)
        {
            var aspect = imageHeight / imageWidth;
      
            var actualHeight = uiHeight;
            var actualWidth = actualHeight / aspect;

            var center = uiWidth / 2;

            var left = center - (actualWidth / 2);
            var top = 0;

            var scaley = actualHeight / imageHeight;
            var scalex = actualWidth / imageWidth;

            return (left, top, scalex, scaley);
        }

        (double, double, double, double) _hw(double imageHeight, double imageWidth, double uiHeight, double uiWidth)
        {
            var aspect = imageWidth / imageHeight;
            var actualWidth = uiWidth;
            var actualHeight = actualWidth / aspect;

            var center = uiHeight / 2;
            var left = 0;
            var top = center - (actualHeight / 2);

            var scaley = actualHeight / imageHeight;
            var scalex = actualWidth / imageWidth;

            return (left, top, scalex, scaley);
        }

        private void DoClick(object sender, RoutedEventArgs e)
        {
            _doImage();
        }
    }
}
