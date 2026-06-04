using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System.Collections.ObjectModel;
using System.Windows;

namespace BasicFit_CETIN_CADAR_HAJJI.Vues
{
    public partial class Window_DetailSeance : Window
    {
        private Seance laSeance;

        public Window_DetailSeance(Seance seance)
        {
            InitializeComponent();
            this.laSeance = seance;
            AfficheDonnees();
        }

        private void AfficheDonnees()
        {
            try
            {
                tbTitre.Text = laSeance.Cours.CoursNom + " - " +
                                   laSeance.HeureDebut.ToString(@"hh\:mm") + "/" +
                                   laSeance.HeureFin.ToString(@"hh\:mm");
                tbEntraineur.Text = laSeance.Entraineur.EntraineurPrenom + " " +
                                    laSeance.Entraineur.EntraineurNom;
                tbSalle.Text = laSeance.Salle.SalleNom;

                ObservableCollection<Inscription> lesInscrits =
                    new ObservableCollection<Inscription>(
                        new Inscription().FindBySeance(laSeance.SeanceId));

                tbInscrits.Text = lesInscrits.Count + " / " + laSeance.NbPlaces;
                dgInscrits.ItemsSource = lesInscrits;

                tbSousTitre.Text = laSeance.Entraineur.EntraineurPrenom + " · " +
                                   laSeance.Salle.SalleNom + " · " +
                                   lesInscrits.Count + " inscrits / " +
                                   laSeance.NbPlaces + " places";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}