using BasicFit_CETIN_CADAR_HAJJI.Modele;
using BasicFit_CETIN_CADAR_HAJJI.Vues;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BasicFit_CETIN_CADAR_HAJJI.Employe
{
    public partial class UC_AccueilEmploye : UserControl
    {
        private ObservableCollection<Seance> lesSeances;

        public UC_AccueilEmploye()
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
                lesSeances = new ObservableCollection<Seance>(new Seance().FindAll());

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

            ObservableCollection<Seance> seancesFiltrees = new ObservableCollection<Seance>();
            foreach (Seance s in lesSeances)
                if (s.JourNom == cbJour.SelectedItem.ToString())
                    seancesFiltrees.Add(s);

            dgSeances.ItemsSource = seancesFiltrees;
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            Seance seance = (Seance)((Button)sender).DataContext;
            Window_DetailSeance fenetre = new Window_DetailSeance(seance);
            fenetre.ShowDialog();
        }
    }
}