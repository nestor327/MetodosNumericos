
using MetodosNumericos.Complementos;
using MetodosNumericos.Matrices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic.Core.Parser;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosNumericos
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

        private void calculoDeRaisesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculo_De_Raices calculo = new Calculo_De_Raices();
            calculo.isRaiz = true;
            calculo.Show();
        }

        private void graficarFuncionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculo_De_Raices calculo = new Calculo_De_Raices();
            calculo.isRaiz = false;
            calculo.Show();
        }

        private void raicesPorAproximacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrizGaussJordan matriz = new MatrizGaussJordan();
            matriz.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calculo_De_Raices calculo = new Calculo_De_Raices();
            calculo.isRaiz = true;
            calculo.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MatrizGaussJordan matriz = new MatrizGaussJordan();
            matriz.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Calculo_De_Raices calculo = new Calculo_De_Raices();
            calculo.isRaiz = false;
            calculo.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}