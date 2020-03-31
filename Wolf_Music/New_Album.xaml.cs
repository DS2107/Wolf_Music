using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Wolf_Music.Classes;

namespace Wolf_Music
{
    /// <summary>
    /// Логика взаимодействия для New_Album.xaml
    /// </summary>
    public partial class New_Album : Window
    {
        /// <summary>
        /// связь с классом
        /// </summary>
        Albums album = new Albums();
       

      
        public New_Album()
        {
            InitializeComponent();
            Button_Create.IsEnabled = false;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }
          
        } // Window_MouseLeftButtonDown

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Minim_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        string name;
        /// <summary>
        /// Выбор фото
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///   
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {

                string filename = openFileDialog.FileName;
                name = openFileDialog.SafeFileName;
                ImagePlay.Source = new BitmapImage(new Uri(openFileDialog.FileName)); ;

            } // if  
        } // Image_MouseLeftButtonDown
       
        /// <summary>
        ///  Создание Плейлиста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {        
            album.Create(ImagePlay.Source.ToString(), TitleName.Text,name);
            TitleName.Text = "";
            Button_Create.IsEnabled = false;
        }

        private void Button_Choose_Click(object sender, RoutedEventArgs e)
        {
            if (album.searchMusic())
                Button_Create.IsEnabled = true;
            else
                Button_Create.IsEnabled = false;
        }
    }
}
