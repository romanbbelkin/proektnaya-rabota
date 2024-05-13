using System;
using проектная_работа.db;
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
using Microsoft.Win32;
using System.Data.Entity;
using System.IO;

namespace проектная_работа
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private roman_belkinEntities db;
        User selectedUser;
        public Page1()
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

            if(selectedUser.image != null)
            {
                Avatarka.Source = new BitmapImage(new Uri($@"{Directory.GetCurrentDirectory()}\Images\{selectedUser.image}"));
            }

            List<User> users = db.User.ToList();
            _userGrid.ItemsSource = users;

            List<expenses> expenses = db.expenses.ToList();
            FinanceGrid.ItemsSource = expenses;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if(TxbName.Text == "" || TxbSurname.Text == "" || TxbLastame.Text == "" || TxbAge.Text == "" || TxbLogin.Text == "" || TxbLogin.Text == "" || TxbPassword.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            User user = db.User.Where(a => a.login == TxbLogin.Text).FirstOrDefault();
            if(TxbLogin.Text != selectedUser.login && user != null)
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

        private void BtnAddGame_Click(object sender, RoutedEventArgs e)
        {
            if(TxbOpponent.Text == "" || TxbPlace.Text == "" || TxbDate.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            Game game = new Game()
            {
                opponent = TxbOpponent.Text,
                location = TxbPlace.Text,
                date = Convert.ToDateTime(TxbDate.Text)
            };
            db.Game.Add(game);
            db.SaveChanges();
        }

        private void BtnAddStatistic_Click(object sender, RoutedEventArgs e)
        {
            if(TxbTeam_1.Text == "" || TxbTeam_2.Text == "" || TxbScore.Text == "" || TxbDate_Play.Text == "" || TxbStadium.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            Statistic statistic = new Statistic()
            {
                team1 = TxbTeam_1.Text,
                team2 = TxbTeam_2.Text,
                score = TxbScore.Text,
                date = Convert.ToDateTime(TxbDate_Play.Text),
                stadium = TxbStadium.Text
            };
            db.Statistic.Add(statistic);
            db.SaveChanges();
        }

        private void Remove_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(_userGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберете пользователя чтобы удалить");
                return;
            }
            User user = _userGrid.SelectedItem as User;
            db.User.Remove(user);
            db.SaveChanges();
        }

        private void Remove__Btn_Click(object sender, RoutedEventArgs e)
        {
            if(FinanceGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберете затраты чтобы удалить");
                return;
            }
            expenses expenses = FinanceGrid.SelectedItem as expenses;
            db.expenses.Remove(expenses);
            db.SaveChanges();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            WorkFrame.frame.Navigate(new Page2());
        }
    }
}
