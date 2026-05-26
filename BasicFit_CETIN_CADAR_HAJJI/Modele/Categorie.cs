using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Categorie
    {
        private int categorieId;
        private string categorieNom;
        private string categorieDescription;

        public Categorie(int categorieId, string categorieNom, string categorieDescription)
        {
            this.CategorieId = categorieId;
            this.CategorieNom = categorieNom;
            this.CategorieDescription = categorieDescription;
        }

        public int CategorieId
        {
            get
            {
                return this.categorieId;
            }

            set
            {
                this.categorieId = value;
            }
        }

        public string CategorieNom
        {
            get
            {
                return this.categorieNom;
            }

            set
            {
                this.categorieNom = value;
            }
        }

        public string CategorieDescription
        {
            get
            {
                return this.categorieDescription;
            }

            set
            {
                this.categorieDescription = value;
            }
        }
    }
}
