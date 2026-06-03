using BasicFit_CETIN_CADAR_HAJJI;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Categorie : ICrud<Categorie>, INotifyPropertyChanged
    {
        private int categorieId;
        private string categorieNom;
        private string categorieDescription;

        public event PropertyChangedEventHandler? PropertyChanged;

        // Constructeur vide (obligatoire pour new Categorie())
        public Categorie()
        {
            this.categorieNom = "";
            this.categorieDescription = "";
        }

        // Constructeur avec paramètres
        public Categorie(int categorieId, string categorieNom, string categorieDescription)
        {
            this.CategorieId = categorieId;
            this.CategorieNom = categorieNom;
            this.CategorieDescription = categorieDescription;
        }

        public int CategorieId
        {
            get { return this.categorieId; }
            set
            {
                this.categorieId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategorieId)));
            }
        }

        public string CategorieNom
        {
            get { return this.categorieNom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le nom est obligatoire");
                this.categorieNom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategorieNom)));
            }
        }

        public string CategorieDescription
        {
            get { return this.categorieDescription; }
            set
            {
                this.categorieDescription = value ?? "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategorieDescription)));
            }

        }

        public int Create()
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO categorie (categorie_nom, categorie_description) VALUES (@nom, @desc) RETURNING categorie_id"))
            {
                cmd.Parameters.AddWithValue("nom", this.CategorieNom);
                cmd.Parameters.AddWithValue("desc", this.CategorieDescription);
                this.CategorieId = DataAccess.ExecuteInsert(cmd);
                return this.CategorieId;
            }
        }

        public void Read() { throw new NotImplementedException(); }

        public int Update()
        {
            using (var cmd = new NpgsqlCommand("UPDATE categorie SET categorie_nom=@nom, categorie_description=@desc WHERE categorie_id=@id"))
            {
                cmd.Parameters.AddWithValue("nom", this.CategorieNom);
                cmd.Parameters.AddWithValue("desc", this.CategorieDescription);
                cmd.Parameters.AddWithValue("id", this.CategorieId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public int Delete()
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM categorie WHERE categorie_id=@id"))
            {
                cmd.Parameters.AddWithValue("id", this.CategorieId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Categorie> FindAll()
        {
            List<Categorie> liste = new List<Categorie>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM categorie ORDER BY categorie_nom"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                    liste.Add(new Categorie(
                        Convert.ToInt32(dr["categorie_id"]),
                        (string)dr["categorie_nom"],
                        (string)dr["categorie_description"]));
            }
            return liste;
        }

        public List<Categorie> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => this.CategorieNom;
    }
}