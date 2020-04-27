using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для RedactWin.xaml
    /// </summary>
    public partial class RedactWin : Window
    {
        private string full_name = ""; 
        public RedactWin(string fileName, string name, string album, string fullname)
        {
            InitializeComponent();
            TB_ALBUM.Text = album;
            TB_NAme.Text = name;
            TB_File.Text = fileName;
            full_name = fullname;
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

        private void Button_Minim_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        } // Button_Minim_Click

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Button_Close_Click

        private void BUTT_EDIT_Click(object sender, RoutedEventArgs e)
        {
            Edit editing = new Edit(full_name);
            editing.FileEditTag(TB_File.Text, TB_NAme.Text, TB_ALBUM.Text);
        } // BUTT_EDIT_Click
    }
}
