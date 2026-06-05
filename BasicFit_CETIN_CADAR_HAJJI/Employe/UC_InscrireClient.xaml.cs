using BasicFit_CETIN_CADAR_HAJJI.Modele;
using BasicFit_CETIN_CADAR_HAJJI.vues;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Employe
{
    public partial class UC_InscrireClient : UserControl
    {
        private ObservableCollection<Client> lesClients;
        private ObservableCollection<Seance> lesSeances;

        public UC_InscrireClient()
        {
            InitializeComponent();
            ChargeDonnees();
            dgClients.Items.Filter = FiltreClients;
        }

        private void ChargeDonnees()
        {
            try
            {
                lesClients = new ObservableCollection<Client>(new Client().FindAll());
                lesSeances = new ObservableCollection<Seance>(new Seance().FindAll());

                dgClients.ItemsSource = lesClients;
                dgSeances.ItemsSource = lesSeances;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool FiltreClients(object obj)
        {
            if (string.IsNullOrEmpty(tbRecherche.Text)) return true;
            Client c = obj as Client;
            return c.Nom.StartsWith(tbRecherche.Text, StringComparison.OrdinalIgnoreCase)
                || c.Prenom.StartsWith(tbRecherche.Text, StringComparison.OrdinalIgnoreCase)
                || c.Mail.StartsWith(tbRecherche.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void tbRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgClients.ItemsSource == null) return;
            CollectionViewSource.GetDefaultView(dgClients.ItemsSource).Refresh();
        }

        private void dgClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClients.SelectedItem == null)
            {
                tbClientSelectionne.Text = "Aucun client sélectionné";
                return;
            }
            Client c = (Client)dgClients.SelectedItem;
            tbClientSelectionne.Text = c.Nom + " " + c.Prenom;
        }

        private void btnNouveauClient_Click(object sender, RoutedEventArgs e)
        {
            Window_NouveauClient fenetre = new Window_NouveauClient();
            if (fenetre.ShowDialog() == true)
            {
                try
                {
                    fenetre.NouveauClient.Create();
                    lesClients.Add(fenetre.NouveauClient);
                    dgClients.SelectedItem = fenetre.NouveauClient;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur à la création : " + ex.Message,
                                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un client",
                                "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dgSeances.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une séance",
                                "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Client clientSelectionne = (Client)dgClients.SelectedItem;
            Seance seanceSelectionnee = (Seance)dgSeances.SelectedItem;

            int nbInscrits = seanceSelectionnee.FindNbInscrits();
            if (nbInscrits >= seanceSelectionnee.NbPlaces)
            {
                MessageBox.Show("Ce cours est complet, impossible d'inscrire le client",
                                "Cours complet", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (Inscription i in new Inscription().FindBySeance(seanceSelectionnee.SeanceId))
            {
                if (i.Client.ClientId == clientSelectionne.ClientId)
                {
                    MessageBox.Show("Ce client est déjà inscrit à cette séance",
                                    "Doublon", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                Inscription nouvelle = new Inscription();
                nouvelle.Client = clientSelectionne;
                nouvelle.Seance = seanceSelectionnee;
                nouvelle.DateInscription = DateTime.Today;

                nouvelle.Create();

                MessageBox.Show($"{clientSelectionne.Nom} {clientSelectionne.Prenom} inscrit(e) à {seanceSelectionnee.Cours.CoursNom} !",
                                "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                dgClients.SelectedItem = null;
                dgSeances.SelectedItem = null;
                tbClientSelectionne.Text = "Aucun client sélectionné";
                tbSeanceSelectionnee.Text = "Aucune séance sélectionnée";
                tbRecherche.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur à l'inscription : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void dgSeances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSeances.SelectedItem == null)
            {
                tbSeanceSelectionnee.Text = "Aucune séance sélectionnée";
                return;
            }
            Seance s = (Seance)dgSeances.SelectedItem;
            tbSeanceSelectionnee.Text = s.Cours.CoursNom + " — " + s.JourNom + " " + s.HeureDebut.ToString(@"hh\:mm");
        }
    }
}