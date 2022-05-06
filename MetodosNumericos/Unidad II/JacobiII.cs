using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosNumericos.Unidad_II
{
    public class JacobiII
    {
        //elementos:(Datos de la matriz),iniciales:(los datos iniciales x0,x1...)
            public List<double> MetodoDeJacobi(List<double> elementos, int filas, double error, List<double> iniciales)
            {
                List<double> lista = new List<double>();
                int contador = 0;
                lista.Add(++contador);
                for (int i = 0; i < filas; i++)
                {
                    lista.Add(iniciales.ElementAt(i));
                }
                for (int i = 0; i < filas; i++)
                {
                    lista.Add(iniciales.ElementAt(i));
                }
                lista.Add(0);
                double errorr = 1;
                while (errorr > error)
                {
                    lista.Add(++contador);
                    double valorEsperado = 0;
                    for (int y = 0; y < filas; y++)
                    {
                        valorEsperado = elementos.ElementAt((y + 1) * (filas + 1) - 1) / elementos.ElementAt(y * (filas + 1) + y);
                        for (int i = 0; i < filas; i++)
                        {
                            if (i != y)
                            {
                                valorEsperado = valorEsperado - elementos.ElementAt((y) * (filas + 1) + i) * lista.ElementAt((contador - 2) * (2 * filas + 2) + i + 1) / elementos.ElementAt(y * (filas + 1) + y);
                            }
                        }
                        lista.Add(valorEsperado);
                    }
                    double errorr2 = 0, errorr3 = 0;
                    bool valorVerdad = false;
                    for (int y = 0; y < filas; y++)
                    {
                        errorr2 = (Math.Abs(lista.ElementAt((contador - 2) * (2 * filas + 2) + (y + 1)) - lista.ElementAt((contador - 1) * (2 * filas + 2) + (y + 1))));
                        lista.Add(errorr2);
                        if (errorr2 < error)
                        {
                            valorVerdad = true;
                        }
                        else
                        {
                            valorVerdad = false;
                        }
                    }

                    if (valorVerdad == false)
                    {
                        lista.Add(0);
                        errorr = 1;
                    }
                    else
                    {
                        lista.Add(1);
                        errorr = error - 1;
                    }
                }
                return lista;
            }
        }
}
