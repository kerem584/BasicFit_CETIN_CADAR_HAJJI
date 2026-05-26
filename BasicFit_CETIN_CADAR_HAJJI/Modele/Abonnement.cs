using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Abonnement
    {
        private int abonnementId;
        private string abonnementDescription;
        private decimal tarif;

        public Abonnement(int abonnementId, string abonnementDescription, decimal tarif)
        {
            this.AbonnementId = abonnementId;
            this.AbonnementDescription = abonnementDescription;
            this.Tarif = tarif;
        }

        public int AbonnementId
        {
            get
            {
                return this.abonnementId;
            }

            set
            {
                this.abonnementId = value;
            }
        }

        public string AbonnementDescription
        {
            get
            {
                return this.abonnementDescription;
            }

            set
            {
                this.abonnementDescription = value;
            }
        }

        public decimal Tarif
        {
            get
            {
                return this.tarif;
            }

            set
            {
                this.tarif = value;
            }
        }
    }
}
