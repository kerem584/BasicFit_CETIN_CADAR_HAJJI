using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System.Windows;

namespace BasicFit_CETIN_CADAR_HAJJI.vues
{
    public partial class Window_NouveauClient : Window
    {
        public Client NouveauClient { get; set; }

        public Window_NouveauClient()
        {
            InitializeComponent();
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            // Vérification champs obligatoires
            if (string.IsNullOrWhiteSpace(tbNom.Text)
                || string.IsNullOrWhiteSpace(tbPrenom.Text)
                || string.IsNullOrWhiteSpace(tbMail.Text)
                || string.IsNullOrWhiteSpace(tbTelephone.Text)
                || dpDateNaissance.SelectedDate == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires",
                                "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                NouveauClient = new Client();
                NouveauClient.Nom = tbNom.Text;
                NouveauClient.Prenom = tbPrenom.Text;
                NouveauClient.Mail = tbMail.Text;
                NouveauClient.Telephone = tbTelephone.Text;
                NouveauClient.DateNaissance = dpDateNaissance.SelectedDate.Value;
                NouveauClient.Adresse = tbAdresse.Text;
                NouveauClient.CodePostal = tbCodePostal.Text;
                NouveauClient.Ville = tbVille.Text;

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}