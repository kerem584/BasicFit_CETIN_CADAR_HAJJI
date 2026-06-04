using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Inscription : ICrud<Inscription>, INotifyPropertyChanged
    {
        private DateTime dateInscription;
        private Client client;
        private Seance seance;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Inscription()
        {
            this.client = new Client();
            this.seance = new Seance();
            this.dateInscription = DateTime.Today;
        }

        public Inscription(DateTime dateInscription, Client client, Seance seance)
        {
            this.DateInscription = dateInscription;
            this.Client = client;
            this.Seance = seance;
        }

        public DateTime DateInscription
        {
            get { return this.dateInscription; }
            set
            {
                this.dateInscription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateInscription)));
            }
        }

        public Client Client
        {
            get { return this.client; }
            set
            {
                this.client = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Client)));
            }
        }

        public Seance Seance
        {
            get { return this.seance; }
            set
            {
                this.seance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Seance)));
            }
        }

        public int Create()
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO inscription (seance_id, client_id, date_inscription) VALUES (@seanceId, @clientId, @date)"))
            {
                cmd.Parameters.AddWithValue("seanceId", this.Seance.SeanceId);
                cmd.Parameters.AddWithValue("clientId", this.Client.ClientId);
                cmd.Parameters.AddWithValue("date", this.DateInscription);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public void Read() { throw new NotImplementedException(); }
        public int Update() { throw new NotImplementedException(); }

        public int Delete()
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM inscription WHERE seance_id=@seanceId AND client_id=@clientId"))
            {
                cmd.Parameters.AddWithValue("seanceId", this.Seance.SeanceId);
                cmd.Parameters.AddWithValue("clientId", this.Client.ClientId);
                return DataAccess.ExecuteSet(cmd);
            }
        }

        public List<Inscription> FindAll() { throw new NotImplementedException(); }

        public List<Inscription> FindBySelection(string criteres) { throw new NotImplementedException(); }

        // Récupère tous les inscrits d'une séance
        public List<Inscription> FindBySeance(int seanceId)
        {
            List<Inscription> liste = new List<Inscription>();
            using (var cmd = new NpgsqlCommand(@"
                SELECT i.date_inscription, c.*
                FROM inscription i
                JOIN client c ON i.client_id = c.client_id
                WHERE i.seance_id = @seanceId
                ORDER BY c.nom"))
            {
                cmd.Parameters.AddWithValue("seanceId", seanceId);
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    Client client = new Client(
                        Convert.ToInt32(dr["client_id"]),
                        (string)dr["nom"],
                        (string)dr["prenom"],
                        (string)dr["mail"],
                        (string)dr["telephone"],
                        ((DateOnly)dr["date_naissance"]).ToDateTime(TimeOnly.MinValue),
                        dr["adresse"] == DBNull.Value ? "" : (string)dr["adresse"],
                        dr["code_postal"] == DBNull.Value ? "" : (string)dr["code_postal"],
                        dr["ville"] == DBNull.Value ? "" : (string)dr["ville"],
                        null);
                    liste.Add(new Inscription(((DateOnly)dr["date_inscription"]).ToDateTime(TimeOnly.MinValue), client, new Seance { SeanceId = seanceId }));
                }
            }
            return liste;
        }
    }
}