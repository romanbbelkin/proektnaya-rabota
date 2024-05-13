using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page

    {
        
        public Page2()
        {
            InitializeComponent();

            List<Game> games = WorkDB.db.Game.ToList();
            MatchesGrid.ItemsSource = games;

            List<User> users = WorkDB.db.User.Where(a => a.UserType1.name == "trainer").ToList();
            coaching_staff.ItemsSource = users;

            List<Statistic> statistics = WorkDB.db.Statistic.ToList();
            team_statistics.ItemsSource = statistics;

            List<Sponsor> sponsorList = WorkDB.db.Sponsor.ToList();
            sponsors.ItemsSource = sponsorList;
        }

        

        private void Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Есть ли у вас аккаунт?", "Вход или Регистрация", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                WorkFrame.frame.Navigate(new Page5());

            }
            else
            {
                WorkFrame.frame.Navigate(new Page3());
                
            }
        }

        
    }
}
