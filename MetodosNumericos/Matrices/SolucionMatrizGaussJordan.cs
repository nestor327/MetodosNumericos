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
    public partial class SolucionMatrizGaussJordan : Form
    {
        public string sistema;
        public string matriz;
        public int filas;
        public string vector;
        public string vectorIncognitas;
        public string matrizTranspueta;
        public List<double> matrizTotal = new List<double>();
        public SolucionMatrizGaussJordan()
        {
            InitializeComponent();
        }

        private void SolucionMatrizGaussJordan_Load(object sender, EventArgs e)
        {
            this.AutoScroll = true;
            label6.Text = matriz;
            label3.Text = sistema;
            label7.Text = vector.Replace(" ", "");
            int fuente;
            if (filas <= 3)
            {
                label13.Font = new Font(label13.Font.Name, 36);
                label14.Font = new Font(label13.Font.Name, 36);
                label15.Font = new Font(label15.Font.Name, 36);
                label16.Font = new Font(label16.Font.Name, 36);
                label17.Font = new Font(label17.Font.Name, 36);
                label18.Font = new Font(label18.Font.Name, 36);
                label19.Font = new Font(label19.Font.Name, 36);
                label20.Font = new Font(label20.Font.Name, 36);
                label22.Font = new Font(label22.Font.Name, 36);
                label23.Font = new Font(label23.Font.Name, 36);
                fuente = 36;
            } else if (filas == 4)
            {
                label13.Font = new Font(label13.Font.Name, 48);
                label14.Font = new Font(label13.Font.Name, 48);
                label15.Font = new Font(label15.Font.Name, 48);
                label16.Font = new Font(label16.Font.Name, 48);
                label17.Font = new Font(label17.Font.Name, 48);
                label18.Font = new Font(label18.Font.Name, 48);
                label19.Font = new Font(label19.Font.Name, 48);
                label20.Font = new Font(label20.Font.Name, 48);
                label22.Font = new Font(label22.Font.Name, 48);
                label23.Font = new Font(label23.Font.Name, 48);
                fuente = 48;
            }
            else
            {
                label13.Font = new Font(label13.Font.Name, 72);
                label14.Font = new Font(label13.Font.Name, 72);
                label15.Font = new Font(label15.Font.Name, 72);
                label16.Font = new Font(label16.Font.Name, 72);
                label17.Font = new Font(label17.Font.Name, 72);
                label18.Font = new Font(label18.Font.Name, 72);
                label19.Font = new Font(label19.Font.Name, 72);
                label20.Font = new Font(label20.Font.Name, 72);
                label22.Font = new Font(label22.Font.Name, 72);
                label23.Font = new Font(label23.Font.Name, 72);
                fuente = 72;
            }

            label13.Location = new Point(label13.Location.X, label3.Location.Y + 35 + label3.Height * 413 / 408);
            label6.Location = new Point((label13.Location.X + (label13.Size.Width * 413 / 408)), label13.Location.Y);
            label13.Location = new Point(label13.Location.X, label13.Location.Y + (label6.Size.Height - label13.Size.Height) * 413 / 816);
            label14.Location = new Point(label6.Location.X + (label6.Size.Width * 413 / 408), label13.Location.Y);
            label4.Location = new Point(label4.Location.X, ((label13.Location.Y) + label13.Height * 413 / 816 - label4.Height * 413 / 816));
            label5.Location = new Point(label14.Location.X + label14.Size.Width * 413 / 408, label4.Location.Y);
            label15.Location = new Point(label5.Location.X + label5.Width * 413 / 408, label13.Location.Y);
            label7.Location = new Point(label15.Location.X + (label15.Size.Width * 413 / 408), label6.Location.Y);
            label16.Location = new Point(label7.Location.X + label7.Width * 413 / 408, label13.Location.Y);
            label8.Location = new Point(label8.Location.X, label6.Location.Y + 35 + label6.Height * 413 / 408);
            label9.Text = matriz;
            label17.Location = new Point(label13.Location.X, label8.Location.Y + 20 + label8.Height * 413 / 408);
            label9.Location = new Point(label17.Location.X + label17.Width * 413 / 408, label17.Location.Y);
            label17.Location = new Point(label17.Location.X, label9.Location.Y + label9.Height * 413 / 816 - label17.Height * 413 / 816);
            label18.Location = new Point(label9.Location.X + label9.Width * 413 / 408, label17.Location.Y);
            label10.Location = new Point(label18.Location.X + label18.Width * 413 / 408, label18.Location.Y + label18.Height * 413 / 816 - label10.Height * 413 / 816);
            label23.Location = new Point(label10.Location.X + label10.Width * 413 / 408, label18.Location.Y);
            label24.Location = new Point(label23.Location.X + label23.Width * 413 / 408, label9.Location.Y);
            label24.Text = vectorIncognitas;
            label22.Location = new Point(label24.Location.X + label24.Width * 413 / 408, label23.Location.Y);
            label11.Location = new Point(label22.Location.X + label22.Width * 413 / 408, label10.Location.Y);
            label20.Location = new Point(label11.Location.X + label11.Width * 413 / 408, label22.Location.Y);
            label21.Text = vector.Replace(" ", "");
            label21.Location = new Point(label20.Location.X + label20.Width * 413 / 408, label24.Location.Y);
            label19.Location = new Point(label21.Location.X + label21.Width * 413 / 408, label20.Location.Y);
            label12.Location = new Point(label8.Location.X, label9.Location.Y + 35 + label9.Height * 413 / 408);
            label25.Text = matrizTranspueta;
            label26.Location = new Point(label17.Location.X, label12.Location.Y + label12.Height * 413 / 408 + 35);
            label25.Text = matriz;
            label25.Location = new Point(label26.Location.X + label26.Width * 413 / 408, label26.Location.Y);
            label26.Location = new Point(label26.Location.X, label25.Location.Y + label25.Height * 413 / 816 - label26.Height * 413 / 816);
            string varra = "";
            for (int i = 0; i < filas; i++)
            {
                varra = varra + ":\n";
            }
            label28.Text = varra;
            
            label28.Location = new Point(label25.Location.X + label25.Width * 413 / 408 + 15, label25.Location.Y);
            label29.Text = vector.Replace(" ", "");
            label29.Location = new Point(label28.Location.X + label28.Width * 413 / 408 + 15, label28.Location.Y);
            label27.Location = new Point(label29.Location.X + label29.Width * 413 / 408, label26.Location.Y);
            label36.Location = new Point(label19.Location.X + label19.Width * 413 / 408 + 40, label2.Location.Y);
            label36.Font = new Font(label36.Font.Name, fuente);
            label37.Text = label25.Text;
            label37.Location = new Point(label36.Location.X + label36.Width * 413 / 408, label36.Location.Y);
            label36.Location = new Point(label36.Location.X, label37.Location.Y + label37.Height * 413 / 816 - label36.Height * 413 / 816);
            label34.Location = new Point(label37.Location.X + label37.Width * 413 / 408, label37.Location.Y);
            label34.Text = label28.Text;
            label33.Location = new Point(label34.Location.X + label34.Width * 413 / 408, label34.Location.Y);
            label33.Text = label29.Text;
            label35.Location = new Point(label33.Location.X + label33.Width * 413 / 408, label36.Location.Y);
            label35.Font = new Font(label33.Font.Name, label36.Font.Size);
            label32.Location = new Point(label35.Location.X + label35.Width * 413 / 408, label33.Location.Y);
            label32.Text = "F" + subIndices(1) + "→" + "(1/" + matrizTotal.ElementAt(0).ToString() + ")*F" + subIndices(1);



            label30.Location = new Point(label36.Location.X, label36.Location.Y + label36.Height * 413 / 408 + 35);
            label30.Font = new Font(label27.Font.Name, label27.Font.Size);
            //Apartir de aqui empesamos a hacer las operaciones elementales
            List<Label> elementales = new List<Label>();
            elementales.Add(label30);
            Label parentesisIzqui;
            Label parentesisDerec;
            Label matrizPrin;
            Label centro;
            Label vectorLabel;
            Label señalUnica;
            Label señalVarias;
            List<double> matrizActualizada = new List<double>();
            foreach (double d in matrizTotal)
            {
                matrizActualizada.Add(d);
            }

            for (int i = 0; i < filas; i++)
            {
                double sustituto;
                double inicial = matrizActualizada.ElementAt(i * (filas + 1) + i);
                for (int j = 0; j <= filas; j++)
                {
                    sustituto = matrizActualizada.ElementAt((i) * (filas + 1) + j);
                    matrizActualizada.RemoveAt((i) * (filas + 1) + j);
                    matrizActualizada.Insert((i) * (filas + 1) + j, sustituto / inicial);
                }


                matrizPrin = new Label();
                matrizPrin.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408,
                elementales.ElementAt(elementales.Count - 1).Location.Y);
                int maximo = 0;
                for (int m = 0; m < matrizActualizada.Count; m++)
                {
                    if (maximo < Math.Round(matrizActualizada.ElementAt(m), 7).ToString().Length)
                    {
                        maximo = Math.Round(matrizActualizada.ElementAt(m), 7).ToString().Length;
                    }
                }
                maximo = maximo + 4;
                matrizPrin.Text = "";
                string vectorSolo = "";
                for (int y = 0; y < filas; y++)
                {
                    for (int k = 0; k <= filas; k++)
                    {
                        string valorModi = Math.Round(matrizActualizada.ElementAt(y * (filas + 1) + k), 7).ToString();
                        int limite = Math.Abs(matrizActualizada.ElementAt(y * (filas + 1) + k).ToString().Length - maximo);
                        if (k == 0 || k == filas)
                        {
                            limite = limite - 4;
                        }
                        if (k != filas)
                        {
                            for (int a = 0; a < limite; a++)
                            {
                                valorModi = " " + valorModi;
                            }
                            matrizPrin.Text = matrizPrin.Text + valorModi;
                        }
                        else
                        {
                            for (int a = 0; a < limite; a++)
                            {
                                valorModi = " " + valorModi;
                            }
                            vectorSolo = vectorSolo + valorModi + "\n";
                        }


                    }
                    matrizPrin.Text = matrizPrin.Text + "\n";
                }

                matrizPrin.Font = new Font(elementales.ElementAt(elementales.Count - 1).Font.Name, label6.Font.Size);
                matrizPrin.Size = new Size(matrizPrin.PreferredWidth, matrizPrin.PreferredHeight);
                elementales.Add(matrizPrin);
                elementales.ElementAt(elementales.Count - 2).Location = new Point(elementales.ElementAt(elementales.Count - 2).Location.X,
                    elementales.ElementAt(elementales.Count - 1).Location.Y + elementales.ElementAt(elementales.Count - 1).Height * 413 / 816 - elementales.ElementAt(elementales.Count - 2).Height * 413 / 816);


                centro = new Label();
                centro.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408,
                    elementales.ElementAt(elementales.Count - 1).Location.Y);
                centro.Text = "";
                for (int b = 0; b < filas; b++)
                {
                    centro.Text = centro.Text + ":\n";
                }
                centro.Font = new Font(elementales.ElementAt(elementales.Count - 1).Font.Name, label6.Font.Size + 2);
                centro.Size = new Size(centro.PreferredWidth, centro.PreferredHeight);
                elementales.Add(centro);

                vectorLabel = new Label();
                vectorLabel.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408, elementales.ElementAt(elementales.Count - 1).Location.Y);
                vectorLabel.Text = vectorSolo;
                vectorLabel.Font = new Font(elementales.ElementAt(elementales.Count - 1).Font.Name, elementales.ElementAt(elementales.Count - 2).Font.Size);
                vectorLabel.Size = new Size(vectorLabel.PreferredWidth, vectorLabel.PreferredHeight);
                elementales.Add(vectorLabel);


                parentesisDerec = new Label();
                parentesisDerec.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408,
                elementales.ElementAt(elementales.Count - 4).Location.Y);
                parentesisDerec.Text = ")";
                parentesisDerec.Font = new Font(label30.Font.Name, label30.Font.Size);
                parentesisDerec.Size = new Size(parentesisDerec.PreferredWidth, parentesisDerec.PreferredHeight);
                elementales.Add(parentesisDerec);


                señalVarias = new Label();
                señalVarias.Location = new Point(elementales.ElementAt((elementales.Count - 1)).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408,
                    elementales.ElementAt(elementales.Count - 2).Location.Y);
                string señal = "";
                for (int v = 0; v < filas; v++)
                {
                    if (v > i)
                    {
                        señal = señal + "F" + subIndices(v + 1) + " → " + "F" + subIndices(v + 1) + " + F" + subIndices(i + 1) + "*(" + Math.Round((matrizActualizada.ElementAt(v * (filas + 1) + i) * (-1)),7).ToString() + ")\n";
                    } else
                    {
                        señal = señal + "\n";
                    }
                }
                señalVarias.Text = señal;
                señal = "";
                señalVarias.Font = new Font(label30.Font.Name, elementales.ElementAt(elementales.Count - 2).Font.Size);
                señalVarias.Size = new Size(señalVarias.PreferredWidth, señalVarias.PreferredHeight);
                elementales.Add(señalVarias);



                parentesisIzqui = new Label();
                parentesisIzqui.Location = new Point(elementales.ElementAt(elementales.Count - 6).Location.X, elementales.ElementAt(elementales.Count - 6).Location.Y + 35 + elementales.ElementAt(elementales.Count - 6).Height * 413 / 408);
                parentesisIzqui.Text = "(";
                parentesisIzqui.Font = new Font(label30.Font.Name, label30.Font.Size);
                parentesisIzqui.Size = new Size(parentesisIzqui.PreferredWidth, parentesisIzqui.PreferredHeight);
                elementales.Add(parentesisIzqui);

                if (i != filas - 1)
                {
                    double pibote;
                    for (int k = i + 1; k < filas; k++)
                    {
                        pibote = matrizActualizada.ElementAt((k) * (filas + 1) + i);
                        for (int t = 0; t <= filas; t++)
                        {
                            matrizActualizada.Insert((k) * (filas + 1) + t + 1, (matrizActualizada.ElementAt((k) * (filas + 1) + t) - matrizActualizada.ElementAt(i * (filas + 1) + t) * pibote));
                            matrizActualizada.RemoveAt((k) * (filas + 1) + t);
                        }
                    }

                    matrizPrin = new Label();
                    matrizPrin.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408,
                    elementales.ElementAt(elementales.Count - 1).Location.Y);
                    maximo = 0;
                    for (int m = 0; m < matrizActualizada.Count; m++)
                    {
                        if (maximo < Math.Round(matrizActualizada.ElementAt(m), 7).ToString().Length)
                        {
                            maximo = Math.Round(matrizActualizada.ElementAt(m), 7).ToString().Length;
                        }
                    }
                    maximo = maximo + 4;
                    matrizPrin.Text = "";
                    vectorSolo = "";
                    for (int y = 0; y < filas; y++)
                    {
                        for (int k = 0; k <= filas; k++)
                        {
                            string valorModi = Math.Round(matrizActualizada.ElementAt(y * (filas + 1) + k), 7).ToString();
                            int limite = Math.Abs(matrizActualizada.ElementAt(y * (filas + 1) + k).ToString().Length - maximo);
                            if (k == 0 || k == filas)
                            {
                                limite = limite - 4;
                            }
                            if (k != filas)
                            {
                                for (int a = 0; a < limite; a++)
                                {
                                    valorModi = " " + valorModi;
                                }
                                matrizPrin.Text = matrizPrin.Text + valorModi;
                            }
                            else
                            {
                                for (int a = 0; a < limite; a++)
                                {
                                    valorModi = " " + valorModi;
                                }
                                vectorSolo = vectorSolo + valorModi + "\n";
                            }


                        }
                        matrizPrin.Text = matrizPrin.Text + "\n";
                    }

                    matrizPrin.Font = new Font(elementales.ElementAt(elementales.Count - 1).Font.Name, label6.Font.Size);
                    matrizPrin.Size = new Size(matrizPrin.PreferredWidth, matrizPrin.PreferredHeight);
                    elementales.Add(matrizPrin);
                    elementales.ElementAt(elementales.Count - 2).Location = new Point(elementales.ElementAt(elementales.Count - 2).Location.X,
                        elementales.ElementAt(elementales.Count - 1).Location.Y + elementales.ElementAt(elementales.Count - 1).Height * 413 / 816 - elementales.ElementAt(elementales.Count - 2).Height * 413 / 816);


                    centro = new Label();
                    centro.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408,
                        elementales.ElementAt(elementales.Count - 1).Location.Y);
                    centro.Text = "";
                    for (int b = 0; b < filas; b++)
                    {
                        centro.Text = centro.Text + ":\n";
                    }
                    centro.Font = new Font(elementales.ElementAt(elementales.Count - 1).Font.Name, label6.Font.Size + 2);
                    centro.Size = new Size(centro.PreferredWidth, centro.PreferredHeight);
                    elementales.Add(centro);

                    vectorLabel = new Label();
                    vectorLabel.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408, elementales.ElementAt(elementales.Count - 1).Location.Y);
                    vectorLabel.Text = vectorSolo;
                    vectorLabel.Font = new Font(elementales.ElementAt(elementales.Count - 1).Font.Name, elementales.ElementAt(elementales.Count - 2).Font.Size);
                    vectorLabel.Size = new Size(vectorLabel.PreferredWidth, vectorLabel.PreferredHeight);
                    elementales.Add(vectorLabel);


                    parentesisDerec = new Label();
                    parentesisDerec.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408,
                    elementales.ElementAt(elementales.Count - 4).Location.Y);
                    parentesisDerec.Text = ")";
                    parentesisDerec.Font = new Font(label30.Font.Name, label30.Font.Size);
                    parentesisDerec.Size = new Size(parentesisDerec.PreferredWidth, parentesisDerec.PreferredHeight);
                    elementales.Add(parentesisDerec);

                    señalUnica = new Label();
                    señalUnica.Location = new Point(elementales.ElementAt(elementales.Count - 1).Location.X + elementales.ElementAt(elementales.Count - 1).Width * 413 / 408,
                        elementales.ElementAt(elementales.Count - 2).Location.Y);
                    string señalunica = "";
                    señalUnica.Text = "";
                    for (int y = 0; y < filas; y++)
                    {
                        if (y == i)
                        {
                            señalunica = señalunica + "F" + subIndices(i + 2) + " → " + "(1/" + Math.Round(matrizActualizada.ElementAt((i + 1) * (filas + 1) + (i + 1)),7) + ")*F" + subIndices(i + 2);
                        }
                        else
                        {
                            señalunica = señalunica + "\n";
                        }
                    }
                    señalUnica.Text = señalunica;
                    señalunica = "";
                    señalUnica.Font = new Font(label30.Font.Name, label29.Font.Size);
                    señalUnica.Size = new Size(señalUnica.PreferredWidth, señalUnica.PreferredHeight);
                    elementales.Add(señalUnica);

                    parentesisIzqui = new Label();
                    parentesisIzqui.Location = new Point(elementales.ElementAt(elementales.Count - 6).Location.X, elementales.ElementAt(elementales.Count - 6).Location.Y + 35 + elementales.ElementAt(elementales.Count - 6).Height * 413 / 408);
                    parentesisIzqui.Text = "(";
                    parentesisIzqui.Font = new Font(label30.Font.Name, label30.Font.Size);
                    parentesisIzqui.Size = new Size(parentesisIzqui.PreferredWidth, parentesisIzqui.PreferredHeight);
                    elementales.Add(parentesisIzqui);
                }




            }

            for (int i = 0; i < elementales.Count - 1; i++)
            {
                elementales.ElementAt(i).Parent = this;
            }
            if ((label26.Height*413/408+label26.Location.Y)> (elementales.ElementAt(elementales.Count - 2).Height*413/408+elementales.ElementAt(elementales.Count - 2).Location.Y))
            {
                label31.Location = new Point(label26.Location.X, label26.Location.Y + label26.Height * 413 / 408 + 35);
            }
            else
            {
                label31.Location = new Point(label26.Location.X, elementales.ElementAt(elementales.Count - 2).Location.Y + elementales.ElementAt(elementales.Count - 2).Height * 413 / 408 + 35);
            }
            string solucion = "";
            List<double> solucionesReales = new List<double>();
            for (int t = 0; t < filas; t++)
            {
                double solu = 0;
                solucion = solucion + "x" + subIndices(t + 1) + " = " + Math.Round(matrizActualizada.ElementAt((filas - t) * (filas + 1) - 1), 7).ToString();
                solu = matrizActualizada.ElementAt((filas - t) * (filas + 1) - 1);
                for (int s = (filas - t); s < filas; s++)
                {
                    solucion = solucion + " + (" + Math.Round((matrizActualizada.ElementAt((filas - t - 1) * (filas + 1) + s) * (-1)), 7).ToString() + ")x" + subIndices(s + 1);
                    solu = solu - matrizActualizada.ElementAt((filas - t - 1) * (filas + 1) + s) * solucionesReales.ElementAt(filas - s - 1);
                }
                solucionesReales.Add(solu);
                solucion = solucion + " = " + Math.Round(solu, 7).ToString() + "\n";
                solu = 0;
            }
            label38.Text = solucion;
            label38.Location = new Point(label31.Location.X, label31.Location.Y + label31.Height * 413 / 408 + 10);
            label40.Location = new Point(label38.Location.X + label38.Width * 413 / 408 + 40, label31.Location.Y);
            label39.Location = new Point(label40.Location.X, label38.Location.Y);
            for (int d=0;d<filas;d++)
            {
                sistema = sistema.Replace(("x"+subIndices(d+1)),"*("+Math.Round(solucionesReales.ElementAt(d),7).ToString()+")");
            }
            label39.Text = sistema;
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
    }
}
