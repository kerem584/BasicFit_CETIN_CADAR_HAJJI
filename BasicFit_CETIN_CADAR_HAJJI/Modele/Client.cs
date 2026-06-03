using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Client : ICrud<Client>, INotifyPropertyChanged
    {
        private int clientId;
        private string nom;
        private string prenom;
        private string mail;
        private string telephone;
        private DateTime dateNaissance;
        private string adresse;
        private string codePostal;
        private string ville;
        private Abonnement? abonnement;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Client()
        {
            this.nom = "";
            this.prenom = "";
            this.mail = "";
            this.telephone = "";
            this.adresse = "";
            this.codePostal = "";
            this.ville = "";
            this.dateNaissance = DateTime.Today;
        }

        public Client(int clientId, string nom, string prenom, string mail, string telephone,
                      DateTime dateNaissance, string adresse, string codePostal, string ville,
                      Abonnement? abonnement)
        {
            this.ClientId = clientId;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Mail = mail;
            this.Telephone = telephone;
            this.DateNaissance = dateNaissance;
            this.Adresse = adresse;
            this.CodePostal = codePostal;
            this.Ville = ville;
            this.Abonnement = abonnement;
        }

        public int ClientId
        {
            get { return this.clientId; }
            set { this.clientId = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClientId))); }
        }

        public string Nom
        {
            get { return this.nom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le nom est obligatoire");
                this.nom = value.ToUpper();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nom)));
            }
        }

        public string Prenom
        {
            get { return this.prenom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le prénom est obligatoire");
                this.prenom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prenom)));
            }
        }

        public string Mail
        {
            get { return this.mail; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("L'email est obligatoire");
                if (!value.Contains("@")) throw new ArgumentException("L'email n'est pas valide");
                this.mail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mail)));
            }
        }

        public string Telephone
        {
            get { return this.telephone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le téléphone est obligatoire");
                this.telephone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Telephone)));
            }
        }

        public DateTime DateNaissance
        {
            get { return this.dateNaissance; }
            set
            {
                if (value > DateTime.Today) throw new ArgumentException("La date de naissance ne peut pas être dans le futur");
                this.dateNaissance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateNaissance)));
            }
        }

        public string Adresse
        {
            get { return this.adresse; }
            set { this.adresse = value ?? ""; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Adresse))); }
        }

        public string CodePostal
        {
            get { return this.codePostal; }
            set { this.codePostal = value ?? ""; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CodePostal))); }
        }

        public string Ville
        {
            get { return this.ville; }
            set { this.ville = value ?? ""; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ville))); }
        }

        public Abonnement? Abonnement
        {
            get { return this.abonnement; }
            set { this.abonnement = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Abonnement))); }
        }

        public int Create()
        {
            using (var cmd = new NpgsqlCommand(@"INSERT INTO client 
                (nom, prenom, mail, telephone, date_naissance, adresse, code_postal, ville)
                VALUES (@nom, @prenom, @mail, @tel, @ddn, @adresse, @cp, @ville) 
                RETURNING client_id"))
            {
                cmd.Parameters.AddWithValue("nom", this.Nom);
                cmd.Parameters.AddWithValue("prenom", this.Prenom);
                cmd.Parameters.AddWithValue("mail", this.Mail);
                cmd.Parameters.AddWithValue("tel", this.Telephone);
                cmd.Parameters.AddWithValue("ddn", this.DateNaissance);
                cmd.Parameters.AddWithValue("adresse", this.Adresse);
                cmd.Parameters.AddWithValue("cp", this.CodePostal);
                cmd.Parameters.AddWithValue("ville", this.Ville);
                this.ClientId = DataAccess.ExecuteInsert(cmd);
                return this.ClientId;
            }
        }

        public void Read() { throw new NotImplementedException(); }

        public int Update()
        {
            using (var cmd = new NpgsqlCommand(@"UPDATE client SET 
                nom=@nom, prenom=@prenom, mail=@mail, telephone=@tel,
                date_naissance=@ddn, adresse=@adresse, code_postal=@cp, ville=@ville
                WHERE client_id=@id"))
            {
                cmd.Parameters.AddWithValue("nom", this.Nom);
                cmd.Parameters.AddWithValue("prenom", this.Prenom);
                cmd.Parameters.AddWithValue("mail", this.Mail);
                cmd.Parameters.AddWithValue("tel", this.Telephone);
                cmd.Parameters.AddWithValue("ddn", this.DateNaissance);
                cmd.Parameters.AddWithValue("adresse", this.Adresse);
                cmd.Parameters.AddWithValue("cp", this.CodePostal);
                cmd.Parameters.AddWithValue("ville", this.Ville);
                cmd.Parameters.AddWithValue("id", this.ClientId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public int Delete()
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM client WHERE client_id=@id"))
            {
                cmd.Parameters.AddWithValue("id", this.ClientId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Client> FindAll()
        {
            List<Client> liste = new List<Client>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM client ORDER BY nom"))
            {
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                    liste.Add(BuildFromRow(dr));
            }
            return liste;
        }

        public List<Client> FindByNomOuMail(string critere)
        {
            List<Client> liste = new List<Client>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM client WHERE LOWER(nom) LIKE @critere OR LOWER(mail) LIKE @critere ORDER BY nom"))
            {
                cmd.Parameters.AddWithValue("critere", "%" + critere.ToLower() + "%");
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                    liste.Add(BuildFromRow(dr));
            }
            return liste;
        }

        public List<Client> FindBySelection(string criteres) { throw new NotImplementedException(); }

        private Client BuildFromRow(DataRow dr)
        {
            return new Client(
                Convert.ToInt32(dr["client_id"]),
                (string)dr["nom"],
                (string)dr["prenom"],
                (string)dr["mail"],
                (string)dr["telephone"],
                (DateTime)dr["date_naissance"],
                dr["adresse"] == DBNull.Value ? "" : (string)dr["adresse"],
                dr["code_postal"] == DBNull.Value ? "" : (string)dr["code_postal"],
                dr["ville"] == DBNull.Value ? "" : (string)dr["ville"],
                null);
        }

        public override string ToString() => $"{this.Nom} {this.Prenom}";
    }
}