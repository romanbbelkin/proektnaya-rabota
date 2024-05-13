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
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if(TxbLastName.Text == "" || TxbLogin.Text == "" || TxbName.Text == "" || TxbRole.Text == "" || TxbSurname.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            UserType userType = WorkDB.db.UserType.Where(a => a.name == TxbRole.Text).FirstOrDefault();
            if(userType == null)
            {
                MessageBox.Show("Такой роли не существует");
                return;
            }
            User existingUser = WorkDB.db.User.Where(a => a.login == TxbLogin.Text).FirstOrDefault();
            if(existingUser != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            human human = new human()
            {
                name = TxbName.Text,
                surname = TxbSurname.Text,
                lastname = TxbLastName.Text,
                age = Convert.ToInt32(TxbAge.Text)
            };

            User user = new User()
            {
                human1 = human,
                login = TxbLogin.Text,
                password = PwbPassword.Text
            };

            WorkDB.db.User.Add(user);
            WorkDB.db.SaveChanges();

            WorkFrame.frame.Navigate(new Page2());
        }
    }
}
