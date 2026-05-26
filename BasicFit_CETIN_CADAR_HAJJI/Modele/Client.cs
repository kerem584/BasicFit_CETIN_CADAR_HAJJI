using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Client
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

        private Abonnement abonnement;
        private List<Inscription> inscriptions = new List<Inscription>();

        public Client(int clientId, string nom, string prenom, string mail, string telephone, DateTime dateNaissance, string adresse, string codePostal, string ville, Abonnement abonnement, List<Inscription> inscriptions)
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
            this.Inscriptions = inscriptions;
        }

        public int ClientId
        {
            get
            {
                return this.clientId;
            }

            set
            {
                this.clientId = value;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
            }
        }

        public string Mail
        {
            get
            {
                return this.mail;
            }

            set
            {
                this.mail = value;
            }
        }

        public string Telephone
        {
            get
            {
                return this.telephone;
            }

            set
            {
                this.telephone = value;
            }
        }

        public DateTime DateNaissance
        {
            get
            {
                return this.dateNaissance;
            }

            set
            {
                this.dateNaissance = value;
            }
        }

        public string Adresse
        {
            get
            {
                return this.adresse;
            }

            set
            {
                this.adresse = value;
            }
        }

        public string CodePostal
        {
            get
            {
                return this.codePostal;
            }

            set
            {
                this.codePostal = value;
            }
        }

        public string Ville
        {
            get
            {
                return this.ville;
            }

            set
            {
                this.ville = value;
            }
        }

        public Abonnement Abonnement
        {
            get
            {
                return this.abonnement;
            }

            set
            {
                this.abonnement = value;
            }
        }

        public List<Inscription> Inscriptions
        {
            get
            {
                return this.inscriptions;
            }

            set
            {
                this.inscriptions = value;
            }
        }
    }
}
