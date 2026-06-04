using BasicFit_CETIN_CADAR_HAJJI.Modele;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Utilisateur
    {
        private int utilisateurId;
        private string login;
        private string motDePasse;
        private string role;

        public int UtilisateurId
        {
            get
            {
                return this.utilisateurId;
            }

            set
            {
                this.utilisateurId = value;
            }
        }

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                this.login = value;
            }
        }

        public string MotDePasse
        {
            get
            {
                return this.motDePasse;
            }

            set
            {
                this.motDePasse = value;
            }
        }

        public string Role
        {
            get
            {
                return this.role;
            }

            set
            {
                this.role = value;
            }
        }
        public Utilisateur FindByLogin(string login)
        {
            using (var cmd = new NpgsqlCommand("SELECT * FROM utilisateur WHERE login = @login"))
            {
                cmd.Parameters.AddWithValue("login", login);
                DataTable dt = DataAccess.ExecuteSelect(cmd);
                if (dt.Rows.Count == 0) return null;
                Utilisateur u = new Utilisateur();
                u.Login = (string)dt.Rows[0]["login"];
                u.Role = (string)dt.Rows[0]["role"];
                return u;
            }
        }
    }
}
    

