using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosNumericos.Unidad_II
{
    public class SSeidel
    {
        public List<double> MetodoDeSeidel(List<double> elementos, int filas, double error, List<double> iniciales)
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
            for (int i = 0; i < filas; i++)
            {
                lista.Add(0);
            }
            double errorr = 1;
            while (errorr >error)
            {
                lista.Add(++contador);
                double valorEsperado = 0;
                double errorr2 = 0, errorr3 = 0;
                for (int y = 0; y < filas; y++)
                {
                    valorEsperado = elementos.ElementAt((y + 1) * (filas + 1) - 1)/ elementos.ElementAt(y * (filas + 1) + y);
                    for (int i = 0; i < y; i++)
                    {
                            valorEsperado = valorEsperado - elementos.ElementAt((y) * (filas + 1) + i)* lista.ElementAt((contador - 1) * (3 * filas + 1) + 2 * i+1) / elementos.ElementAt(y * (filas + 1) + y);                        
                    }
                    for (int i = y+1; i < filas; i++)
                    {                        
                            valorEsperado = valorEsperado - elementos.ElementAt((y)* (filas + 1) + i) * lista.ElementAt((contador - 2) * (3 * filas + 1) + 2*i + 1) / elementos.ElementAt(y * (filas + 1) + y);                        
                    }
                    lista.Add(valorEsperado);
                    errorr2 = (Math.Abs(valorEsperado - lista.ElementAt(lista.Count-3*filas-2)));
                    lista.Add(errorr2);
                    if (errorr2 > errorr3)
                    {
                        errorr3 = errorr2;
                    }
                }

                errorr=errorr3;
                for (int i=0;i<filas;i++)
                {
                    if (lista.ElementAt((contador-1)*(3*filas+1)+2*(i+1))>error)
                    {
                        lista.Add(0);
                    }
                    else
                    {
                        lista.Add(1);
                    }
                }

            }
            return lista;
        }
    }
}
