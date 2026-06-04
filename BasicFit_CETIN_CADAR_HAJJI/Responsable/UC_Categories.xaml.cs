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

        private void tbRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lesCategories == null) return;

            string texteRecherche = Catégories.Text.ToLower();

            var listeFiltree = lesCategories.Where(cat => cat.CategorieNom.ToLower().Contains(texteRecherche)).ToList();

            dgCategories.ItemsSource = listeFiltree;
        }

        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            tbNom.Text = "";
            tbDescription.Text = "";

            tbNom.Focus();
        }

        
        private void btnValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNom.Text) || string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires", "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            foreach (Categorie cat in lesCategories)
            {
                if (cat.CategorieNom.ToLower() == tbNom.Text.ToLower())
                {
                    MessageBox.Show("Une catégorie avec ce nom existe déjà", "Doublon", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; 
                }
            }

            try
            {
                Categorie nouvelle = new Categorie();
                nouvelle.CategorieNom = tbNom.Text;
                nouvelle.CategorieDescription = tbDescription.Text;

                nouvelle.Create();

                lesCategories.Add(nouvelle);

                tbNom.Text = "";
                tbDescription.Text = "";

                MessageBox.Show("La catégorie a été ajoutée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur à l'ajout : " + ex.Message);
            }
        }

        
    }
}