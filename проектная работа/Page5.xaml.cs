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
using System.Windows.Navigation;
using System.Windows.Shapes;
using проектная_работа.db;

namespace проектная_работа
{
    /// <summary>
    /// Логика взаимодействия для Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            User user = WorkDB.db.User.Where(a => a.login == TxbLogin.Text).FirstOrDefault();
            if(user != null && user.password == TxbPassword.Text)
            {
                SelectedUser.user = user;
                if(user.UserType1.name == "trainer")
                {
                    WorkFrame.frame.Navigate(new Page8());
                }
                else if(user.UserType1.name == "manager")
                {
                    WorkFrame.frame.Navigate(new Page4());
                }
                else
                {
                    WorkFrame.frame.Navigate(new Page1());
                }
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
            }
        }
    }
}
