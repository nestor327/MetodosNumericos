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

namespace MetodosNumericos.Aproximacion_Raises
{
    public partial class VistaSecante : Form
    {
        public List<double> table = new List<double>();
        public string Funcion = "";
        public double Error = 0;
        public double a = 0;
        public double b = 0;
        public string codigo { get; set; }
        StringToFormula formula = new StringToFormula();
        public VistaSecante()
        {
            InitializeComponent();
        }

        private void VistaSecante_Load(object sender, EventArgs e)
        {
            label2.Text = "F(x)= " + Funcion;
            label4.Text = "Error = " + Error.ToString();
            label5.Text = "Intervalos        x0 = " + a + "          x1 = " + b;
            presicion(13);
        }
        public void presicion(int decimales)
        {          
            dataSet1.Tables[0].Clear();
            for (int i = 0; i < table.Count; i = i + 5)
            {
                DataRow row = dataSet1.Tables[0].NewRow();
                row[0] = Math.Round(table.ElementAt(i), decimales);
                row[1] = Math.Round(table.ElementAt(i + 1), decimales);
                row[2] = Math.Round(table.ElementAt(i + 2), decimales);
                row[3] = Math.Round(table.ElementAt(i + 3), decimales);
                if (table.ElementAt(i + 4) == 0)
                {
                    row[4] = "FALSO";
                }
                else
                {
                    row[4] = "VERDADERO";
                }
                dataSet1.Tables[0].Rows.Add(row);

                //(x-1)(x+2)=x^2-x+2x-2
            }
            dataGridView1.DataSource = dataSet1.Tables[0];
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            presicion(int.Parse(comboBox1.Text));
        }
    }
}
