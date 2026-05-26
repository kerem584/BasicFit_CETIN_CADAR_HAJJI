using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Inscription
    {
        private DateTime dateInscription;
        private Client client;
        private Seance seance;

        public Inscription(DateTime dateInscription, Client client, Seance seance)
        {
            this.DateInscription = dateInscription;
            this.Client = client;
            this.Seance = seance;
        }

        public DateTime DateInscription
        {
            get
            {
                return this.dateInscription;
            }

            set
            {
                this.dateInscription = value;
            }
        }

        public Client Client
        {
            get
            {
                return this.client;
            }

            set
            {
                this.client = value;
            }
        }

        public Seance Seance
        {
            get
            {
                return this.seance;
            }

            set
            {
                this.seance = value;
            }
        }
    }
}
