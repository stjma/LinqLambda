using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLambda
{
    class Personne
    {
        public Personne(string nom, string prenom, int no, string dept, double salaire)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.NoEmploye = no;
            this.Departement = dept;
            this.Salaire = salaire;
        }


        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int NoEmploye { get; set; }
        public string Departement { get; set; }
        public double Salaire { get; set; }


        public override string ToString()
        {
            return string.Format("{0} {1}  : No Employé {2} Departement : {3}  Salaire : {4}", this.Nom, this.Prenom, this.NoEmploye, this.Departement, this.Salaire);
        }
    }
}
