using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FaceppSDK;
using System.Windows.Forms;
using System.IO;

namespace FaceppDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public FaceService fs = new FaceService("2affcadaeddd18f422375adc869f3991", "EsU9hmgweuz8U-nwv6s4JP-9AJt64vhz");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "jpg文件|*.jpg|png文件|*.png|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "jpg";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            String filepath = openFileDialog.FileName;
            
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filepath);
            bitmap.DecodePixelHeight = (int)image1.Height;
            bitmap.DecodePixelWidth = (int)image1.Width;
            bitmap.EndInit();
            image1.Source = bitmap;
            PngBitmapEncoder pngE = new PngBitmapEncoder();
            pngE.Frames.Add(BitmapFrame.Create(bitmap));
            using (Stream stream = File.Create(System.Environment.CurrentDirectory + "temp.jpg"))
            {
                pngE.Save(stream);
            }
            DetectResult res = fs.Detection_DetectImg(System.Environment.CurrentDirectory + "temp.jpg");
            RectangleGeometry rect = new RectangleGeometry();
            rect.Rect = new Rect(res.face[0].center.x * image1.Width / 100.0 - res.face[0].width * image1.Width / 200.0,
                                 res.face[0].center.y * image1.Height / 100.0 - res.face[0].height * image1.Height / 200.0,
                                 res.face[0].width * image1.Width / 100.0, res.face[0].height * image1.Height / 100.0);
            System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();
            myPath.Stroke = Brushes.Red;
            myPath.StrokeThickness = 3;
            myPath.Data = rect;
            label1.Content = label1.Content + String.Format("({0:F2},{1:F2})", res.face[0].center.x, res.face[0].center.y);
            label2.Content = label2.Content + String.Format("({0:F2},{1:F2})", res.face[0].width, res.face[0].height);
            canvas1.Children.Clear();
            canvas1.Children.Add(myPath);
        }
    }
}
