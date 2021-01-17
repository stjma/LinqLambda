using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLambda
{
    public partial class Form1 : Form
    {
        private delegate double emc2(double kg, double ms);
        private List<Personne> ListPersonne = new List<Personne>();

        Personne personneAjouter;
        private static double Femc2(double kg, double ms)
        {
            return kg * Math.Pow(ms, 2);
        }
        public Form1()
        {
            InitializeComponent();
            txtEnergie.ReadOnly = true;
        }

        private void btnDelegate_Click(object sender, EventArgs e)
        {
            double number;
            if (double.TryParse(txtMasse.Text, out number) && double.TryParse(txtVitesseDeLaLumiere.Text,out number))
            {
                emc2 testEmc2 = Femc2;

                emc2 calcul = delegate(double kg, double ms) { return kg * Math.Pow(ms, 2); };

                txtEnergie.Text = (calcul(double.Parse(txtMasse.Text), double.Parse(txtVitesseDeLaLumiere.Text))).ToString();
            }
          
        }

        private void btnMethodeAnonyme_Click(object sender, EventArgs e)
        {
             double number;
             if (double.TryParse(txtMasse.Text, out number) && double.TryParse(txtVitesseDeLaLumiere.Text, out number))
             {
                 emc2 calcul = delegate(double kg, double ms) { return kg * Math.Pow(ms, 2); };
                 txtEnergie.Text = (calcul(double.Parse(txtMasse.Text), double.Parse(txtVitesseDeLaLumiere.Text))).ToString();
             }
        }

        private void btnLambda_Click(object sender, EventArgs e)
        {
            double number;
            if (double.TryParse(txtMasse.Text, out number) && double.TryParse(txtVitesseDeLaLumiere.Text, out number))
            {
                emc2 calcul = (km, ms) => km * Math.Pow(ms, 2);
                txtEnergie.Text = (calcul(double.Parse(txtMasse.Text), double.Parse(txtVitesseDeLaLumiere.Text))).ToString();
            }
        }

        private void btnFunc_Click(object sender, EventArgs e)
        {
            double number;
            if (double.TryParse(txtMasse.Text, out number) && double.TryParse(txtVitesseDeLaLumiere.Text, out number))
            {
                Func<double, double, double> calcul = (kg, ms) => kg * Math.Pow(ms, 2);
                txtEnergie.Text = (calcul(double.Parse(txtMasse.Text), double.Parse(txtVitesseDeLaLumiere.Text))).ToString();
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            Personne personne = new Personne("bob", "bob", 10, "informatique", 40000);
            Personne personne1 = new Personne("bob1", "bob1", 10, "1", 12.12);
            Personne personne2 = new Personne("bob2", "bob2", 10, "2", 12.12);
            Personne personne3 = new Personne("bob3", "bob3", 10, "3", 12.12);
            Personne personne4 = new Personne("bob4", "bob4", 10, "4", 12.12);

            ListPersonne.Add(personne);
            ListPersonne.Add(personne1);
            ListPersonne.Add(personne2);
            ListPersonne.Add(personne3);
            ListPersonne.Add(personne4);

            var valeur = (from p in ListPersonne select p);
            lstOrg.Items.Clear();
            lstOrg.Items.AddRange(valeur.ToArray());
        }

        private void btnDILinq_Click(object sender, EventArgs e)
        {
            var valeur = from p in ListPersonne
                         where p.Departement == "informatique"
                         select p;
            lstResultat.Items.Clear();
            lstResultat.Items.AddRange(valeur.ToArray());
        }

        private void btnDILambda_Click(object sender, EventArgs e)
        {
            lstResultat.Items.Clear();
            lstResultat.Items.AddRange(ListPersonne.Where(x => x.Departement == "informatique").ToArray());

        }

        private void btnDILinqNew_Click(object sender, EventArgs e)
        {
            var valeur = from p in ListPersonne
                         where p.Departement == "informatique"
                         select new {nom = p.Nom , p.Prenom};
            lstResultat.Items.Clear();
            lstResultat.Items.AddRange(valeur.ToArray());
        }

        private void btnEiLinqLamda_Click(object sender, EventArgs e)
        {
            txtNombre.Text = (from p in ListPersonne select p).Where(x => x.Departement == "informatique").Count().ToString();

            
        }

        private void btnMoyenne_Click(object sender, EventArgs e)
        {
            lstResultat.Items.Clear();
            try
            {
                if ((from p in ListPersonne select p).Count() != 0)
                {
                    txtNombre.Text = (ListPersonne.Where(x => x.Salaire > 30000 && x.Departement == "informatique").Average(x => x.Salaire)).ToString();
                }
                else
                {
                    throw new partieDeuxException("Liste de personne vide");
                }  
            }
            catch (partieDeuxException a)
            {
                lstResultat.Items.Add(a.Message);
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            int intNumero;
            double dbNumero;
            lstResultat.Items.Clear();
            if (int.TryParse(txtNo.Text, out intNumero) && double.TryParse(txtSalaire.Text, out dbNumero))
            {
                personneAjouter = new Personne(txtNom.Text, txtPrenom.Text, Int32.Parse(txtNo.Text), txtDepartement.Text, double.Parse(txtSalaire.Text));
                ListPersonne.Add(personneAjouter);
            }
            else
            {
                lstResultat.Items.Add("Salaire ou Numero vide");
            }
           

            lstOrg.Items.Clear();
            var valeur = (from p in ListPersonne select p);
            lstOrg.Items.AddRange(valeur.ToArray());
        }
    }
}


class partieDeuxException : Exception
{
    public partieDeuxException(string message)
        : base(message)
    {

    }
}
