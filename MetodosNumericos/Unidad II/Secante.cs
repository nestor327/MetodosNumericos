using MetodosNumericos.Calculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosNumericos.Unidad_II
{
    public class Secante
    {
        StringToFormula formula = new StringToFormula();
        double x0, x1;
        public List<double> MetodoSecante(List<string> tokens, double x0, double x1)
        {
            List<double> table = new List<double>();
            this.x0 = x0;
            this.x1 = x1;

            string Ecaucion = "";
            for (int i = 0; i < tokens.Count; i++)
            {
                Ecaucion = Ecaucion + tokens.ElementAt(i);
            }
            double error = 1;
            double xi = 0;
            int it = 0;
            table.Add(++it);
            table.Add(x0);
            table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x",x0.ToString()))));
            table.Add(x0);
            table.Add(0);
            table.Add(++it);
            table.Add(x1);
            table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", x1.ToString()))));
            table.Add(Math.Abs(x0-x1));
            table.Add(0);
            while (error>0.00000001)
            {
                table.Add(++it);
                table.Add(table.ElementAt(table.Count-5)-((table.ElementAt(table.Count - 5)- table.ElementAt(table.Count - 10))* table.ElementAt(table.Count - 4)) 
                    /(table.ElementAt(table.Count - 4)- table.ElementAt(table.Count - 9)));
                table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x",table.ElementAt(table.Count-1).ToString()))));
                error = Math.Abs(table.ElementAt(table.Count-2)-table.ElementAt(table.Count-7));
                table.Add(error);
                if (error>0.00000001)
                {
                    table.Add(0);
                }
                else
                {
                    table.Add(1);
                }
            }
            return table;
        }
    }
}
