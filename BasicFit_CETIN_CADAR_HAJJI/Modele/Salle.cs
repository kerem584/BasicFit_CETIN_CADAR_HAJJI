using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Salle : ICrud<Salle>, INotifyPropertyChanged
    {
        private int salleId;
        private string salleNom;
        private int salleNbPlaces;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Salle()
        {
            this.salleNom = "";
        }

        public Salle(int salleId, string salleNom, int salleNbPlaces)
        {
            this.SalleId = salleId;
            this.SalleNom = salleNom;
            this.SalleNbPlaces = salleNbPlaces;
        }

        public int SalleId
        {
            get { return this.salleId; }
            set
            {
                this.salleId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SalleId)));
            }
        }

        public string SalleNom
        {
            get { return this.salleNom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le nom est obligatoire");
                this.salleNom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SalleNom)));
            }
        }

        public int SalleNbPlaces
        {
            get { return this.salleNbPlaces; }
            set
            {
                if (value < 1) throw new ArgumentException("Le nombre de places doit être >= 1");
                this.salleNbPlaces = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SalleNbPlaces)));
            }
        }

        public int Create()
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO salle (salle_nom, salle_nb_places) VALUES (@nom, @places) RETURNING salle_id"))
            {
                cmd.Parameters.AddWithValue("nom", this.SalleNom);
                cmd.Parameters.AddWithValue("places", this.SalleNbPlaces);
                this.SalleId = DataAccess.ExecuteInsert(cmd);
                return this.SalleId;
            }
        }

        public void Read() { throw new NotImplementedException(); }

        public int Update()
        {
            using (var cmd = new NpgsqlCommand("UPDATE salle SET salle_nom=@nom, salle_nb_places=@places WHERE salle_id=@id"))
            {
                cmd.Parameters.AddWithValue("nom", this.SalleNom);
                cmd.Parameters.AddWithValue("places", this.SalleNbPlaces);
                cmd.Parameters.AddWithValue("id", this.SalleId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public int Delete()
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM salle WHERE salle_id=@id"))
            {
                cmd.Parameters.AddWithValue("id", this.SalleId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Salle> FindAll()
        {
            List<Salle> liste = new List<Salle>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM salle ORDER BY salle_nom"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                    liste.Add(new Salle(
                        Convert.ToInt32(dr["salle_id"]),
                        (string)dr["salle_nom"],
                        Convert.ToInt32(dr["salle_nb_places"])));
            }
            return liste;
        }

        public List<Salle> FindBySelection(string criteres) { throw new NotImplementedException(); }

        public override string ToString() => this.SalleNom;
    }
}