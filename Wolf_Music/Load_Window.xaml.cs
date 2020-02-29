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


namespace Wolf_Music
{
    /// <summary>
    /// Логика взаимодействия для Load_Window.xaml
    /// </summary>
    public partial class Load_Window : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        public Load_Window(){
        InitializeComponent();
        timer = new System.Windows.Threading.DispatcherTimer();
        timer.Tick += new EventHandler(timer_Tick);
        timer.Interval = new TimeSpan(0, 0, 4); 
        timer.Start();
         
           

        } // Load_Window

        private void timer_Tick(object sender, EventArgs e){
            MainWindow main = new MainWindow();
            main.Show();
            timer.Stop();
            this.Close();

        } // timer_Tick

      
    }
}
