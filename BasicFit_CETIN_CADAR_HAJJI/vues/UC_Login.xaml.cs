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

namespace BasicFit_CETIN_CADAR_HAJJI.vues
{
    public partial class UC_Login : UserControl
    {

        public UC_Login()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string mdp = pbMdp.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(mdp))
            {
                tbErreur.Visibility = Visibility.Visible;
                return;
            }

            // TODO étape 5 : remplacer par vrai check BD
            // Pour l'instant on simule avec des logins de test
            string role = "";
            if (login == "employe" && mdp == "1234")
                role = "employe";
            else if (login == "resp" && mdp == "resp")
                role = "responsable";
            else
            {
                tbErreur.Visibility = Visibility.Visible;
                return;
            }

            tbErreur.Visibility = Visibility.Collapsed;
            MainWindow mw = new MainWindow(role);
            mw.Show();
            Window.GetWindow(this).Close();
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
