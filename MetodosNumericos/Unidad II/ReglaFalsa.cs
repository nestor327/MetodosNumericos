using MetodosNumericos.Calculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosNumericos.Unidad_II
{
    public class ReglaFalsa
    {
        StringToFormula formula = new StringToFormula();
        double xi, xs;
        public List<double> MetodoReglaFalsa(List<string> tokens, double xi, double xs,double erroor)
        {
            List<double> table = new List<double>();
            this.xi = xi;
            this.xs = xs;

            string Ecaucion = "";
            for (int i = 0; i < tokens.Count; i++)
            {
                Ecaucion = Ecaucion + tokens.ElementAt(i);
            }
            double error = 1;
            double xr = 0;
            int it = 0;
            table.Add(++it);
            table.Add(xi);
            table.Add(xs);
            xr = xs - (double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xs.ToString()))) * (xi - xs)) / (double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString()))) - double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xs.ToString()))));
            table.Add(xr);
            table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString()))));
            table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xs.ToString()))));
            table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xr.ToString()))));
            table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString())))* double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xr.ToString()))));
            table.Add(xr);
            table.Add(0);
            while (error > erroor)
            {
                table.Add(++it);

                if (double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xr.ToString()))) == 0)
                {
                    error = 0;
                }
                else if (table.ElementAt(table.Count - 4) < 0)
                {
                    xs = xr;
                }
                else
                {
                    xi = xr;
                }
                table.Add(xi);
                table.Add(xs);
                xr = xs - (double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xs.ToString()))) * (xi - xs)) / (double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString()))) - double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xs.ToString()))));
                table.Add(xr);
                table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString()))));
                table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xs.ToString()))));
                table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xr.ToString()))));
                table.Add(double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xi.ToString()))) * double.Parse(formula.ValorEsperado(Ecaucion.Replace("x", xr.ToString()))));
                error = Math.Abs(table.ElementAt(table.Count - 5) - table.ElementAt(table.Count - 15));
                table.Add(error);
                if (table.ElementAt(table.Count - 1) > erroor)
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
