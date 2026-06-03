using BasicFit_CETIN_CADAR_HAJJI.Employe;
using BasicFit_CETIN_CADAR_HAJJI.Responsable;
using BasicFit_CETIN_CADAR_HAJJI.vues;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BasicFit_CETIN_CADAR_HAJJI
{
    public partial class MainWindow : Window
    {
        private string role;

        public MainWindow(string role)
        {
            InitializeComponent();
            this.role = role;
            ChargeMenu();
        }

        private void ChargeMenu()
        {
            if (this.role == "employe")
            {
                tbRoleUser.Text = "Employé d'accueil";

                Button btnAccueil = CreeBouton("Accueil");
                btnAccueil.Click += BtnAccueilEmploye_Click;
                spMenu.Children.Add(btnAccueil);

                Button btnCours = CreeBouton("Cours collectifs");
                btnCours.Click += BtnCoursCollectifs_Click;
                spMenu.Children.Add(btnCours);

                Button btnInscrire = CreeBouton("Inscrire client");
                btnInscrire.Click += BtnInscrireClient_Click;
                spMenu.Children.Add(btnInscrire);

                

                ZoneContenu.Content = new UC_AccueilEmploye();
            }
            else if (this.role == "responsable")
            {
                tbRoleUser.Text = "Responsable du club";

                Button btnAccueil = CreeBouton("Accueil");
                btnAccueil.Click += BtnAccueilResponsable_Click;
                spMenu.Children.Add(btnAccueil);

                Button btnCategories = CreeBouton("Catégories");
                btnCategories.Click += BtnCategories_Click;
                spMenu.Children.Add(btnCategories);

                Button btnGerer = CreeBouton("Gérer les cours");
                btnGerer.Click += BtnGererCours_Click;
                spMenu.Children.Add(btnGerer);

                

                

                ZoneContenu.Content = new UC_AccueilResponsable();
            }
        }

        private Button CreeBouton(string texte)
        {
            Button btn = new Button();
            btn.Content = texte;
            btn.Background = new SolidColorBrush(Colors.Transparent);
            btn.Foreground = new SolidColorBrush(Colors.White);
            btn.BorderThickness = new Thickness(0);
            btn.Padding = new Thickness(16, 10, 16, 10);
            btn.HorizontalContentAlignment = HorizontalAlignment.Left;
            btn.FontSize = 13;
            btn.Cursor = Cursors.Hand;
            return btn;
        }


        private void BtnAccueilEmploye_Click(object sender, RoutedEventArgs e)
        {
            ZoneContenu.Content = new UC_AccueilEmploye();
        }

        private void BtnCoursCollectifs_Click(object sender, RoutedEventArgs e)
        {
            ZoneContenu.Content = new UC_CoursCollectifs();
        }

        private void BtnInscrireClient_Click(object sender, RoutedEventArgs e)
        {
            ZoneContenu.Content = new UC_InscrireClient();
        }

        


        private void BtnAccueilResponsable_Click(object sender, RoutedEventArgs e)
        {
            ZoneContenu.Content = new UC_AccueilResponsable();
        }

        private void BtnGererCours_Click(object sender, RoutedEventArgs e)
        {
            ZoneContenu.Content = new UC_GererCours();
        }

        

        private void BtnCategories_Click(object sender, RoutedEventArgs e)
        {
            ZoneContenu.Content = new UC_Categories();
        }

        // ===== DÉCONNEXION =====
        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}