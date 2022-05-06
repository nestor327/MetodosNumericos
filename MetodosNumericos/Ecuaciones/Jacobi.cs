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
using MetodosNumericos.Unidad_II;

namespace MetodosNumericos.Ecuaciones
{
    public partial class Jacobi : Form
    {        
        public Jacobi()
        {
            InitializeComponent();
        }
        public string sistema;
        private List<Label> listaLabel = new List<Label>();
        public int filas;
        public List<double> matrizReal = new List<double>();
        public List<double> iniciales;
        public double error;
        DataTable jacobi = new DataTable();
        private void Jacobi_Load(object sender, EventArgs e)
        {
            label3.Text = sistema;
            Label titulo;
            titulo = new Label();
            titulo.Location = new Point(label3.Location.X + label3.Width * 413 / 408 + 25, label3.Location.Y);
            titulo.Text = "Verificacion de que la matriz sea dominante";
            titulo.Font = new Font(label3.Font.Name, label3.Font.Size);
            titulo.Size = new Size(titulo.PreferredWidth, titulo.PreferredHeight);
            titulo.Parent = this;


            Label valorDiago;
            valorDiago = new Label();
            valorDiago.Location = new Point(titulo.Location.X, titulo.Location.Y + titulo.Height * 413 / 408 + 25);
            string validacion = "";
            double resto = 0;
            for (int i = 0; i < filas; i++)
            {
                for (int y = 0; y < filas; y++)
                {
                    resto = resto + Math.Abs(matrizReal.ElementAt(i * (filas + 1) + y));
                }
                resto = resto - Math.Abs(matrizReal.ElementAt((filas + 1) * i + i));
                validacion = validacion + "Valor absoluto del elemento x" + subIndices(i + 1) + " en la fila " + (i + 1) + " =       "
                    + Math.Abs(matrizReal.ElementAt((filas + 1) * i + i)).ToString() + " > " + resto + "       = " + "Suma del valor absoluto del resto de los elementos de la fila " + (i + 1) + "\n";
            }
            valorDiago.Text = validacion;
            valorDiago.Font = new Font(label3.Font.Name, label3.Font.Size);
            valorDiago.Size = new Size(valorDiago.PreferredWidth, valorDiago.PreferredHeight);
            valorDiago.Parent = this;

            Label sumaDeLaFila;
            sumaDeLaFila = new Label(); sumaDeLaFila.Text = "LA MATRIZ ES DIAGONALMENTE DOMINANTE";
            sumaDeLaFila.Font = new Font(label3.Font.Name, label3.Font.Size);
            sumaDeLaFila.Size = new Size(sumaDeLaFila.PreferredWidth, sumaDeLaFila.PreferredHeight);
            sumaDeLaFila.Location = new Point(valorDiago.Location.X + valorDiago.Width * 413 / 816 - sumaDeLaFila.Width * 413 / 816, valorDiago.Location.Y + valorDiago.Height * 413 / 408 + 25);
            sumaDeLaFila.Parent = this;

            label4.Location = new Point(label3.Location.X, sumaDeLaFila.Location.Y);
            label5.Location = new Point(label4.Location.X, label4.Location.Y + label4.Height * 413 / 408 + 20);
            string ecuaciones = "";
            string sustituciones = "";
            for (int i = 0; i < filas; i++)
            {
                ecuaciones = ecuaciones + "x" + subIndices(i + 1) + " = " + "b" + subIndices(i + 1) + "/a" + subIndices(i + 1) + "\x00b8" + subIndices(i + 1);
                for (int y = 0; y < filas; y++)
                {
                    if (y != i)
                    {
                        ecuaciones = ecuaciones + " -" + "(a" + subIndices(i + 1) + "\x00b8" + subIndices(y + 1) + "/a" + subIndices(i + 1) + "\x00b8" + subIndices(i + 1) + ")*x" + subIndices(y + 1);
                    }
                }
                sustituciones = " = " + matrizReal.ElementAt((i + 1) * (filas + 1) - 1).ToString() + "/" + matrizReal.ElementAt(i * (filas + 1) + i).ToString();
                for (int y = 0; y < filas; y++)
                {
                    if (y != i)
                    {
                        sustituciones = sustituciones + " - (" + matrizReal.ElementAt(i * (filas + 1) + y).ToString() + "/" + matrizReal.ElementAt(i * (filas + 1) + i).ToString() + ")*x" + subIndices(y + 1);
                    }
                }
                ecuaciones = ecuaciones + sustituciones + "\n";
                sustituciones = "";
            }
            label5.Text = ecuaciones;

            dataGridView1.Location = new Point(0, label5.Location.Y + label5.Height * 413 / 408 + 15);
            
            DataColumn columna;
            columna = new DataColumn();
            columna.ColumnName = "IT";
            jacobi.Columns.Add(columna);
            for (int i = 0; i < filas; i++)
            {
                columna = new DataColumn();
                columna.ColumnName = "X" + subIndices(i + 1);
                jacobi.Columns.Add(columna);
            }
            for (int i = 0; i < filas; i++)
            {
                columna = new DataColumn();
                columna.ColumnName = "ErrorX" + subIndices(i + 1);
                jacobi.Columns.Add(columna);
            }
            columna = new DataColumn();
            columna.ColumnName = "C.Parada";
            jacobi.Columns.Add(columna);
            Presicion2(13);
            label6.Location = new Point(label5.Location.X + label5.Width * 413 / 408, label5.Location.Y);
            comboBox1.Location = new Point(label6.Location.X,label6.Location.Y+label6.Height*413/408+10);
        }

        public string subIndices(int caso)
        {
            string valor = "";
            switch (caso)
            {
                case 1:
                    valor = "\x2081";
                    break;
                case 2:
                    valor = "\x2082";
                    break;
                case 3:
                    valor = "\x2083";
                    break;
                case 4:
                    valor = "\x2084";
                    break;
                case 5:
                    valor = "\x2085";
                    break;
                case 6:
                    valor = "\x2086";
                    break;
                case 7:
                    valor = "\x2087";
                    break;
                case 8:
                    valor = "\x2088";
                    break;
                case 9:
                    valor = "\x2089";
                    break;
                case 0:
                    valor = "\x2080";
                    break;
                default:
                    MessageBox.Show("No fuerce al programa, ingrese una matriz de menos de 10 filas");
                    valor = "n";
                    break;
            }
            return valor;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presicion2(Convert.ToInt32(comboBox1.Text));
        }

        public void Presicion2(int size)
        {
            dataGridView1.DataSource = null;
            jacobi.Clear();
            SJacobi solucion = new SJacobi();
            List<double> datos = solucion.MetodoDeJacobi(matrizReal, filas, error, iniciales);
            DataRow row;
            for (int i = 0; i < datos.Count; i = i + (2 * filas + 2))
            {
                row = jacobi.NewRow();
                for (int y = 0; y < 2 * filas + 1; y++)
                {
                    row[y] = Math.Round(datos.ElementAt(i + y),size).ToString();
                }
                if (datos.ElementAt(i + 2 * filas + 1) == 0)
                {
                    row[2 * filas + 1] = "Falso";
                }
                else
                {
                    row[2 * filas + 1] = "Verdadero";
                }
                jacobi.Rows.Add(row);
            }
            dataGridView1.DataSource = jacobi;
        }
    }
}
