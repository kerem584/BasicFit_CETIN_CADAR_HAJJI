using BasicFit_CETIN_CADAR_HAJJI.Modele;
using BasicFit_CETIN_CADAR_HAJJI.vues;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BasicFit_CETIN_CADAR_HAJJI.Responsable
{
    public partial class UC_AccueilResponsable : UserControl
    {
        private ObservableCollection<SeanceViewModel> lesSeances;

        public UC_AccueilResponsable()
        {
            InitializeComponent();
            tbDate.Text = "vue du jour : " + DateTime.Now.ToString("dddd dd MMMM yyyy",
                          new System.Globalization.CultureInfo("fr-FR"));
            ChargeDonnees();
        }

        private void ChargeDonnees()
        {
            try
            {
                lesSeances = new ObservableCollection<SeanceViewModel>();
                foreach (Seance s in new Seance().FindAll())
                    lesSeances.Add(new SeanceViewModel(s));

                cbJour.Items.Add("Tous");
                cbJour.Items.Add("Lundi");
                cbJour.Items.Add("Mardi");
                cbJour.Items.Add("Mercredi");
                cbJour.Items.Add("Jeudi");
                cbJour.Items.Add("Vendredi");
                cbJour.Items.Add("Samedi");
                cbJour.Items.Add("Dimanche");
                cbJour.SelectedIndex = 0;

                dgSeances.ItemsSource = lesSeances;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message,
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbJour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lesSeances == null) return;

            if (cbJour.SelectedItem.ToString() == "Tous")
            {
                dgSeances.ItemsSource = lesSeances;
                return;
            }

            ObservableCollection<SeanceViewModel> filtrees = new ObservableCollection<SeanceViewModel>();
            foreach (SeanceViewModel svm in lesSeances)
                if (svm.Seance.JourNom == cbJour.SelectedItem.ToString())
                    filtrees.Add(svm);

            dgSeances.ItemsSource = filtrees;
        }

        private void btnRendreIndisponible_Click(object sender, RoutedEventArgs e)
        {
            SeanceViewModel svm = (SeanceViewModel)((Button)sender).DataContext;

            MessageBoxResult result = MessageBox.Show(
                $"Voulez-vous rendre la séance '{svm.Seance.Cours.CoursNom}' indisponible ?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                svm.Statut = "Indisponible";
        }

        private void btnAjouterSeance_Click(object sender, RoutedEventArgs e)
        {
            Window_AjoutSeance fenetre = new Window_AjoutSeance();
            if (fenetre.ShowDialog() == true)
            {
                lesSeances.Add(new SeanceViewModel(fenetre.NouvelleSeance));
            }
        }
    }

    // Classe helper pour gérer le statut visuel sans toucher à la BD
    public class SeanceViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private string statut;
        public Seance Seance { get; set; }

        public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

        public SeanceViewModel(Seance seance)
        {
            this.Seance = seance;
            this.statut = "Disponible";
        }

        // Propriétés déléguées vers Seance pour le binding DataGrid
        public TimeSpan HeureDebut => Seance.HeureDebut;
        public TimeSpan HeureFin => Seance.HeureFin;
        public Cours Cours => Seance.Cours;
        public Salle Salle => Seance.Salle;
        public Entraineur Entraineur => Seance.Entraineur;
        public int NbPlaces => Seance.NbPlaces;

        public string Statut
        {
            get { return this.statut; }
            set
            {
                this.statut = value;
                PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(Statut)));
            }
        }
    }
}