using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Salle
    {
        private int salleId;
        private string salleNom;
        private int salleNbPlaces;

        public Salle(int salleId, string salleNom, int salleNbPlaces)
        {
            this.SalleId = salleId;
            this.SalleNom = salleNom;
            this.SalleNbPlaces = salleNbPlaces;
        }

        public int SalleId
        {
            get
            {
                return this.salleId;
            }

            set
            {
                this.salleId = value;
            }
        }

        public string SalleNom
        {
            get
            {
                return this.salleNom;
            }

            set
            {
                this.salleNom = value;
            }
        }

        public int SalleNbPlaces
        {
            get
            {
                return this.salleNbPlaces;
            }

            set
            {
                this.salleNbPlaces = value;
            }
        }
    }
}
