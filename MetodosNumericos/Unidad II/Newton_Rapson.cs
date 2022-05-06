using MetodosNumericos.Calculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosNumericos.Unidad_II
{
    public class Newton_Rapson
    {
        StringToFormula formula = new StringToFormula();
        double x0;
        public List<double> MetodoNewtonRapson(List<string> tokens, double x0)
        {
            List<double> table = new List<double>();
            this.x0 = x0;            
            
            string Ecaucion = "";
            for (int i = 0; i < tokens.Count; i++)
            {
                Ecaucion = Ecaucion + tokens.ElementAt(i);
            }
            double error = 1;
            
            int it = 0;
            table.Add(++it);
            table.Add(x0);
            table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x",x0.ToString()))));
            table.Add((double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", (x0-1).ToString())))- double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", (x0+1).ToString())))) /(-2));
            table.Add(x0);
            table.Add(0);
            while (error > 0.00000001)
            {
                table.Add(++it);
                table.Add(table.ElementAt(table.Count-6)-((table.ElementAt(table.Count-5))/(table.ElementAt(table.Count - 4))));                
                table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", (table.ElementAt(table.Count-1)).ToString()))));
                table.Add((double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", ((table.ElementAt(table.Count-2))-1).ToString())))- 
                    double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", ((table.ElementAt(table.Count - 2)) +1).ToString())))) /(-2));
                error = Math.Abs(table.ElementAt(table.Count - 3) - table.ElementAt(table.Count - 9));
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
            //(y1-y0)/(x1-x0)
            return table;
        }
    }
}
 