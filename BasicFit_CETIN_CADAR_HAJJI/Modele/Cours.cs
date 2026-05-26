using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Cours
    {
        private int coursId;
        private string coursNom;
        private string coursDescription;
        private Categorie categorie;

        public Cours(int coursId, string coursNom, string coursDescription, Categorie categorie)
        {
            this.CoursId = coursId;
            this.CoursNom = coursNom;
            this.CoursDescription = coursDescription;
            this.Categorie = categorie;
        }

        public int CoursId
        {
            get
            {
                return this.coursId;
            }

            set
            {
                this.coursId = value;
            }
        }

        public string CoursNom
        {
            get
            {
                return this.coursNom;
            }

            set
            {
                this.coursNom = value;
            }
        }

        public string CoursDescription
        {
            get
            {
                return this.coursDescription;
            }

            set
            {
                this.coursDescription = value;
            }
        }

        public Categorie Categorie
        {
            get
            {
                return this.categorie;
            }

            set
            {
                this.categorie = value;
            }
        }
    }
}
