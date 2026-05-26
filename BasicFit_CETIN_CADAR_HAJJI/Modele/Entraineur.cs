using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Entraineur
    {
        private int entraineurId;
        private string entraineurNom;
        private string entraineurPrenom;

        public Entraineur(int entraineurId, string entraineurNom, string entraineurPrenom)
        {
            this.EntraineurId = entraineurId;
            this.EntraineurNom = entraineurNom;
            this.EntraineurPrenom = entraineurPrenom;
        }

        public int EntraineurId
        {
            get
            {
                return this.entraineurId;
            }

            set
            {
                this.entraineurId = value;
            }
        }

        public string EntraineurNom
        {
            get
            {
                return this.entraineurNom;
            }

            set
            {
                this.entraineurNom = value;
            }
        }

        public string EntraineurPrenom
        {
            get
            {
                return this.entraineurPrenom;
            }

            set
            {
                this.entraineurPrenom = value;
            }
        }
    }
}
