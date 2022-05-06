using MetodosNumericos.Calculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosNumericos.Unidad_II
{
    public class Biseccion
    {
        StringToFormula formula = new StringToFormula();
        public List<double> MetodoDeBiseccion(List<string> tokens, double a, double b, double tolerancia)
        {
            List<double> table = new List<double>();
            double intervaloI = a, intervaloD = b;

            string Ecaucion = "";
            for (int i = 0; i < tokens.Count; i++)
            {
                Ecaucion = Ecaucion + tokens.ElementAt(i);
            }
            double error = 1;
            double xi = 0;
            int it = 0;
            while (error > tolerancia)
            {
                table.Add(++it);
                table.Add(intervaloI);
                table.Add((intervaloD));
                xi = (intervaloI + intervaloD) / 2;
                if (double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString()))) == 0)
                {
                    error = 0;
                }
                else if (double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString()))) * double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", intervaloI.ToString()))) < 0)
                {
                    intervaloD = xi;
                }
                else
                {
                    intervaloI = xi;
                }

                table.Add(xi);
                table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x",table.ElementAt(table.Count-3).ToString()))));
                table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString()))));
                table.Add(table.ElementAt(table.Count-1)*table.ElementAt(table.Count-2));
                if (table.Count-13>0) 
                {
                    table.Add(Math.Abs(table.ElementAt(table.Count - 13) - xi));
                }
                else
                {
                    table.Add(xi);
                }
                if (table.ElementAt(table.Count-1)> tolerancia)
                {
                    table.Add(0);
                }
                else
                {
                    table.Add(1);
                }             
                
                if (table.Count > 8)
                {
                    error = table.ElementAt(table.Count-2);
                }                
            }          
            
            return table;
        }
    }
}

/*Analisis del campo magnetico en un tuvo metalico que conduce una corriente (aplicar la Ley de Amper)
                   relacionarlo con el cableado estrcturado, las normas en casode hacer un cbleado de red, tuvos metalicos
                       de aluminio, pasar el cable de red paralelo al cable de la red domiciliar se entrega en la semana del 17 de mayo el guion*/