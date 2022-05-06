using MetodosNumericos.Ecuaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosNumericos.Matrices
{
    public partial class MatrizGaussJordan : Form
    {
        public string funcion;
        private int FuncionLenght;
        private int orden;
        private int fila = 1;
        private List<double> matriz = new List<double>();
        public int columna = 1;
        private string sisteEcua = "";
        public string matrizA = "";
        public string vectorb = "";
        private string matrizTranpuesta = "";
        private bool verificacion = false;
        public double errorJacobi = 0;
        private int contadorValoresIniciales = 1;
        List<string> lista = new List<string>();
        List<double> iniciales = new List<double>();
        public MatrizGaussJordan()
        {
            InitializeComponent();
        }
        public void Concatenar(string cadena)
        {
            funcion = funcion + cadena;
            richTextBox1.Text = funcion;
            FuncionLenght = funcion.Length;
            richTextBox1.Focus();
            richTextBox1.SelectionStart = richTextBox1.TextLength;

        }
        public void ConcatenarIniciales(string cadena)
        {
            funcion = funcion + cadena;
            richTextBox2.Text = funcion;
            FuncionLenght = funcion.Length;
            richTextBox2.Focus();
            richTextBox2.SelectionStart = richTextBox2.TextLength;
        }
        public void ConcatenarError(string cadena)
        {
            funcion = funcion + cadena;
            richTextBox3.Text = funcion;
            FuncionLenght = funcion.Length;
            richTextBox3.Focus();
            richTextBox3.SelectionStart = richTextBox3.TextLength;
        }
        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string codigointer;
            codigointer = e.KeyCode.ToString();
            int letra = e.KeyValue;
            if (letra >= 47 & letra <= 57 || letra == 190 || letra == 189)
            {
                switch (letra)
                {
                    case 48:
                        Concatenar("0");
                        break;
                    case 49:
                        Concatenar("1");
                        break;
                    case 50:
                        Concatenar("2");
                        break;
                    case 51:
                        Concatenar("3");
                        break;
                    case 52:
                        Concatenar("4");
                        break;
                    case 53:
                        Concatenar("5");
                        break;
                    case 54:
                        Concatenar("6");
                        break;
                    case 55:
                        Concatenar("7");
                        break;
                    case 56:
                        Concatenar("8");
                        break;
                    case 57:
                        Concatenar("9");
                        break;
                    case 189:
                        Concatenar("-");
                        break;
                    case 190:
                        Concatenar(".");
                        break;
                }
            }
            else
            {
                if (!codigointer.Equals("Back"))
                {
                    if (richTextBox1.Text.Length > FuncionLenght)
                    {
                        richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.Text.Count() - 1);
                    }
                }
                else
                {
                    if (richTextBox1.Text.Count() >= 1)
                    {
                        funcion = funcion.Substring(0, funcion.Length - 1);
                    }
                    else
                    {
                        funcion = "";
                    }
                }
            }
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            FuncionLenght = richTextBox1.Text.Length;
        }
        private void MatrizGaussJordan_Load(object sender, EventArgs e)
        {
            richTextBox1.Enabled = true;
            button2.Enabled = false;
            label10.Visible = false;
            richTextBox2.Visible = false;
            button4.Visible = false;
            label11.Visible = false;
            button5.Visible = false;
            richTextBox3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            label6.Text = "1,1";
            button2.Text = "Elemnto a\x2081\x00b8\x2081";
            orden = (Convert.ToInt32(maskedTextBox1.Text)) * (Convert.ToInt32(maskedTextBox1.Text));
            if (maskedTextBox1.Text.Length > 0)
            {
                button2.Enabled = true;
                richTextBox1.Enabled = true;
                maskedTextBox1.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Debe ingresar la cantidad de ecuaciones del sistema");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("La columna actualmente es: "+columna);

            if (fila == Convert.ToInt32(maskedTextBox1.Text) && columna == Convert.ToInt32(maskedTextBox1.Text))
            {
                button1.Enabled = false;
                matriz.Add(double.Parse(richTextBox1.Text));
                if (columna == Convert.ToInt32(maskedTextBox1.Text))
                {
                    label5.Text = "b";
                    label6.Text = fila.ToString();
                    if (fila != Convert.ToInt32(maskedTextBox1.Text))
                    {
                        columna = 0;
                    }
                    fila++;
                }
                else
                {
                    label5.Text = "a";
                    label6.Text = ((fila).ToString() + "," + (columna + 1).ToString());
                    columna++;
                }
                funcion = "";
                richTextBox1.Text = "";
                richTextBox1.Focus();
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                columna++;
            } else if (columna == Convert.ToInt32(maskedTextBox1.Text) + 1)
            {
                matriz.Add(double.Parse(richTextBox1.Text));
                label5.Text = "a";
                label6.Text = "m,n";
                button2.Text = "Elemento a\x2098\x00b8\x2099";
                richTextBox1.Text = "";
                int mayorCantidad = 0;
                double elemento = 0;
                for (int i = 0; i < matriz.Count; i++)
                {
                    elemento = Math.Abs(matriz.ElementAt(i));
                    if (elemento.ToString().Length > mayorCantidad)
                    {
                        mayorCantidad = elemento.ToString().Length;
                    }
                }
                mayorCantidad = mayorCantidad + 4;
                for (int y = 0; y < matriz.Count; y++)
                {
                    string valorSecond = Math.Abs(matriz.ElementAt(y)).ToString();
                    for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                    {
                        if (y % (columna) != 0)
                        {
                            valorSecond = " " + valorSecond;
                        }
                    }

                    if (y % columna == 0)
                    {
                        if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                        {
                            valorSecond = "-" + valorSecond;
                            for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 2; t++)
                            {
                                valorSecond = " " + valorSecond;
                            }
                        }
                        else
                        {
                            for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                            {
                                valorSecond = " " + valorSecond;
                            }
                        }
                    }

                    if (y % columna != 0 && y % columna != (columna - 1))
                    {
                        if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                        {
                            valorSecond = "-" + valorSecond;
                            for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                            {
                                valorSecond = " " + valorSecond;
                            }
                        }
                        else
                        {
                            valorSecond = "+" + valorSecond;
                            for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                            {
                                valorSecond = " " + valorSecond;
                            }
                        }
                    } else if (y % columna == (columna - 1))
                    {
                        if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                        {
                            valorSecond = valorSecond.Replace(" ", "");
                            valorSecond = "-" + valorSecond;
                            valorSecond = "=" + valorSecond;
                            for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                            {
                                valorSecond = " " + valorSecond;
                            }
                        }
                        else
                        {
                            valorSecond = "=" + valorSecond;
                            for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                            {
                                valorSecond = " " + valorSecond;
                            }
                        }
                    }
                    lista.Add(valorSecond);
                }
                sisteEcua = "";
                for (int i = 0; i < columna - 1; i++)
                {
                    for (int j = 0; j < columna - 1; j++)
                    {
                        sisteEcua = sisteEcua + lista.ElementAt(i * columna + j) + "x" + subIndices(j + 1);
                        matrizA = matrizA + lista.ElementAt(i * columna + j);
                        matrizTranpuesta = matrizTranpuesta + lista.ElementAt(i * columna + j).Replace("+", " ").Replace("-", " ").Replace(Math.Abs(matriz.ElementAt(i * columna + j)).ToString(),
                            (matriz.ElementAt(i * columna + j)).ToString());
                    }
                    matrizA = matrizA + "\n";
                    vectorb = vectorb + lista.ElementAt((i + 1) * (columna) - 1) + "\n";
                    sisteEcua = sisteEcua + lista.ElementAt((i + 1) * (columna) - 1) + "\n";
                    matrizTranpuesta = matrizTranpuesta + "    :" + lista.ElementAt((i + 1) * columna - 1).Replace("+", " ").Replace("-", " ").Replace("=", " ").Replace(Math.Abs(matriz.ElementAt((i + 1) * (columna) - 1)).ToString(),
                        (matriz.ElementAt((i + 1) * (columna) - 1)).ToString()) + "\n";
                }
                label8.Text = sisteEcua;
                funcion = "";
                button2.Enabled = false;
                richTextBox1.Enabled = false;
            }
            else
            {
                button1.Enabled = false;
                matriz.Add(double.Parse(richTextBox1.Text));
                if (columna == Convert.ToInt32(maskedTextBox1.Text))
                {
                    label5.Text = "b";
                    label6.Text = fila.ToString();
                    button2.Text = "Elemento b" + subIndices(fila);
                    columna = 0;
                    fila++;
                }
                else
                {
                    label5.Text = "a";
                    label6.Text = ((fila).ToString() + "," + (columna + 1).ToString());
                    button2.Text = "Elemento a" + subIndices(fila) + "\x00b8" + subIndices(columna + 1);
                    columna++;
                }
                funcion = "";
                richTextBox1.Text = "";
                richTextBox1.Focus();
                richTextBox1.SelectionStart = richTextBox1.TextLength;
            }
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

        private void button3_Click(object sender, EventArgs e)
        {


            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    SolucionMatrizGaussJordan gaussJordan = new SolucionMatrizGaussJordan();
                    gaussJordan.sistema = sisteEcua;
                    gaussJordan.matriz = matrizA.Replace("x", " ").Replace("+", " ");
                    gaussJordan.vector = vectorb.Replace("=", " ");
                    string vectorIncognitas = "";
                    for (int i = 0; i < fila - 1; i++)
                    {
                        vectorIncognitas = vectorIncognitas + "x" + subIndices(i + 1) + "\n";
                    }
                    gaussJordan.filas = fila - 1;
                    gaussJordan.matrizTranspueta = matrizTranpuesta;
                    gaussJordan.vectorIncognitas = vectorIncognitas;
                    gaussJordan.matrizTotal = matriz;
                    gaussJordan.Show();
                    break;
                case 1:
                    if (iniciales.Count != fila - 1)
                    {
                        MessageBox.Show("Introdusca los valores iniciales");
                    }
                    if (errorJacobi == 0)
                    {
                        MessageBox.Show("Introdusca el error");
                    }

                    double suma = 0;
                    Jacobi jacobi = new Jacobi();
                    List<double> matrizDiagonal = new List<double>();
                    for (int i = 0; i < fila - 2; i++)
                    {
                        verificacion = false;
                        for (int y = 0; y < fila - 1; y++)
                        {
                            suma = 0;
                            for (int t = 0; t < fila - 1; t++)
                            {
                                suma = suma + Math.Abs(matriz.ElementAt(y * (fila) + t));
                            }
                            suma = suma - Math.Abs(matriz.ElementAt(y * (fila) + i));
                            if (Math.Abs(matriz.ElementAt(y * (fila) + i)) > suma)
                            {
                                for (int k = 0; k < i; k++)
                                {
                                    for (int d = 0; d < fila; d++)
                                    {
                                        matrizDiagonal.Add(matriz.ElementAt(k * (fila) + d));
                                    }
                                }

                                for (int s = y; s < fila - 1; s++)
                                {
                                    for (int d = 0; d < fila; d++)
                                    {
                                        matrizDiagonal.Add(matriz.ElementAt(s * (fila) + d));
                                    }
                                }

                                for (int s = i; s < y; s++)
                                {
                                    for (int d = 0; d < fila; d++)
                                    {
                                        matrizDiagonal.Add(matriz.ElementAt(s * (fila) + d));
                                    }
                                }

                                matriz.Clear();
                                for (int s = 0; s < fila - 1; s++)
                                {
                                    for (int d = 0; d < fila; d++)
                                    {
                                        matriz.Add(matrizDiagonal.ElementAt(s * (fila) + d));
                                    }
                                }
                                matrizDiagonal.Clear();
                                y = fila;
                                suma = 0;
                                verificacion = true;
                            }
                        }

                        if (verificacion == false)
                        {//TODO revisa esta parte de aqui cabrom
                            MessageBox.Show("La matriz no es diagonalmente dominante, vuelva a ingresar los datos");
                            i = fila - 1;
                        }
                    }

                    if (verificacion == false)
                    {
                        matriz.Clear();
                        button2.Enabled = true;
                        richTextBox1.Enabled = true;
                        funcion = "";
                        columna = 1;
                        lista.Clear();
                        fila = 1;
                        break;
                    }
                    lista.Clear();
                    int mayorCantidad = 0;
                    double elemento = 0;
                    for (int i = 0; i < matriz.Count; i++)
                    {
                        elemento = Math.Abs(matriz.ElementAt(i));
                        if (elemento.ToString().Length > mayorCantidad)
                        {
                            mayorCantidad = elemento.ToString().Length;
                        }
                    }
                    mayorCantidad = mayorCantidad + 4;


                    for (int y = 0; y < matriz.Count; y++)
                    {
                        string valorSecond = Math.Abs(matriz.ElementAt(y)).ToString();
                        for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                        {
                            if (y % (columna) != 0)
                            {
                                valorSecond = " " + valorSecond;
                            }
                        }

                        if (y % columna == 0)
                        {
                            if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                            {
                                valorSecond = "-" + valorSecond;
                                for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 2; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                            else
                            {
                                for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                        }

                        if (y % columna != 0 && y % columna != (columna - 1))
                        {
                            if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                            {
                                valorSecond = "-" + valorSecond;
                                for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                            else
                            {
                                valorSecond = "+" + valorSecond;
                                for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                        }
                        else if (y % columna == (columna - 1))
                        {
                            if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                            {
                                valorSecond = valorSecond.Replace(" ", "");
                                valorSecond = "-" + valorSecond;
                                valorSecond = "=" + valorSecond;
                                for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                            else
                            {
                                valorSecond = "=" + valorSecond;
                                for (int t = 0; t < (mayorCantidad - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                        }
                        lista.Add(valorSecond);
                    }
                    sisteEcua = "";
                    matrizTranpuesta = "";
                    matrizA = "";
                    vectorb = "";
                    for (int i = 0; i < fila - 1; i++)
                    {
                        for (int j = 0; j < fila - 1; j++)
                        {
                            sisteEcua = sisteEcua + lista.ElementAt(i * columna + j) + "x" + subIndices(j + 1);
                            matrizA = matrizA + lista.ElementAt(i * columna + j);
                            matrizTranpuesta = matrizTranpuesta + lista.ElementAt(i * columna + j).Replace("+", " ").Replace("-", " ").Replace(Math.Abs(matriz.ElementAt(i * columna + j)).ToString(),
                                (matriz.ElementAt(i * columna + j)).ToString());
                        }
                        matrizA = matrizA + "\n";
                        vectorb = vectorb + lista.ElementAt((i + 1) * (columna) - 1) + "\n";
                        sisteEcua = sisteEcua + lista.ElementAt((i + 1) * (columna) - 1) + "\n";
                        matrizTranpuesta = matrizTranpuesta + "    :" + lista.ElementAt((i + 1) * columna - 1).Replace("+", " ").Replace("-", " ").Replace("=", " ").Replace(Math.Abs(matriz.ElementAt((i + 1) * (columna) - 1)).ToString(),
                            (matriz.ElementAt((i + 1) * (columna) - 1)).ToString()) + "\n";
                    }
                    jacobi.iniciales = iniciales;
                    jacobi.sistema = sisteEcua;
                    jacobi.filas = fila - 1;
                    jacobi.matrizReal = matriz;
                    jacobi.error = errorJacobi;
                    jacobi.Show();
                    break;
                case 2:

                    if (iniciales.Count != fila - 1)
                    {
                        MessageBox.Show("Introdusca los valores iniciales");
                    }
                    if (errorJacobi == 0)
                    {
                        MessageBox.Show("Introdusca el error");
                    }

                    double suma2 = 0;
                    Gauss_Seidel gauss = new Gauss_Seidel();
                    List<double> matrizDiagonal2 = new List<double>();
                    for (int i = 0; i < fila - 2; i++)
                    {
                        verificacion = false;
                        for (int y = 0; y < fila - 1; y++)
                        {
                            suma2 = 0;
                            for (int t = 0; t < fila - 1; t++)
                            {
                                suma2 = suma2 + Math.Abs(matriz.ElementAt(y * (fila) + t));
                            }
                            suma2 = suma2 - Math.Abs(matriz.ElementAt(y * (fila) + i));
                            if (Math.Abs(matriz.ElementAt(y * (fila) + i)) > suma2)
                            {
                                for (int k = 0; k < i; k++)
                                {
                                    for (int d = 0; d < fila; d++)
                                    {
                                        matrizDiagonal2.Add(matriz.ElementAt(k * (fila) + d));
                                    }
                                }

                                for (int s = y; s < fila - 1; s++)
                                {
                                    for (int d = 0; d < fila; d++)
                                    {
                                        matrizDiagonal2.Add(matriz.ElementAt(s * (fila) + d));
                                    }
                                }

                                for (int s = i; s < y; s++)
                                {
                                    for (int d = 0; d < fila; d++)
                                    {
                                        matrizDiagonal2.Add(matriz.ElementAt(s * (fila) + d));
                                    }
                                }

                                matriz.Clear();
                                for (int s = 0; s < fila - 1; s++)
                                {
                                    for (int d = 0; d < fila; d++)
                                    {
                                        matriz.Add(matrizDiagonal2.ElementAt(s * (fila) + d));
                                    }
                                }
                                matrizDiagonal2.Clear();
                                y = fila;
                                suma2 = 0;
                                verificacion = true;
                            }
                        }

                        if (verificacion == false)
                        {//TODO revisa esta parte de aqui cabrom
                            MessageBox.Show("La matriz no es diagonalmente dominante, vuelva a ingresar los datos");
                            i = fila - 1;
                        }
                    }

                    if (verificacion == false)
                    {
                        matriz.Clear();
                        button2.Enabled = true;
                        richTextBox1.Enabled = true;
                        funcion = "";
                        columna = 1;
                        lista.Clear();
                        fila = 1;
                        break;
                    }
                    lista.Clear();
                    int mayorCantidad2 = 0;
                    double elemento2 = 0;
                    for (int i = 0; i < matriz.Count; i++)
                    {
                        elemento2 = Math.Abs(matriz.ElementAt(i));
                        if (elemento2.ToString().Length > mayorCantidad2)
                        {
                            mayorCantidad2 = elemento2.ToString().Length;
                        }
                    }
                    mayorCantidad2 = mayorCantidad2 + 4;


                    for (int y = 0; y < matriz.Count; y++)
                    {
                        string valorSecond = Math.Abs(matriz.ElementAt(y)).ToString();
                        for (int t = 0; t < (mayorCantidad2 - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                        {
                            if (y % (columna) != 0)
                            {
                                valorSecond = " " + valorSecond;
                            }
                        }

                        if (y % columna == 0)
                        {
                            if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                            {
                                valorSecond = "-" + valorSecond;
                                for (int t = 0; t < (mayorCantidad2 - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 2; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                            else
                            {
                                for (int t = 0; t < (mayorCantidad2 - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                        }

                        if (y % columna != 0 && y % columna != (columna - 1))
                        {
                            if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                            {
                                valorSecond = "-" + valorSecond;
                                for (int t = 0; t < (mayorCantidad2 - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                            else
                            {
                                valorSecond = "+" + valorSecond;
                                for (int t = 0; t < (mayorCantidad2 - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                        }
                        else if (y % columna == (columna - 1))
                        {
                            if (matriz.ElementAt(y).ToString().ToCharArray()[0] == '-')
                            {
                                valorSecond = valorSecond.Replace(" ", "");
                                valorSecond = "-" + valorSecond;
                                valorSecond = "=" + valorSecond;
                                for (int t = 0; t < (mayorCantidad2 - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                            else
                            {
                                valorSecond = "=" + valorSecond;
                                for (int t = 0; t < (mayorCantidad2 - (Math.Abs(matriz.ElementAt(y))).ToString().Length) - 1; t++)
                                {
                                    valorSecond = " " + valorSecond;
                                }
                            }
                        }
                        lista.Add(valorSecond);
                    }
                    sisteEcua = "";
                    matrizTranpuesta = "";
                    matrizA = "";
                    vectorb = "";
                    for (int i = 0; i < fila - 1; i++)
                    {
                        for (int j = 0; j < fila - 1; j++)
                        {
                            sisteEcua = sisteEcua + lista.ElementAt(i * columna + j) + "x" + subIndices(j + 1);
                            matrizA = matrizA + lista.ElementAt(i * columna + j);
                            matrizTranpuesta = matrizTranpuesta + lista.ElementAt(i * columna + j).Replace("+", " ").Replace("-", " ").Replace(Math.Abs(matriz.ElementAt(i * columna + j)).ToString(),
                                (matriz.ElementAt(i * columna + j)).ToString());
                        }
                        matrizA = matrizA + "\n";
                        vectorb = vectorb + lista.ElementAt((i + 1) * (columna) - 1) + "\n";
                        sisteEcua = sisteEcua + lista.ElementAt((i + 1) * (columna) - 1) + "\n";
                        matrizTranpuesta = matrizTranpuesta + "    :" + lista.ElementAt((i + 1) * columna - 1).Replace("+", " ").Replace("-", " ").Replace("=", " ").Replace(Math.Abs(matriz.ElementAt((i + 1) * (columna) - 1)).ToString(),
                            (matriz.ElementAt((i + 1) * (columna) - 1)).ToString()) + "\n";
                    }
                    gauss.iniciales = iniciales;
                    gauss.sistema = sisteEcua;
                    gauss.filas = fila - 1;
                    gauss.matrizReal = matriz;
                    gauss.error = errorJacobi;
                    gauss.Show();
                    break;
                default:
                    MessageBox.Show("Elija un metodo para encontrar las soluciones del sistema");
                    break;
            }


        }

        private void richTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string codigointer;
            codigointer = e.KeyCode.ToString();
            int letra = e.KeyValue;
            if (letra >= 47 & letra <= 57 || letra == 190 || letra == 189)
            {
                switch (letra)
                {
                    case 48:
                        ConcatenarIniciales("0");
                        break;
                    case 49:
                        ConcatenarIniciales("1");
                        break;
                    case 50:
                        ConcatenarIniciales("2");
                        break;
                    case 51:
                        ConcatenarIniciales("3");
                        break;
                    case 52:
                        ConcatenarIniciales("4");
                        break;
                    case 53:
                        ConcatenarIniciales("5");
                        break;
                    case 54:
                        ConcatenarIniciales("6");
                        break;
                    case 55:
                        ConcatenarIniciales("7");
                        break;
                    case 56:
                        ConcatenarIniciales("8");
                        break;
                    case 57:
                        ConcatenarIniciales("9");
                        break;
                    case 189:
                        ConcatenarIniciales("-");
                        break;
                    case 190:
                        ConcatenarIniciales(".");
                        break;
                }
            }
            else
            {
                if (!codigointer.Equals("Back"))
                {
                    if (richTextBox1.Text.Length > FuncionLenght)
                    {
                        richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.Text.Count() - 1);
                    }
                }
                else
                {
                    if (richTextBox1.Text.Count() >= 1)
                    {
                        funcion = funcion.Substring(0, funcion.Length - 1);
                    }
                    else
                    {
                        funcion = "";
                    }
                }
            }
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            FuncionLenght = richTextBox1.Text.Length;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 2)
            {
                label10.Visible = true;
                richTextBox2.Visible = true;
                button4.Visible = true;
                label11.Visible = true;
                button5.Visible = true;
                richTextBox3.Visible = true;
                label10.Text = "Valor inicial X" + subIndices(1);
            }
            else
            {
                label10.Visible = false;
                richTextBox2.Visible = false;
                button4.Visible = false;
                label11.Visible = false;
                button5.Visible = false;
                richTextBox3.Visible = false;
                errorJacobi = 0;
                iniciales.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            funcion = "";
            if (contadorValoresIniciales < fila)
            {
                label10.Text = "Valor inicial X" + subIndices(contadorValoresIniciales + 1);
                if (richTextBox2.Text.Length > 0)
                {
                    iniciales.Add(Convert.ToDouble(richTextBox2.Text));
                    contadorValoresIniciales++;
                    richTextBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("Introdusca el valor incial para X" + subIndices(contadorValoresIniciales + 1));
                }
                if (contadorValoresIniciales == fila)
                {
                    label10.Text = "Valor inicial X";
                    button4.Enabled = false;
                    richTextBox2.Enabled = false;
                }
            }

            funcion = "";
        }

        private void richTextBox3_KeyUp(object sender, KeyEventArgs e)
        {

            string codigointer;
            codigointer = e.KeyCode.ToString();
            int letra = e.KeyValue;
            if (letra >= 47 & letra <= 57 || letra == 190 || letra == 189)
            {
                switch (letra)
                {
                    case 48:
                        ConcatenarError("0");
                        break;
                    case 49:
                        ConcatenarError("1");
                        break;
                    case 50:
                        ConcatenarError("2");
                        break;
                    case 51:
                        ConcatenarError("3");
                        break;
                    case 52:
                        ConcatenarError("4");
                        break;
                    case 53:
                        ConcatenarError("5");
                        break;
                    case 54:
                        ConcatenarError("6");
                        break;
                    case 55:
                        ConcatenarError("7");
                        break;
                    case 56:
                        ConcatenarError("8");
                        break;
                    case 57:
                        ConcatenarError("9");
                        break;
                    case 189:
                        ConcatenarError("-");
                        break;
                    case 190:
                        ConcatenarError(".");
                        break;
                }
            }
            else
            {
                if (!codigointer.Equals("Back"))
                {
                    if (richTextBox3.Text.Length > FuncionLenght)
                    {
                        richTextBox3.Text = richTextBox3.Text.Substring(0, richTextBox3.Text.Count() - 1);
                    }
                }
                else
                {
                    if (richTextBox3.Text.Count() >= 1)
                    {
                        funcion = funcion.Substring(0, funcion.Length - 1);
                    }
                    else
                    {
                        funcion = "";
                    }
                }
            }
            richTextBox3.SelectionStart = richTextBox3.TextLength;
            FuncionLenght = richTextBox1.Text.Length;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Text.Length > 0)
            {
                errorJacobi = Convert.ToDouble(richTextBox3.Text);
                richTextBox3.Text = "";
                richTextBox3.Enabled = false;
                button5.Enabled = false;
            }
            else
            {
                MessageBox.Show("Introdusca el error");
            }
        } 
    }
}
