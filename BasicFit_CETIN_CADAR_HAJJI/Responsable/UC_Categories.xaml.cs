using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BasicFit_CETIN_CADAR_HAJJI.Responsable
{

    public partial class UC_Categories : UserControl
    {

        private ObservableCollection<Categorie> lesCategories;

        public UC_Categories()
        {
            InitializeComponent();
            ChargeDonnees();
        }

        private void ChargeDonnees()
        {
            try
            {
                lesCategories = new ObservableCollection<Categorie>(new Categorie().FindAll());
                dgCategories.ItemsSource = lesCategories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message);
            }
        }

        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // rien pour l'instant
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Vérification champs obligatoires
            if (string.IsNullOrWhiteSpace(tbNom.Text) || string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires",
                                "Champs manquants",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            // Vérification doublon
            foreach (Categorie cat in lesCategories)
            {
                if (cat.CategorieNom.ToLower() == tbNom.Text.ToLower())
                {
                    MessageBox.Show("Une catégorie avec ce nom existe déjà",
                                    "Doublon",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                Categorie nouvelle = new Categorie();
                nouvelle.CategorieNom = tbNom.Text;
                nouvelle.CategorieDescription = tbDescription.Text;

                nouvelle.Create();                  // Ajout en BD
                lesCategories.Add(nouvelle);        // Ajout dans la liste

                // Vider le formulaire
                tbNom.Text = "";
                tbDescription.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur à l'ajout : " + ex.Message);
            }
        }
    }
}