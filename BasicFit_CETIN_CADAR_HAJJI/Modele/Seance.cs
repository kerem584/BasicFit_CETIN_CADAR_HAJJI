using BasicFit_CETIN_CADAR_HAJJI.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BasicFit_CETIN_CADAR_HAJJI.Modele
{
    public class Seance
    {
        private int seanceId;
        private string jour;
        private TimeSpan heureDebut;
        private TimeSpan heureFin;
        private int nbPlaces;
        private bool complet;
        private Salle salle;
        private Cours cours;
        private Entraineur entraineur;
        private List<Inscription> inscriptions = new List<Inscription>();

        public Seance(int seanceId, string jour, TimeSpan heureDebut, TimeSpan heureFin, int nbPlaces, bool complet, Salle salle, Cours cours, Entraineur entraineur, List<Inscription> inscriptions)
        {
            this.SeanceId = seanceId;
            this.Jour = jour;
            this.HeureDebut = heureDebut;
            this.HeureFin = heureFin;
            this.NbPlaces = nbPlaces;
            this.Complet = complet;
            this.Salle = salle;
            this.Cours = cours;
            this.Entraineur = entraineur;
            this.Inscriptions = inscriptions;
        }

        public int SeanceId
        {
            get
            {
                return this.seanceId;
            }

            set
            {
                this.seanceId = value;
            }
        }

        public string Jour
        {
            get
            {
                return this.jour;
            }

            set
            {
                this.jour = value;
            }
        }

        public TimeSpan HeureDebut
        {
            get
            {
                return this.heureDebut;
            }

            set
            {
                this.heureDebut = value;
            }
        }

        public TimeSpan HeureFin
        {
            get
            {
                return this.heureFin;
            }

            set
            {
                this.heureFin = value;
            }
        }

        public int NbPlaces
        {
            get
            {
                return this.nbPlaces;
            }

            set
            {
                this.nbPlaces = value;
            }
        }

        public bool Complet
        {
            get
            {
                return this.complet;
            }

            set
            {
                this.complet = value;
            }
        }

        public Salle Salle
        {
            get
            {
                return this.salle;
            }

            set
            {
                this.salle = value;
            }
        }

        public Cours Cours
        {
            get
            {
                return this.cours;
            }

            set
            {
                this.cours = value;
            }
        }

        public Entraineur Entraineur
        {
            get
            {
                return this.entraineur;
            }

            set
            {
                this.entraineur = value;
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
