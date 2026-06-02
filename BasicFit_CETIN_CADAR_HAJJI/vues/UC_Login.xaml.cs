using BasicFit_CETIN_CADAR_HAJJI.Modele;
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
            try
            {
                DataAccess.InitializeConnection(login, mdp);
                DataAccess.GetConnection();
                Utilisateur u = new Utilisateur().FindByLogin(login);

                if (u == null)
                {
                    tbErreur.Visibility = Visibility.Visible;
                    return;
                }

                tbErreur.Visibility = Visibility.Collapsed;
                MainWindow mw = new MainWindow(u.Role);
                mw.Show();
                Window.GetWindow(this).Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("La connexion a échoué. verifiez vos login.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            //string role = "";
            //if (login == "employe" && mdp == "1234")
            //    role = "employe";
            //else if (login == "resp" && mdp == "resp")
            //    role = "responsable";
            //else
            //{
            //    tbErreur.Visibility = Visibility.Visible;
            //    return;
            //}

            
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
