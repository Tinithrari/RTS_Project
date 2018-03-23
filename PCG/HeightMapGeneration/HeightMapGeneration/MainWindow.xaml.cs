using Microsoft.Win32;
using PerlinNoises;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeightMapGeneration
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HeightMapFactory factory;
        private Bitmap result;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            int width = int.Parse(WidthBox.Text);
            int height = int.Parse(HeightBox.Text);

            int octaves = int.Parse(OctavesBox.Text);
            double persistancy = double.Parse(PersitanceBox.Text);
            double step = double.Parse(StepBox.Text);

            this.factory = new HeightMapFactory(width, height);
            this.result = this.factory.CreateHeightMap(octaves, step, persistancy);

            ResultImage.Source = Imaging.CreateBitmapSourceFromHBitmap(result.GetHbitmap(), 
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            SaveButton.IsEnabled = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "PNG Image (*.png)|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Interlace = PngInterlaceOption.On;
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource) ResultImage.Source));
                encoder.Save(stream);
                stream.Close();
            }
        }
    }
}
