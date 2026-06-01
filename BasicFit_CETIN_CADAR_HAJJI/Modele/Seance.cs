using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Seance : ICrud<Seance>, INotifyPropertyChanged
    {
        private int seanceId;
        private int jour;
        private TimeSpan heureDebut;
        private TimeSpan heureFin;
        private int nbPlaces;
        private Salle salle;
        private Cours cours;
        private Entraineur entraineur;

        public event PropertyChangedEventHandler? PropertyChanged;

        // Dictionnaire pour convertir le chiffre en nom du jour
        public static readonly Dictionary<int, string> NomsJours = new Dictionary<int, string>
        {
            { 1, "Lundi" }, { 2, "Mardi" }, { 3, "Mercredi" },
            { 4, "Jeudi" }, { 5, "Vendredi" }, { 6, "Samedi" }, { 7, "Dimanche" }
        };

        public Seance()
        {
            this.salle = new Salle();
            this.cours = new Cours();
            this.entraineur = new Entraineur();
        }

        public Seance(int seanceId, int jour, TimeSpan heureDebut, TimeSpan heureFin,
                      int nbPlaces, Salle salle, Cours cours, Entraineur entraineur)
        {
            this.SeanceId = seanceId;
            this.Jour = jour;
            this.HeureDebut = heureDebut;
            this.HeureFin = heureFin;
            this.NbPlaces = nbPlaces;
            this.Salle = salle;
            this.Cours = cours;
            this.Entraineur = entraineur;
        }

        public int SeanceId
        {
            get { return this.seanceId; }
            set
            {
                this.seanceId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SeanceId)));
            }
        }

        public int Jour
        {
            get { return this.jour; }
            set
            {
                if (value < 1 || value > 7) throw new ArgumentException("Le jour doit être entre 1 et 7");
                this.jour = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Jour)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JourNom)));
            }
        }

        // Propriété calculée : affiche "Lundi" au lieu de 1
        public string JourNom => NomsJours.ContainsKey(this.jour) ? NomsJours[this.jour] : "";

        public TimeSpan HeureDebut
        {
            get { return this.heureDebut; }
            set
            {
                this.heureDebut = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeureDebut)));
            }
        }

        public TimeSpan HeureFin
        {
            get { return this.heureFin; }
            set
            {
                this.heureFin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeureFin)));
            }
        }

        public int NbPlaces
        {
            get { return this.nbPlaces; }
            set
            {
                if (value < 1) throw new ArgumentException("Le nombre de places doit être >= 1");
                this.nbPlaces = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NbPlaces)));
            }
        }

        public Salle Salle
        {
            get { return this.salle; }
            set
            {
                this.salle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Salle)));
            }
        }

        public Cours Cours
        {
            get { return this.cours; }
            set
            {
                this.cours = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cours)));
            }
        }

        public Entraineur Entraineur
        {
            get { return this.entraineur; }
            set
            {
                this.entraineur = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Entraineur)));
            }
        }

        public int Create()
        {
            using (var cmd = new NpgsqlCommand(@"INSERT INTO seance 
                (cours_id, entraineur_id, salle_id, jour, heure_debut, heure_fin, nb_places)
                VALUES (@coursId, @entraineurId, @salleId, @jour, @debut, @fin, @places)
                RETURNING seance_id"))
            {
                cmd.Parameters.AddWithValue("coursId", this.Cours.CoursId);
                cmd.Parameters.AddWithValue("entraineurId", this.Entraineur.EntraineurId);
                cmd.Parameters.AddWithValue("salleId", this.Salle.SalleId);
                cmd.Parameters.AddWithValue("jour", this.Jour);
                cmd.Parameters.AddWithValue("debut", this.HeureDebut);
                cmd.Parameters.AddWithValue("fin", this.HeureFin);
                cmd.Parameters.AddWithValue("places", this.NbPlaces);
                this.SeanceId = DataAccess.ExecuteInsert(cmd);
                return this.SeanceId;
            }
        }

        public void Read() { throw new NotImplementedException(); }

        public int Update()
        {
            using (var cmd = new NpgsqlCommand(@"UPDATE seance SET
                cours_id=@coursId, entraineur_id=@entraineurId, salle_id=@salleId,
                jour=@jour, heure_debut=@debut, heure_fin=@fin, nb_places=@places
                WHERE seance_id=@id"))
            {
                cmd.Parameters.AddWithValue("coursId", this.Cours.CoursId);
                cmd.Parameters.AddWithValue("entraineurId", this.Entraineur.EntraineurId);
                cmd.Parameters.AddWithValue("salleId", this.Salle.SalleId);
                cmd.Parameters.AddWithValue("jour", this.Jour);
                cmd.Parameters.AddWithValue("debut", this.HeureDebut);
                cmd.Parameters.AddWithValue("fin", this.HeureFin);
                cmd.Parameters.AddWithValue("places", this.NbPlaces);
                cmd.Parameters.AddWithValue("id", this.SeanceId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public int Delete()
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM seance WHERE seance_id=@id"))
            {
                cmd.Parameters.AddWithValue("id", this.SeanceId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Seance> FindAll()
        {
            return FindBySelection("");
        }

        public List<Seance> FindBySelection(string jourStr)
        {
            List<Seance> liste = new List<Seance>();
            string sql = @"
                SELECT s.*,
                       c.cours_nom, c.cours_description,
                       ca.categorie_id, ca.categorie_nom, ca.categorie_description,
                       sa.salle_nom, sa.salle_nb_places,
                       e.entraineur_nom, e.entraineur_prenom
                FROM seance s
                JOIN cours c      ON s.cours_id      = c.cours_id
                JOIN categorie ca ON c.categorie_id  = ca.categorie_id
                JOIN salle sa     ON s.salle_id       = sa.salle_id
                JOIN entraineur e ON s.entraineur_id  = e.entraineur_id";

            if (!string.IsNullOrEmpty(jourStr) && int.TryParse(jourStr, out int jourInt))
                sql += " WHERE s.jour = @jour";

            sql += " ORDER BY s.jour, s.heure_debut";

            using (var cmd = new NpgsqlCommand(sql))
            {
                if (!string.IsNullOrEmpty(jourStr) && int.TryParse(jourStr, out int j))
                    cmd.Parameters.AddWithValue("jour", j);

                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                    liste.Add(BuildFromRow(dr));
            }
            return liste;
        }

        // Nombre d'inscrits pour cette séance
        public int FindNbInscrits()
        {
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM inscription WHERE seance_id=@id"))
            {
                cmd.Parameters.AddWithValue("id", this.SeanceId);
                return int.Parse(DataAccess.ExecuteSelectOneValue(cmd));
            }
        }

        private Seance BuildFromRow(DataRow dr)
        {
            Categorie cat = new Categorie(
                Convert.ToInt32(dr["categorie_id"]),
                (string)dr["categorie_nom"],
                (string)dr["categorie_description"]);
            Cours c = new Cours(
                Convert.ToInt32(dr["cours_id"]),
                (string)dr["cours_nom"],
                (string)dr["cours_description"],
                cat);
            Salle sa = new Salle(
                Convert.ToInt32(dr["salle_id"]),
                (string)dr["salle_nom"],
                Convert.ToInt32(dr["salle_nb_places"]));
            Entraineur e = new Entraineur(
                Convert.ToInt32(dr["entraineur_id"]),
                (string)dr["entraineur_nom"],
                (string)dr["entraineur_prenom"]);

            return new Seance(
                Convert.ToInt32(dr["seance_id"]),
                Convert.ToInt32(dr["jour"]),
                ((TimeOnly)dr["heure_debut"]).ToTimeSpan(),
                ((TimeOnly)dr["heure_fin"]).ToTimeSpan(),
                Convert.ToInt32(dr["nb_places"]),
                sa, c, e);
        }

        public override string ToString() => $"{this.Cours.CoursNom} — {this.JourNom} {this.HeureDebut:hh\\:mm}";
    }
}