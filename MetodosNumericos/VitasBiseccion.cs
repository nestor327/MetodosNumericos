using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MetodosNumericos.Calculos;

namespace MetodosNumericos
{
    public partial class VitasBiseccion : Form
    {
        public List<double> table = new List<double>();
        public string Funcion = "";
        public double Error = 0;
        public double a = 0;
        public double b = 0;
        public string codigo;
        StringToFormula formula = new StringToFormula();
        public VitasBiseccion()
        {
            InitializeComponent();
        }

        private void VitasTablas_Load(object sender, EventArgs e)
        {
            label2.Text = "F(x)= "+Funcion;
            label4.Text = "Error = " + Error.ToString();
            label5.Text = "Intervalos        a = " + a + "          b = " + b;
            label6.Text= "f(a)= " + Math.Round(double.Parse(formula.ValorEsperado(codigo.Replace("x", a.ToString()))), 7) + "      f(b)= " + Math.Round(double.Parse(formula.ValorEsperado(codigo.Replace("x",b.ToString()))),7);
            label7.Text = "f(a) * f(b) =" + (Math.Round(double.Parse(formula.ValorEsperado(codigo.Replace("x", a.ToString()))) 
                * double.Parse(formula.ValorEsperado(codigo.Replace("x", b.ToString()))),7)).ToString() + " < 0 Cumple";
            presicion(13);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            presicion(int.Parse(comboBox1.Text));
        }
        public void presicion(int decimales)
        {
            dataSet1.Tables[0].Clear();
            for (int i = 0; i < table.Count; i = i + 9)
            {
                DataRow row = dataSet1.Tables[0].NewRow();
                row[0] = Math.Round(table.ElementAt(i), decimales);
                row[1] = Math.Round(table.ElementAt(i + 1), decimales);
                row[2] = Math.Round(table.ElementAt(i + 2), decimales);
                row[3] = Math.Round(table.ElementAt(i + 3), decimales);
                row[4] = Math.Round(table.ElementAt(i + 4), decimales);
                row[5] = Math.Round(table.ElementAt(i + 5), decimales);
                row[6] = Math.Round(table.ElementAt(i + 6), decimales);
                row[7] = Math.Round(table.ElementAt(i + 7), decimales);
                if (table.ElementAt(i + 8) == 0)
                {
                    row[8] = "FALSO";
                }
                else
                {
                    row[8] = "VERDADERO";
                }
                dataSet1.Tables[0].Rows.Add(row);
                //(x-1)(x+2)=x^2-x+2x-2
            }
            dataGridView1.DataSource = dataSet1.Tables[0];
        }
    }
}
