using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Cours : ICrud<Cours>, INotifyPropertyChanged
    {
        private int coursId;
        private string coursNom;
        private string coursDescription;
        private Categorie categorie;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Cours()
        {
            this.coursNom = "";
            this.coursDescription = "";
            this.categorie = new Categorie();
        }

        public Cours(int coursId, string coursNom, string coursDescription, Categorie categorie)
        {
            this.CoursId = coursId;
            this.CoursNom = coursNom;
            this.CoursDescription = coursDescription;
            this.Categorie = categorie;
        }

        public int CoursId
        {
            get { return this.coursId; }
            set
            {
                this.coursId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoursId)));
            }
        }

        public string CoursNom
        {
            get { return this.coursNom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le nom est obligatoire");
                this.coursNom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoursNom)));
            }
        }

        public string CoursDescription
        {
            get { return this.coursDescription; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("La description est obligatoire");
                this.coursDescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoursDescription)));
            }
        }

        public Categorie Categorie
        {
            get { return this.categorie; }
            set
            {
                this.categorie = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Categorie)));
            }
        }

        public int Create()
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO cours (cours_nom, cours_description, categorie_id) VALUES (@nom, @desc, @catId) RETURNING cours_id"))
            {
                cmd.Parameters.AddWithValue("nom", this.CoursNom);
                cmd.Parameters.AddWithValue("desc", this.CoursDescription);
                cmd.Parameters.AddWithValue("catId", this.Categorie.CategorieId);
                this.CoursId = DataAccess.ExecuteInsert(cmd);
                return this.CoursId;
            }
        }

        public void Read() { throw new NotImplementedException(); }

        public int Update()
        {
            using (var cmd = new NpgsqlCommand("UPDATE cours SET cours_nom=@nom, cours_description=@desc, categorie_id=@catId WHERE cours_id=@id"))
            {
                cmd.Parameters.AddWithValue("nom", this.CoursNom);
                cmd.Parameters.AddWithValue("desc", this.CoursDescription);
                cmd.Parameters.AddWithValue("catId", this.Categorie.CategorieId);
                cmd.Parameters.AddWithValue("id", this.CoursId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public int Delete()
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM cours WHERE cours_id=@id"))
            {
                cmd.Parameters.AddWithValue("id", this.CoursId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Cours> FindAll()
        {
            List<Cours> liste = new List<Cours>();
            using (var cmd = new NpgsqlCommand(@"
                SELECT c.*, ca.categorie_nom, ca.categorie_description
                FROM cours c
                JOIN categorie ca ON c.categorie_id = ca.categorie_id
                ORDER BY c.cours_nom"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    Categorie cat = new Categorie(
                        Convert.ToInt32(dr["categorie_id"]),
                        (string)dr["categorie_nom"],
                        (string)dr["categorie_description"]);
                    liste.Add(new Cours(
                        Convert.ToInt32(dr["cours_id"]),
                        (string)dr["cours_nom"],
                        (string)dr["cours_description"],
                        cat));
                }
            }
            return liste;
        }

        public List<Cours> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => this.CoursNom;
    }
}