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
            // TODO étape 5 : vérifier en BD
            // Pour l'instant : juste ouvrir la MainWindow pour tester la navigation
            MainWindow mw = new MainWindow();
            mw.Show();
            Window.GetWindow(this).Close();
        }
    }
}
