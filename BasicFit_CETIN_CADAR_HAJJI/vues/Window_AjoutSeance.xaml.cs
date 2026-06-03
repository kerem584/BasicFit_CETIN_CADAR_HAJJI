using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System.Windows;
using System.Windows.Controls;

namespace BasicFit_CETIN_CADAR_HAJJI.vues
{
    public partial class Window_AjoutSeance : Window
    {
        public Seance NouvelleSeance { get; set; }

        public Window_AjoutSeance()
        {
            InitializeComponent();
            ChargeDonnees();
        }

        private void ChargeDonnees()
        {
            try
            {
                cbCours.ItemsSource = new Cours().FindAll();
                cbSalle.ItemsSource = new Salle().FindAll();
                cbEntraineur.ItemsSource = new Entraineur().FindAll();

                // Remplir les heures de 06:00 à 20:00
                for (int h = 6; h <= 20; h++)
                {
                    string heure = h.ToString("00") + ":00";
                    cbHeureDebut.Items.Add(heure);
                    cbHeureFin.Items.Add(heure);

                    if (h < 20)
                    {
                        string heure30 = h.ToString("00") + ":30";
                        cbHeureDebut.Items.Add(heure30);
                        cbHeureFin.Items.Add(heure30);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Quand heure début change, met à jour heure fin pour n'afficher que les heures après
        private void cbHeureDebut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbHeureDebut.SelectedItem == null) return;

            TimeSpan heureDebut = TimeSpan.Parse(cbHeureDebut.SelectedItem.ToString());

            cbHeureFin.Items.Clear();
            for (int h = 6; h <= 20; h++)
            {
                TimeSpan h00 = new TimeSpan(h, 0, 0);
                TimeSpan h30 = new TimeSpan(h, 30, 0);

                if (h00 > heureDebut)
                    cbHeureFin.Items.Add(h.ToString("00") + ":00");

                if (h30 > heureDebut && h < 20)
                    cbHeureFin.Items.Add(h.ToString("00") + ":30");
            }

            cbHeureFin.SelectedIndex = 0;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            // Vérification champs obligatoires
            if (cbCours.SelectedItem == null || cbSalle.SelectedItem == null
                || cbEntraineur.SelectedItem == null || cbJour.SelectedItem == null
                || string.IsNullOrWhiteSpace(cbHeureDebut.Text)
                || string.IsNullOrWhiteSpace(cbHeureFin.Text)
                || string.IsNullOrWhiteSpace(tbCapacite.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires",
                                "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Vérification format heures
            if (cbHeureDebut.SelectedItem == null || cbHeureFin.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner les heures",
                                "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TimeSpan heureDebut = TimeSpan.Parse(cbHeureDebut.SelectedItem.ToString());
            TimeSpan heureFin = TimeSpan.Parse(cbHeureFin.SelectedItem.ToString());

            // Vérification capacité
            if (!int.TryParse(tbCapacite.Text, out int capacite) || capacite < 1)
            {
                MessageBox.Show("La capacité doit être un nombre >= 1",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Récupérer le numéro du jour
            string jourNom = ((ComboBoxItem)cbJour.SelectedItem).Content.ToString();
            int jourNum = Seance.NomsJours.FirstOrDefault(x => x.Value == jourNom).Key;

            try
            {
                NouvelleSeance = new Seance();
                NouvelleSeance.Cours = (Cours)cbCours.SelectedItem;
                NouvelleSeance.Salle = (Salle)cbSalle.SelectedItem;
                NouvelleSeance.Entraineur = (Entraineur)cbEntraineur.SelectedItem;
                NouvelleSeance.Jour = jourNum;
                NouvelleSeance.HeureDebut = heureDebut;
                NouvelleSeance.HeureFin = heureFin;
                NouvelleSeance.NbPlaces = capacite;

                NouvelleSeance.Create();    // Ajout en BD

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur à l'ajout : " + ex.Message,
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