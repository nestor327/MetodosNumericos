using MetodosNumericos.Aproximacion_Raises;
using MetodosNumericos.Calculos;
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

namespace MetodosNumericos.Complementos
{
    public partial class Calculo_De_Raices : Form
    {
        public bool isRaiz = false;
        private VitasBiseccion vistasBisecccion = new VitasBiseccion();
        private VistaReglaFalsa vistaReglaFalsa = new VistaReglaFalsa();
        private VistaSecante vistaSecante = new VistaSecante();
        private VistaNewtonRapson vistaNewton = new VistaNewtonRapson();
        public Biseccion bisec = new Biseccion();
        public ReglaFalsa reglaFalsa = new ReglaFalsa();
        public Secante secante = new Secante();
        public Newton_Rapson newton = new Newton_Rapson();
        public StringToFormula form = new StringToFormula();
        private List<string> funcion= new List<string>();
        private List<string> codigo = new List<string>();
        private int FuncionLenght;
        int indice = 0;
        private bool ValorAbsoluto = false;
        private string limiteInferior="";
        private string limiteSuperior = "";
        private string tolerancia = "";        
        public Calculo_De_Raices()
        {
            InitializeComponent();
        }

        private void Calculo_De_Raices_Load(object sender, EventArgs e)
        {
            
            if (isRaiz==true)
            {
                label3.Visible = false;
                label4.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                button3.Visible = false;
            }
            else
            {
                label5.Visible = false;
                textBox3.Visible = false;
                button2.Visible = false;
                comboBox1.Visible = false;
            }
            StringToFormula toFormula = new StringToFormula();
            string resultado = "";
            string rest = " ";
            string valor = "y(-1)";
            
            /*while (valor.Length>4)
            {*/
            rest = toFormula.ValorEsperado(valor);
            //valor = rest;
            //  MessageBox.Show(valor);
            /*}  */
            
            comboBox1.Text = "Seleccione el Metodo";
        }

        private string funcionCodigo()
        {
            string codigoString = "";
            for (int i = 0; i < codigo.Count; i++)
            {
                codigoString = codigoString + codigo.ElementAt(i);
            }
            return codigoString;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice]=='x'
                || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("(", "*(");
            }
            else
            {
                Concatenar("(", "(");                
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Concatenar(")", ")");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Concatenar("^(", "^(");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("x", "*x");
            }
            else
            {
                Concatenar("x", "x");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("√(", "*r(");
            }
            else
            {
                Concatenar("√(", "r(");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice>=0 && ((char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')') && ValorAbsoluto == false))
            {
                Concatenar("|(", "*a(");
                ValorAbsoluto = !ValorAbsoluto;
            }
            else if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')') && ValorAbsoluto == true)
            {
                Concatenar(")|", ")");
                ValorAbsoluto = !ValorAbsoluto;
            }
            else if (ValorAbsoluto == false)
            {
                Concatenar("|(", "a(");
            }
            else if (ValorAbsoluto == true)
            {
                Concatenar(")|", ")");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("Cos(", "*c(");
            }
            else
            {
                Concatenar("Cos(", "c(");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("Sen(", "*s(");
            }
            else
            {
                Concatenar("Sen(", "s(");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("Tan(", "*t(");
            }
            else
            {
                Concatenar("Tan(", "t(");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("ArcCos(", "*v(");
            }
            else
            {
                Concatenar("ArcCos(", "v(");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("ArcSen(", "*d(");
            }
            else
            {
                Concatenar("ArcSen(", "d(");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
            {
                Concatenar("ArcTan(", "*y(");
            }
            else
            {
                Concatenar("ArcTan(", "y(");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string codigoString = funcionCodigo();
            indice = codigoString.ToCharArray().Length - 1;
            if (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')' && indice > 0)
            {
                Concatenar("Log(", "*l(");
            }
            else
            {
                Concatenar("Log(", "l(");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Concatenar("(", "*(");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Concatenar("/(", "/(");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Concatenar("+", "+");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Concatenar("-", "-");
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string codigointer;
            codigointer = e.KeyCode.ToString();
            int letra = e.KeyValue;


            
            if (letra > 47 & letra < 57 || letra == 187 || letra == 189 || letra == 190 || letra==88)
            {
                switch (letra)
                {
                    case 48:
                        Concatenar("0", "0");
                        break;
                    case 49:
                        Concatenar("1", "1");
                        break;
                    case 50:
                        Concatenar("2", "2");
                        break;
                    case 51:
                        Concatenar("3", "3");
                        break;
                    case 52:
                        Concatenar("4", "4");
                        break;
                    case 53:
                        Concatenar("5", "5");
                        break;
                    case 54:
                        Concatenar("6", "6");
                        break;
                    case 55:
                        Concatenar("7", "7");
                        break;
                    case 56:
                        Concatenar("8", "8");
                        break;
                    case 57:
                        Concatenar("9", "9");
                        break;
                    case 88:
                        string codigoString = funcionCodigo();
                        indice = codigoString.ToCharArray().Length - 1;
                        if (indice >= 0 && (char.IsDigit(codigoString.ToCharArray()[indice]) || codigoString.ToCharArray()[indice] == ')'))
                        {
                            Concatenar("x", "*x");
                        }
                        else
                        {
                            Concatenar("x", "x");
                        }
                        break;
                    case 187:
                        Concatenar("+", "+");
                        break;
                    case 189:
                        Concatenar("-", "-");
                        break;
                    case 190:
                        Concatenar(".", ".");
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

                }else if (codigointer.Equals("Back") && funcion.Count>0)
                {
                    funcion.RemoveAt(funcion.Count-1);
                    codigo.RemoveAt(codigo.Count-1);
                    richTextBox1.Text = stringFuncion();
                }
            }
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            FuncionLenght = richTextBox1.Text.Length;
        }
        public void Concatenar(string cadena, string codigo)
        {
            funcion.Add(cadena);
            this.codigo.Add(codigo);
            string valor = stringFuncion();
            richTextBox1.Text = valor;
            richTextBox1.Focus();
            richTextBox1.SelectionStart = richTextBox1.TextLength;
        }
        private string stringFuncion()
        {
            string valor = "";
            for (int i = 0; i < funcion.Count; i++)
            {
                valor = valor + funcion.ElementAt(i);
            }
            return valor;
        }
        private void button2_Click(object sender, EventArgs e)
        {            
            int indexTexbox = comboBox1.SelectedIndex;
            switch (indexTexbox)
            {
                case 0:
                    if (textBox1.Text.Length < 1)
                    {
                        MessageBox.Show("Introduzca el valor del limite Inferior");
                        break;
                    } else if (textBox2.Text.Length < 1)
                    {
                        MessageBox.Show("Introduzca el valor del limite Superior");
                        break;
                    }else if (textBox3.Text.Length<1)
                    {
                        MessageBox.Show("Introduzca el error o tolerancia de las raices");
                    }
                    string valor = stringFuncion();
                    vistasBisecccion.Funcion = valor;
                    string codigoff = funcionCodigo();
                    vistasBisecccion.table = bisec.MetodoDeBiseccion(form.getTokens(codigoff), Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text),Convert.ToDouble(textBox3.Text));
                    vistasBisecccion.Error = Convert.ToDouble(textBox3.Text);
                    vistasBisecccion.codigo = codigoff;
                    vistasBisecccion.a = Convert.ToDouble(textBox1.Text);
                    vistasBisecccion.b = Convert.ToDouble(textBox2.Text);
                    vistasBisecccion.Show();
                    break;
                case 1:
                    if (textBox1.Text.Length < 1)
                    {
                        MessageBox.Show("Introduzca el valor del limite Inferior");
                        break;
                    }
                    else if (textBox2.Text.Length < 1)
                    {
                        MessageBox.Show("Introduzca el valor del limite Superior");
                        break;
                    }
                    else if (textBox3.Text.Length < 1)
                    {
                        MessageBox.Show("Introduzca el error o tolerancia de las raices");
                    }
                    string codigof = funcionCodigo();
                    string valor2 = stringFuncion();
                    vistaReglaFalsa.Funcion = valor2;
                    vistaReglaFalsa.table = reglaFalsa.MetodoReglaFalsa(form.getTokens(codigof), Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text),Convert.ToDouble(textBox3.Text));
                    vistaReglaFalsa.Error = Convert.ToDouble(textBox3.Text);
                    vistaReglaFalsa.codigo = codigof;
                    vistaReglaFalsa.a = Convert.ToDouble("0.1");//Convert.ToDouble(textBox1.Text);
                    vistaReglaFalsa.b = Convert.ToDouble(textBox2.Text);
                    vistaReglaFalsa.Show();
                    break;
                case 2:
                    if (textBox3.Text.Length < 1)
                    {
                        MessageBox.Show("Introduzca el error o tolerancia de las raices");
                    }
                    string codigofff = funcionCodigo();
                    string valor3 = stringFuncion();
                    vistaSecante.Funcion = valor3;
                    vistaSecante.table = secante.MetodoSecante(form.getTokens(codigofff), 0.1, 1);
                    vistaSecante.Error = Convert.ToDouble(textBox3.Text);
                    vistaSecante.codigo = codigofff;
                    vistaSecante.a = 0.1;
                    vistaSecante.b = 1;
                    vistaSecante.Show();
                    break;
                case 3:
                    if (textBox3.Text.Length < 1)
                    {
                        MessageBox.Show("Introduzca el error o tolerancia de las raices");
                    }
                    string codigoffff = funcionCodigo();
                    string valor4 = stringFuncion();
                    vistaNewton.Funcion = valor4;
                    vistaNewton.table = newton.MetodoNewtonRapson(form.getTokens(codigoffff),-1);
                    vistaNewton.Error = Convert.ToDouble(textBox3.Text);
                    vistaNewton.codigo = codigoffff;
                    vistaNewton.a = 0.1;
                    vistaNewton.b = 1;
                    vistaNewton.Show();
                    break;
                default:
                    MessageBox.Show("Seleccione el metodo por el cual desea encontrar la aproximacion de la raiz");
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string codigoffff = funcionCodigo();
            Grafica grafica = new Grafica();
            grafica.funcion = form.getTokens(codigoffff);
            if (textBox1.Text.Length<1 || textBox2.Text.Length<1)
            {
                MessageBox.Show("Ingrese los limites o intervalos de la grafica");
                return;
            }
            grafica.limiInf = Convert.ToDouble(textBox1.Text);
            grafica.limiSup = Convert.ToDouble(textBox2.Text);
            grafica.funcionCompleta = richTextBox1.Text;
            grafica.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 1)
            {
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox1.Text = "0.01";
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

            string codigointer;
            codigointer = e.KeyCode.ToString();
            int letra = e.KeyValue;
            if (letra >= 47 & letra <= 57 || letra == 190 || letra == 189)
            {
                switch (letra)
                {
                    case 48:
                        ConcatenarLimiteInferior("0");
                        break;
                    case 49:
                        ConcatenarLimiteInferior("1");
                        break;
                    case 50:
                        ConcatenarLimiteInferior("2");
                        break;
                    case 51:
                        ConcatenarLimiteInferior("3");
                        break;
                    case 52:
                        ConcatenarLimiteInferior("4");
                        break;
                    case 53:
                        ConcatenarLimiteInferior("5");
                        break;
                    case 54:
                        ConcatenarLimiteInferior("6");
                        break;
                    case 55:
                        ConcatenarLimiteInferior("7");
                        break;
                    case 56:
                        ConcatenarLimiteInferior("8");
                        break;
                    case 57:
                        ConcatenarLimiteInferior("9");
                        break;
                    case 189:
                        ConcatenarLimiteInferior("-");
                        break;
                    case 190:
                        ConcatenarLimiteInferior(".");
                        break;
                }
            }
            else
            {
                if (!codigointer.Equals("Back"))
                {
                    if (textBox1.Text.Length > FuncionLenght)
                    {
                        textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Count() - 1);
                    }
                }
                else
                {
                    if (textBox1.Text.Count() >= 1)
                    {
                        limiteInferior = limiteInferior.Substring(0, limiteInferior.Length - 1);
                    }
                    else
                    {
                        limiteInferior = "";
                    }
                }
            }
            textBox1.SelectionStart = textBox1.TextLength;
            FuncionLenght = textBox1.Text.Length;
        }
        public void ConcatenarLimiteInferior(string cadena)
        {
            limiteInferior = limiteInferior + cadena;
            textBox1.Text = limiteInferior;
            FuncionLenght = limiteInferior.Length;
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.TextLength;
        }        
        public void ConcatenarTolerancia(string cadena)
        {
            tolerancia = tolerancia + cadena;
            textBox3.Text = tolerancia;
            FuncionLenght = tolerancia.Length;
            textBox3.Focus();
            textBox3.SelectionStart = textBox3.TextLength;
        }
        public void ConcatenarLimiteSuperior(string cadena)
        {
            limiteSuperior = limiteSuperior + cadena;
            textBox2.Text = limiteSuperior;
            FuncionLenght = limiteSuperior.Length;
            textBox2.Focus();
            textBox2.SelectionStart = textBox2.TextLength;
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string codigointer;
            codigointer = e.KeyCode.ToString();
            int letra = e.KeyValue;
            if (letra >= 47 & letra <= 57 || letra == 190 || letra == 189)
            {
                switch (letra)
                {
                    case 48:
                        ConcatenarLimiteSuperior("0");
                        break;
                    case 49:
                        ConcatenarLimiteSuperior("1");
                        break;
                    case 50:
                        ConcatenarLimiteSuperior("2");
                        break;
                    case 51:
                        ConcatenarLimiteSuperior("3");
                        break;
                    case 52:
                        ConcatenarLimiteSuperior("4");
                        break;
                    case 53:
                        ConcatenarLimiteSuperior("5");
                        break;
                    case 54:
                        ConcatenarLimiteSuperior("6");
                        break;
                    case 55:
                        ConcatenarLimiteSuperior("7");
                        break;
                    case 56:
                        ConcatenarLimiteSuperior("8");
                        break;
                    case 57:
                        ConcatenarLimiteSuperior("9");
                        break;
                    case 189:
                        ConcatenarLimiteSuperior("-");
                        break;
                    case 190:
                        ConcatenarLimiteSuperior(".");
                        break;
                }
            }
            else
            {
                if (!codigointer.Equals("Back"))
                {
                    if (textBox2.Text.Length > FuncionLenght)
                    {
                        textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Count() - 1);
                    }
                }
                else
                {
                    if (textBox2.Text.Count() >= 1)
                    {
                        limiteSuperior = limiteSuperior.Substring(0, limiteSuperior.Length - 1);
                    }
                    else
                    {
                        limiteSuperior = "";
                    }
                }
            }
            textBox2.SelectionStart = textBox2.TextLength;
            FuncionLenght = textBox2.Text.Length;
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string codigointer;
            codigointer = e.KeyCode.ToString();
            int letra = e.KeyValue;
            if (letra >= 47 & letra <= 57 || letra == 190 || letra == 189)
            {
                switch (letra)
                {
                    case 48:
                        ConcatenarTolerancia("0");
                        break;
                    case 49:
                        ConcatenarTolerancia("1");
                        break;
                    case 50:
                        ConcatenarTolerancia("2");
                        break;
                    case 51:
                        ConcatenarTolerancia("3");
                        break;
                    case 52:
                        ConcatenarTolerancia("4");
                        break;
                    case 53:
                        ConcatenarTolerancia("5");
                        break;
                    case 54:
                        ConcatenarTolerancia("6");
                        break;
                    case 55:
                        ConcatenarTolerancia("7");
                        break;
                    case 56:
                        ConcatenarTolerancia("8");
                        break;
                    case 57:
                        ConcatenarTolerancia("9");
                        break;
                    case 189:
                        ConcatenarTolerancia("-");
                        break;
                    case 190:
                        ConcatenarTolerancia(".");
                        break;
                }
            }
            else
            {
                if (!codigointer.Equals("Back"))
                {
                    if (textBox3.Text.Length > FuncionLenght)
                    {
                        textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Count() - 1);
                    }
                }
                else
                {
                    if (textBox3.Text.Count() >= 1)
                    {
                        tolerancia = tolerancia.Substring(0, tolerancia.Length - 1);
                    }
                    else
                    {
                        tolerancia = "";
                    }
                }
            }
            textBox3.SelectionStart = textBox3.TextLength;
            FuncionLenght = textBox3.Text.Length;
        }
    }
}
