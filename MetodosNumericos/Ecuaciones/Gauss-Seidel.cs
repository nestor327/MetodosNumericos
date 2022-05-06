using MetodosNumericos.Unidad_II;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosNumericos.Ecuaciones
{
    public partial class Gauss_Seidel : Form
    {
        public Gauss_Seidel()
        {
            InitializeComponent();
        }
        public List<double> iniciales;
        public string sistema;
        public int filas;
        public List<double> matrizReal;
        public double error;
        DataTable seidel = new DataTable();
        private void Gauss_Seidel_Load(object sender, EventArgs e)
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
            seidel.Columns.Add(columna);
            for (int i = 0; i < filas; i++)
            {
                columna = new DataColumn();
                columna.ColumnName = "X" + subIndices(i + 1);
                seidel.Columns.Add(columna);
                columna = new DataColumn();
                columna.ColumnName = "ErrorX" + subIndices(i + 1);
                seidel.Columns.Add(columna);
            }
            for (int i = 0; i < filas; i++)
            {
                columna = new DataColumn();
                columna.ColumnName = "C.Parada X" + subIndices(i + 1);
                seidel.Columns.Add(columna);
            }
            Presicion(13);
            SSeidel solucion = new SSeidel();
            List<double> datos = solucion.MetodoDeSeidel(matrizReal, filas, error, iniciales);
            label7.Location = new Point(dataGridView1.Location.X + dataGridView1.Width * 413 / 408 + 15, dataGridView1.Location.Y);
            label6.Location = new Point(label7.Location.X, label7.Location.Y + label7.Height + 413 / 408 + 10);
            string soluciones = "";
            for (int i = 0; i < filas; i++)
            {
                soluciones = soluciones + "X" + subIndices(i + 1) + " = " + Math.Round(datos.ElementAt(datos.Count - 3 * filas + 2 * i), 7).ToString() + "\n";
            }
            label6.Text = soluciones;
            label9.Location = new Point(label6.Location.X, label6.Location.Y + label6.Height * 413 / 408 + 10);
            label8.Location = new Point(label9.Location.X, label9.Location.Y + label9.Height * 413 / 408 + 10);
            
            string comprovacion = "";
            double valor = 0;
            for (int j = 0; j < filas; j++)
            {
                comprovacion = comprovacion + "b" + subIndices(j + 1) + " = (" + Math.Round(matrizReal.ElementAt(j * (filas + 1)), 6).ToString() + ")*(" + Math.Round(datos.ElementAt(datos.Count - 3 * filas), 6).ToString() + ")";
                valor = valor + matrizReal.ElementAt(j * (filas + 1)) * datos.ElementAt(datos.Count - 3 * filas);
                for (int i = 1; i < filas; i++)
                {
                    comprovacion = comprovacion + " + (" + Math.Round(matrizReal.ElementAt(j * (filas + 1) + i), 6).ToString() + ")*(" + Math.Round(datos.ElementAt(datos.Count - 3 * filas + 2 * i), 6).ToString() + ")";
                    valor = valor + matrizReal.ElementAt(j * (filas + 1) + i) * datos.ElementAt(datos.Count - 3 * filas + 2 * i);
                }
                comprovacion = comprovacion + " = " + Math.Round(valor, 6).ToString() + "\n";
                valor = 0;
            }
            label8.Text = comprovacion;
            label7.Location = new Point(label5.Location.X + label5.Width * 413 / 408 + 15, label5.Location.Y);
            label6.Location = new Point(label7.Location.X + label7.Width * 413 / 408 + 10, label7.Location.Y);
            label9.Location = new Point(label6.Location.X + label6.Width * 413 / 408 + 10, label7.Location.Y);
            label8.Location = new Point(label9.Location.X + label9.Width * 413 / 408 + 10, label7.Location.Y);
            comboBox1.Location = new Point(label4.Location.X+label4.Width*413/408+10,label4.Location.Y);
            label10.Location = new Point(comboBox1.Location.X,comboBox1.Location.Y-comboBox1.Height*413/408);
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
            Presicion(Convert.ToInt32(comboBox1.Text));
        }

        public void Presicion(int size)
        {
            seidel.Clear();
            SSeidel solucion = new SSeidel();
            List<double> datos = solucion.MetodoDeSeidel(matrizReal, filas, error, iniciales);
            DataRow row;
            for (int i = 0; i < datos.Count; i = i + (3 * filas + 1))
            {
                row = seidel.NewRow();
                for (int y = 0; y < 2 * filas + 1; y++)
                {
                    row[y] = Math.Round(datos.ElementAt(i + y),size).ToString();
                }
                for (int k = 0; k < filas; k++)
                {
                    if (datos.ElementAt(i + 2 * filas + 1 + k) == 0)
                    {
                        row[2 * filas + 1 + k] = "Continuar";
                    }
                    else if (datos.ElementAt(i + 2 * filas + 1 + k) == 1)
                    {
                        row[2 * filas + 1 + k] = "Detener";
                    }
                }
                seidel.Rows.Add(row);
            }

            dataGridView1.DataSource = seidel;
        }
    }
}
