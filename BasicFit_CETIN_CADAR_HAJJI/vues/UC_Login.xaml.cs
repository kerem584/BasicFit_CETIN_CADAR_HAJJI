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
        private readonly string placeholder = "ex : a.lacroix";

        public UC_Login()
        {
            InitializeComponent();
        }

        // Placeholder : efface le texte gris au clic
        private void tbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == placeholder)
            {
                tbLogin.Text = "";
                tbLogin.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        // Placeholder : remet le texte gris si vide
        private void tbLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                tbLogin.Text = placeholder;
                tbLogin.Foreground = new SolidColorBrush(Color.FromRgb(156, 163, 175));
            }
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            // TODO étape 5 : vérifier en BD
            // Pour l'instant : juste ouvrir la MainWindow pour tester la navigation
            MainWindow mw = new MainWindow();
            mw.Show();
            Window.GetWindow(this).Close();
        }
    }
}
