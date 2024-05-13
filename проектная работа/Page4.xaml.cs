using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using проектная_работа.db;

namespace проектная_работа
{
    /// <summary>
    /// Логика взаимодействия для Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        private roman_belkinEntities db;
        User selectedUser;
        public Page4()
        {
            InitializeComponent();
            db = new roman_belkinEntities();
            selectedUser = SelectedUser.user;

            TxbName.Text = selectedUser.human1.name;
            TxbSurname.Text = selectedUser.human1.surname;
            TxbLastame.Text = selectedUser.human1.lastname;
            TxbAge.Text = selectedUser.human1.age.ToString();
            TxbLogin.Text = selectedUser.login;
            TxbPassword.Text = selectedUser.password;

            if (selectedUser.image != null)
            {
                Avatarka.Source = new BitmapImage(new Uri($@"{Directory.GetCurrentDirectory()}\Images\{selectedUser.image}"));
            }
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if(TxbAmount.Text == "" || TxbReason.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            expenses expenses = new expenses()
            {
                amount = TxbAmount.Text,
                reason = TxbReason.Text
            };
            db.expenses.Add(expenses);
            db.SaveChanges();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            WorkFrame.frame.Navigate(new Page2());
        }

        private void Button_Click_Avatarka(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Выберите аватарку",
                Multiselect = false,
                Filter = "Фото |*.png;*.jpg"
            };

            if ((bool)fileDialog.ShowDialog())
            {
                string photo = $@"{Guid.NewGuid()}.png";
                string FullPath = $@"{Directory.GetCurrentDirectory()}\Images\{photo}";

                Directory.CreateDirectory(FullPath);

                using (FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    using (FileStream saveFileStream = new FileStream(FullPath, FileMode.Create))
                    {
                        fileStream.CopyTo(saveFileStream);
                    }
                }

                selectedUser.image = photo;
                Avatarka.Source = new BitmapImage(new Uri(FullPath));
                db.SaveChanges();
            }
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if (TxbName.Text == "" || TxbSurname.Text == "" || TxbLastame.Text == "" || TxbAge.Text == "" || TxbLogin.Text == "" || TxbLogin.Text == "" || TxbPassword.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            User user = db.User.Where(a => a.login == TxbLogin.Text).FirstOrDefault();
            if (TxbLogin.Text != selectedUser.login && user != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }
            selectedUser.login = TxbLogin.Text;
            selectedUser.password = TxbPassword.Text;
            selectedUser.human1.name = TxbName.Text;
            selectedUser.human1.surname = TxbSurname.Text;
            selectedUser.human1.lastname = TxbLastame.Text;
            selectedUser.human1.age = Convert.ToInt32(TxbAge.Text);
            db.SaveChanges();
        }
    }
}
