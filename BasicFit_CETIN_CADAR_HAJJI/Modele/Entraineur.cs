using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Entraineur : ICrud<Entraineur>, INotifyPropertyChanged
    {
        private int entraineurId;
        private string entraineurNom;
        private string entraineurPrenom;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Entraineur()
        {
            this.entraineurNom = "";
            this.entraineurPrenom = "";
        }

        public Entraineur(int entraineurId, string entraineurNom, string entraineurPrenom)
        {
            this.EntraineurId = entraineurId;
            this.EntraineurNom = entraineurNom;
            this.EntraineurPrenom = entraineurPrenom;
        }

        public int EntraineurId
        {
            get { return this.entraineurId; }
            set
            {
                this.entraineurId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EntraineurId)));
            }
        }

        public string EntraineurNom
        {
            get { return this.entraineurNom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le nom est obligatoire");
                this.entraineurNom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EntraineurNom)));
            }
        }

        public string EntraineurPrenom
        {
            get { return this.entraineurPrenom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le prénom est obligatoire");
                this.entraineurPrenom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EntraineurPrenom)));
            }
        }

        public int Create()
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO entraineur (entraineur_nom, entraineur_prenom) VALUES (@nom, @prenom) RETURNING entraineur_id"))
            {
                cmd.Parameters.AddWithValue("nom", this.EntraineurNom);
                cmd.Parameters.AddWithValue("prenom", this.EntraineurPrenom);
                this.EntraineurId = DataAccess.ExecuteInsert(cmd);
                return this.EntraineurId;
            }
        }

        public void Read() { throw new NotImplementedException(); }

        public int Update()
        {
            using (var cmd = new NpgsqlCommand("UPDATE entraineur SET entraineur_nom=@nom, entraineur_prenom=@prenom WHERE entraineur_id=@id"))
            {
                cmd.Parameters.AddWithValue("nom", this.EntraineurNom);
                cmd.Parameters.AddWithValue("prenom", this.EntraineurPrenom);
                cmd.Parameters.AddWithValue("id", this.EntraineurId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public int Delete()
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM entraineur WHERE entraineur_id=@id"))
            {
                cmd.Parameters.AddWithValue("id", this.EntraineurId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Entraineur> FindAll()
        {
            List<Entraineur> liste = new List<Entraineur>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM entraineur ORDER BY entraineur_nom"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                    liste.Add(new Entraineur(
                        Convert.ToInt32(dr["entraineur_id"]),
                        (string)dr["entraineur_nom"],
                        (string)dr["entraineur_prenom"]));
            }
            return liste;
        }

        public List<Entraineur> FindBySelection(string criteres) { throw new NotImplementedException(); }

        public override string ToString() => $"{this.EntraineurPrenom} {this.EntraineurNom}";
    }
}