using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Employe
{
    public partial class UC_CoursCollectifs : UserControl
    {
        private ObservableCollection<Seance> lesSeances;

        public UC_CoursCollectifs()
        {
            InitializeComponent();
            ChargeDonnees();
            dgSeances.Items.Filter = FiltreSeances;
        }

        private void ChargeDonnees()
        {
            try
            {
                lesSeances = new ObservableCollection<Seance>(new Seance().FindAll());
                dgSeances.ItemsSource = lesSeances;

                // Remplir ComboBox catégories
                ObservableCollection<Categorie> lesCategories =
                    new ObservableCollection<Categorie>(new Categorie().FindAll());
                cbFiltreCategorie.Items.Add(new Categorie(0, "Toutes", ""));
                foreach (Categorie cat in lesCategories)
                    cbFiltreCategorie.Items.Add(cat);
                cbFiltreCategorie.SelectedIndex = 0;

                // Remplir ComboBox jours
                cbFiltreJour.Items.Add("Tous");
                cbFiltreJour.Items.Add("Lundi");
                cbFiltreJour.Items.Add("Mardi");
                cbFiltreJour.Items.Add("Mercredi");
                cbFiltreJour.Items.Add("Jeudi");
                cbFiltreJour.Items.Add("Vendredi");
                cbFiltreJour.Items.Add("Samedi");
                cbFiltreJour.Items.Add("Dimanche");
                cbFiltreJour.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Erreur de chargement : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ===== FILTRE =====
        private bool FiltreSeances(object obj)
        {
            Seance s = obj as Seance;

            // Filtre catégorie
            if (cbFiltreCategorie.SelectedItem is Categorie catSelectionnee
                && catSelectionnee.CategorieId != 0)
                if (s.Cours.Categorie.CategorieId != catSelectionnee.CategorieId)
                    return false;

            // Filtre jour
            if (cbFiltreJour.SelectedItem != null
                && cbFiltreJour.SelectedItem.ToString() != "Tous")
                if (s.JourNom != cbFiltreJour.SelectedItem.ToString())
                    return false;

            // Filtre places disponibles
            if (cbPlacesDispo.IsChecked == true)
                if (s.FindNbInscrits() >= s.NbPlaces)
                    return false;

            return true;
        }

        private void Filtre_Changed(object sender, RoutedEventArgs e)
        {
            if (dgSeances.ItemsSource == null) return;
            CollectionViewSource.GetDefaultView(dgSeances.ItemsSource).Refresh();
        }

        // ===== SÉLECTION =====
        private void dgSeances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSeances.SelectedItem == null)
            {
                gbDetail.Visibility = Visibility.Collapsed;
                return;
            }

            Seance s = (Seance)dgSeances.SelectedItem;

            tbDetailCours.Text = s.Cours.CoursNom;
            tbDetailEntraineur.Text = s.Entraineur.EntraineurPrenom + " " + s.Entraineur.EntraineurNom;
            tbDetailSalle.Text = s.Salle.SalleNom;
            tbDetailPlaces.Text = (s.NbPlaces - s.FindNbInscrits()).ToString();

            gbDetail.Visibility = Visibility.Visible;
        }
    }
}