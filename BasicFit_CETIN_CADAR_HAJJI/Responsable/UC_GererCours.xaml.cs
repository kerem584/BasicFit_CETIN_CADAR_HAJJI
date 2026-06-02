using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Responsable
{
    public partial class UC_GererCours : UserControl
    {
        private ObservableCollection<Cours> lesCours;
        private ObservableCollection<Categorie> lesCategories;

        public UC_GererCours()
        {
            InitializeComponent();
            ChargeDonnees();
            dgCours.Items.Filter = FiltreRecherche;
        }

        private void ChargeDonnees()
        {
            try
            {
                lesCours = new ObservableCollection<Cours>(new Cours().FindAll());
                lesCategories = new ObservableCollection<Categorie>(new Categorie().FindAll());
                dgCours.ItemsSource = lesCours;
                cbCategorie.ItemsSource = lesCategories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ===== FILTRE =====
        private bool FiltreRecherche(object obj)
        {
            if (string.IsNullOrEmpty(tbRecherche.Text)) return true;
            Cours c = obj as Cours;
            return c.CoursNom.StartsWith(tbRecherche.Text, StringComparison.OrdinalIgnoreCase)
                || c.Categorie.CategorieNom.StartsWith(tbRecherche.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void tbRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgCours.ItemsSource).Refresh();
        }

        // ===== SÉLECTION =====
        private void dgCours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSupprimer.IsEnabled = dgCours.SelectedItem != null;
        }

        // ===== AJOUTER =====
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            tbNom.Text = "";
            tbDescription.Text = "";
            cbCategorie.SelectedItem = null;
            tbNom.Focus();
        }

        private void btnValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNom.Text)
                || string.IsNullOrWhiteSpace(tbDescription.Text)
                || cbCategorie.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires",
                                "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (Cours c in lesCours)
            {
                if (c.CoursNom.ToLower() == tbNom.Text.ToLower())
                {
                    MessageBox.Show("Un cours avec ce nom existe déjà",
                                    "Doublon", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                Cours nouveau = new Cours();
                nouveau.CoursNom = tbNom.Text;
                nouveau.CoursDescription = tbDescription.Text;
                nouveau.Categorie = (Categorie)cbCategorie.SelectedItem;

                nouveau.Create();
                lesCours.Add(nouveau);

                tbNom.Text = "";
                tbDescription.Text = "";
                cbCategorie.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur à l'ajout : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ===== RENDRE INDISPONIBLE =====
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgCours.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un cours",
                                "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Cours coursSelectionne = (Cours)dgCours.SelectedItem;

            MessageBoxResult result = MessageBox.Show(
                $"Voulez-vous rendre le cours '{coursSelectionne.CoursNom}' indisponible ?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    coursSelectionne.Delete();
                    lesCours.Remove(coursSelectionne);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message,
                                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}