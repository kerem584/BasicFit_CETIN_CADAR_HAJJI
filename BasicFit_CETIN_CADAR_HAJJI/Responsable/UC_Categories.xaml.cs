using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BasicFit_CETIN_CADAR_HAJJI.Responsable
{

    public partial class UC_Categories : UserControl
    {
        // Notre liste "observable" qui mettra à jour le DataGrid automatiquement
        private ObservableCollection<Categorie> lesCategories;

        // Constructeur
        public UC_Categories()
        {
            InitializeComponent();
            // On charge les données dès l'ouverture de la page
            ChargeDonnees();
        }

        // ==========================================
        // CHARGEMENT DES DONNÉES
        // ==========================================
        private void ChargeDonnees()
        {
            try
            {
                // On récupère toutes les catégories depuis la base de données
                lesCategories = new ObservableCollection<Categorie>(new Categorie().FindAll());
                // On relie la liste au DataGrid
                dgCategories.ItemsSource = lesCategories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message);
            }
        }

        // ==========================================
        // BARRE DE RECHERCHE
        // ==========================================
        // Se déclenche à chaque fois qu'on tape une lettre dans la barre de recherche
        private void tbRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Si la liste n'est pas encore chargée, on ne fait rien
            if (lesCategories == null) return;

            // On récupère le texte tapé, et on le met en minuscules pour faciliter la recherche
            string texteRecherche = Catégories.Text.ToLower();

            // On filtre la liste : on garde uniquement les catégories dont le nom contient le texte tapé
            var listeFiltree = lesCategories.Where(cat => cat.CategorieNom.ToLower().Contains(texteRecherche)).ToList();

            // On affiche cette liste filtrée dans le DataGrid
            dgCategories.ItemsSource = listeFiltree;
        }

        // ==========================================
        // SELECTION D'UNE LIGNE DANS LE DATAGRID
        // ==========================================
        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Cet événement se déclenche quand on clique sur une ligne du tableau.
            // (Tu pourrais l'utiliser plus tard pour remplir les TextBox si tu veux modifier une catégorie existante).
        }

        // ==========================================
        // BOUTON "AJOUTER" (Celui à côté du tableau)
        // ==========================================
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Ce bouton sert juste à préparer une nouvelle saisie
            // On vide les champs texte pour être sûr qu'ils sont propres
            tbNom.Text = "";
            tbDescription.Text = "";

            // On place le curseur clignotant directement dans la case "Nom"
            tbNom.Focus();
        }

        // ==========================================
        // BOUTON "RENDRE INDISPONIBLE" (Supprimer)
        // ==========================================
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // On vérifie d'abord qu'une ligne a bien été sélectionnée dans le tableau
            if (dgCategories.SelectedItem is Categorie categorieSelectionnee)
            {
                // On demande confirmation à l'utilisateur
                MessageBoxResult reponse = MessageBox.Show($"Voulez-vous vraiment rendre '{categorieSelectionnee.CategorieNom}' indisponible ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (reponse == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Si tu as une méthode Delete() dans ta classe Categorie, tu l'appelles ici :
                        categorieSelectionnee.Delete(); 

                        // Puis on la retire de la liste visuelle
                        lesCategories.Remove(categorieSelectionnee);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie dans le tableau d'abord.");
            }
        }

        // ==========================================
        // BOUTON "VALIDER L'AJOUT" (Le formulaire en bas)
        // ==========================================
        private void btnValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            // 1. Vérification des champs obligatoires
            if (string.IsNullOrWhiteSpace(tbNom.Text) || string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires", "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // On arrête la méthode ici
            }

            // 2. Vérification des doublons
            foreach (Categorie cat in lesCategories)
            {
                if (cat.CategorieNom.ToLower() == tbNom.Text.ToLower())
                {
                    MessageBox.Show("Une catégorie avec ce nom existe déjà", "Doublon", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // On arrête la méthode ici
                }
            }

            // 3. Création et sauvegarde
            try
            {
                Categorie nouvelle = new Categorie();
                nouvelle.CategorieNom = tbNom.Text;
                nouvelle.CategorieDescription = tbDescription.Text;

                // Ajout en Base de Données
                nouvelle.Create();

                // Ajout dans la liste visuelle pour mettre à jour le DataGrid
                lesCategories.Add(nouvelle);

                // On vide le formulaire une fois que c'est terminé avec succès
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