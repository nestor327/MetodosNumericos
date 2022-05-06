using MetodosNumericos.Calculos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosNumericos.Complementos
{
    public partial class Grafica : Form
    {
        public string funcionCompleta { get; set; }
        public string funcion1;
        public string funcion2;
        public double limiInf = 0;
        public double limiSup = 0;
        public List<string> funcion;
        private StringToFormula formula = new StringToFormula();
        public Grafica()
        {
            InitializeComponent();
        }

        private void Grafica_Load(object sender, EventArgs e)
        {
            Dividirfuncion(funcion);
            label1.Text = "Soluciones de la funcion: " + funcionCompleta;
            label2.Text = "La serie 1 es: " + funcion1 + " y la serie 2 es: " + funcion2;
        }
        public void Dividirfuncion(List<string> stokens)
        {
            funcion1 = "";
            funcion2 = "";
            int contParentesis = 0;
            int conVariable = 0;
            int incF2 = 0;
            for (int j = 0; j <= ((funcion.Count - 1)); j++)
            {
                if (funcion.ElementAt(j).ToCharArray()[0]=='(')
                {
                    contParentesis++;
                }
                else if (funcion.ElementAt(j).ToCharArray()[0]==')')
                {
                    contParentesis--;
                } else if (funcion.ElementAt(j).ToCharArray()[0]=='x')
                {
                    conVariable++;
                }
                
                if (conVariable>0 && contParentesis==0 && (funcion.ElementAt(j).ToCharArray()[0]=='+' || funcion.ElementAt(j).ToCharArray()[0]==('-')))
                {
                    incF2 = j;
                    j = funcion.Count;
                }
                else
                {
                    funcion1 = funcion1 + funcion.ElementAt(j);
                }
            }

            for (int k=incF2;k<funcion.Count;k++)
            {
                funcion2 = funcion2 + funcion.ElementAt(k);
            }
            for (double i = limiInf; i <= limiSup; i++)
            {
                chart1.Series[0].Points.AddXY(i, formula.ValorEsperado(funcion1.Replace("x", i.ToString())));
                chart1.Series[1].Points.AddXY(i, Convert.ToDouble(formula.ValorEsperado(funcion2.Replace("x", i.ToString())))*(-1));
            }

        }
    }
}
