using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosNumericos.Calculos
{
	public class StringToFormula
	{

		public List<string> getTokens(string expression)
		{
			string operators = "()^*/-+lcrastvdy";
			List<string> tokens = new List<string>();
			StringBuilder sb = new StringBuilder();
			char[] funcionChar = expression.Replace(" ", string.Empty).ToCharArray();
			
			for (int i = 0; i < funcionChar.Length; i++)
			{
				if (operators.IndexOf(funcionChar[i]) >= 0)
				{
					if ((i-1)>= 0)
					{					
						if (operators.IndexOf(funcionChar[i]) == 5 && Char.IsDigit(funcionChar[i + 1]) && char.IsDigit(funcionChar[i - 1]) == false && funcionChar[i-1]!=')')
						{
							//	sb.Append(funcionChar[i]);
							sb.Append(funcionChar[i]);
						}
						else if (operators.IndexOf(funcionChar[i]) == 5 && Char.IsDigit(funcionChar[i + 1]) && char.IsDigit(funcionChar[i - 1]) == false && funcionChar[i - 1] == ')')
                        {
							//Esto lo agregue recientemente
							tokens.Add(funcionChar[i].ToString());
						}
						else if (operators.IndexOf(funcionChar[i]) == 6 && Char.IsDigit(funcionChar[i + 1]) && char.IsDigit(funcionChar[i - 1]) == false && funcionChar[i - 1] != ')')
						{
							string funcion = "";
                            for (int y=0;y<funcionChar.Length;y++)
                            {
								funcion = funcion + funcionChar[y].ToString();
                            }
							//Este lo puse recientemente
							tokens.Add(funcionChar[i].ToString());							
						}else if (operators.IndexOf(funcionChar[i]) == 6 && Char.IsDigit(funcionChar[i + 1]) && char.IsDigit(funcionChar[i - 1]) == false && funcionChar[i - 1] == ')')
                        {
							sb.Append(funcionChar[i]);
						}
						else
						{
							if ((sb.Length > 0))
							{
								tokens.Add(sb.ToString());
								sb.Length = 0;
							}
							tokens.Add(funcionChar[i].ToString());
						}
                    }
                    else
                    {
						if (operators.IndexOf(funcionChar[i]) == 5 && Char.IsDigit(funcionChar[i + 1]))
						{
							sb.Append(funcionChar[i]);
						}
						else if (operators.IndexOf(funcionChar[i]) == 6 && Char.IsDigit(funcionChar[i + 1]))
						{
							sb.Append(funcionChar[i]);
						}
						else
						{
							if ((sb.Length > 0))
							{
								tokens.Add(sb.ToString());
								sb.Length = 0;
							}
							tokens.Add(funcionChar[i].ToString());
						}
					}
				}
				else if (funcionChar[i]=='x')
                {
					tokens.Add(funcionChar[i].ToString());
                }else
				{
					sb.Append(funcionChar[i]);
				}
			}

			if ((sb.Length > 0))
			{
				tokens.Add(sb.ToString());
			}
			return tokens;
		}

		public string ValorEsperado(string expression)
		{
			string funcion = expression;
			int contParentesis = 0;
			List<string> stokeFuncion = new List<string>();
			List<string> stokenFuncionArreglos = new List<string>();
			foreach (char c in expression.ToCharArray())
			{
				if (c == '(')
				{
					contParentesis++;
				}
			}

			do
			{
				stokeFuncion = EvaluarSegunNestor(funcion);
				for (int i = 0; i < stokeFuncion.Count; i++)
				{
					if (stokeFuncion.ElementAt(i).ToCharArray()[0] == '(')
					{
						if (char.IsDigit(stokeFuncion.ElementAt(i + 1).ToCharArray()[stokeFuncion.ElementAt(i + 1).ToCharArray().Length - 1]) && stokeFuncion.ElementAt(i + 2).ToCharArray()[stokeFuncion.ElementAt(i + 2).ToCharArray().Length - 1] == ')')
						{
							stokeFuncion.RemoveAt(i + 2);
							stokeFuncion.RemoveAt(i);
							contParentesis = contParentesis - 1;
						}
					}
				}
				funcion = "";
				for (int i = 0; i < stokeFuncion.Count; i++)
				{
					funcion = funcion + stokeFuncion.ElementAt(i);
				}
			} while (contParentesis > 0);
			//LEY DE GAUS, DIFERENCIA DE POTENICAL Y CAPACITANCIA			
			funcion = "";
			for (int i = 0; i < stokeFuncion.Count; i++)
			{
				funcion = funcion + stokeFuncion.ElementAt(i);
			}
			stokeFuncion = EvaluarSegunNestor(funcion);		
			
			funcion = "";
			for (int i = 0; i < stokeFuncion.Count; i++)
			{
				funcion = funcion + stokeFuncion.ElementAt(i);
			}
			stokeFuncion = EvaluarSegunNestor(funcion);
			funcion = "";
			for (int i = 0; i < stokeFuncion.Count; i++)
			{
				funcion = funcion + stokeFuncion.ElementAt(i);
			}

			//AQUI ES DONDE TIENES QUE VER EL PROBLEMA DEL SENO
			/*
			bool esReal = false;
			string valor = "";
            if (funcion.Length==1)
            {
                if (char.IsDigit(funcion.ToCharArray()[0]))
                {
					esReal = true;
                }
                else
                {
					esReal = false;
                }
            }

            for (int i=1;i<funcion.Length;i++)
            {
                if (char.IsDigit(funcion.ToCharArray()[i]))
                {
					esReal = true;
                }
                else
                {
					esReal = false;
					break;
                }
            }

            if (esReal==false)
            {
				return ValorEsperado(funcion);
            }*/

			return funcion;
        }

		public List<string> EvaluarSegunNestor(string expression)
		{
			List<string> partesFuncion = getTokens(expression);
			List<string> partesFuncionMove = new List<string>();
			int multContador = 0, divContador = 0, sumContador = 0, restContador = 0, logContador = 0, cosContador = 0, powContador = 0, sqrtContador = 0, absContador = 0
				, senoContador = 0, tanContador = 0, arcCosContador = 0, arcSenContador = 0, arcTanContador = 0;
			string caracter = "";
			for (int k = 0; k < partesFuncion.Count; k++)
			{
				caracter = partesFuncion.ElementAt(k);
				if (caracter.Equals("*"))
				{
					multContador++;
				} else if (caracter.Equals("/"))
				{
					divContador++;
				}
				else if (caracter.Equals("+"))
				{
					sumContador++;
				}
				else if (caracter.Equals("-"))
				{
					restContador++;
				} else if (caracter.Equals("l"))
				{
					logContador++;
				} else if (caracter.Equals("c"))
				{
					cosContador++;
				} else if (caracter.Equals("^"))
				{
					powContador++;
				} else if (caracter.Equals("r"))
				{
					sqrtContador++;
				} else if (caracter.Equals("a"))
				{
					absContador++;
				} else if (caracter.Equals("s"))
				{
					senoContador++;
				}
				else if (caracter.Equals("t"))
				{
					tanContador++;
				}
				else if (caracter.Equals("v"))
				{
					arcCosContador++;
				}
				else if (caracter.Equals("d"))
				{
					arcSenContador++;
				}
				else if (caracter.Equals("y"))
				{
					arcTanContador++;
				}
			}

			string Numfuntion = "";
			for (int j = 0; j < partesFuncion.Count; j++)
			{
				Numfuntion = Numfuntion + "|" + partesFuncion.ElementAt(j);
			}

			while (multContador > 0)
			{
				int p = partesFuncion.Count;
				for (int i = 0; i < partesFuncion.Count; i++)
				{
					if (partesFuncion.Count > i)
					{

						if (partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).ToCharArray().Length - 1] == '*')
						{

							if (char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[(partesFuncion.ElementAt(i - 1).ToCharArray().Length - 1)])
								&& (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[(partesFuncion.ElementAt(i + 1).ToCharArray().Length - 1)])) &&
								((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || Char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0])) &&
								((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || Char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
								|| partesFuncion.ElementAt(i + 1).ToCharArray()[0] == '-' || partesFuncion.ElementAt(i + 1).ToCharArray()[0] == '+')
							{
								partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
								partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) * double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
								if ((i + 2) < partesFuncion.Count)
								{
									for (int a = i + 2; a < partesFuncion.Count; a++)
									{
										partesFuncionMove.Add(partesFuncion.ElementAt(a));
									}
									partesFuncion.Clear();
									for (int b = 0; b < partesFuncionMove.Count; b++)
									{
										partesFuncion.Add(partesFuncionMove.ElementAt(b));
									}
									partesFuncionMove.Clear();
								}
								else
								{
									partesFuncion.Clear();
									for (int b = 0; b < partesFuncionMove.Count; b++)
									{
										partesFuncion.Add(partesFuncionMove.ElementAt(b));
									}
									partesFuncionMove.Clear();
								}
								break;
							}
							else
							{
								partesFuncionMove.Add(partesFuncion.ElementAt(i));
							}


						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
						string funtion = "";
						for (int j = 0; j < partesFuncion.Count; j++)
						{
							funtion = funtion + partesFuncion.ElementAt(j);
						}
					}
				}
				multContador--;
			}
			partesFuncionMove.Clear();
			while (divContador > 0)
			{
				int p = partesFuncion.Count;
				for (int i = 0; i < partesFuncion.Count; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).ToCharArray().Length - 1] == '/')
					{
						if (char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[(partesFuncion.ElementAt(i - 1).ToCharArray().Length - 1)]) && char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[(partesFuncion.ElementAt(i + 1).ToCharArray().Length - 1)])
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || Char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0])) && ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || Char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0])))
						{
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) / double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();

							}
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				divContador--;
			}
			partesFuncionMove.Clear();

			while (sumContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count; i++)
				{
					if ((i <= 1 || i >= partesFuncion.Count - 2) && (partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).Length - 1] == '+'))
					{
						if ((i == 1 && partesFuncion.Count - 1 <= 2) && char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0])))
						{
							//Aqui solo es cuando hay tres elemntos y esta en el final e inicio al mismo tiempo
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) + double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						} else if ((i == 1 && partesFuncion.Count - 1 >= 3) && char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
							&& (partesFuncion.ElementAt(i + 2).ToCharArray()[(partesFuncion.ElementAt(i + 2).Length - 1)] == '-' || partesFuncion.ElementAt(i + 2).ToCharArray()[(partesFuncion.ElementAt(i + 2).Length - 1)] == '+'
							|| partesFuncion.ElementAt(i + 2).ToCharArray()[(partesFuncion.ElementAt(i + 2).Length - 1)] == ')')
							)
						{
							//Aqui es cuan hay mas de tres elementos y esta al inicio
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) + double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						} else if ((i == partesFuncion.Count - 2 && partesFuncion.Count - 1 > 2)
							&& char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
							&& partesFuncion.ElementAt(i - 2).ToCharArray()[partesFuncion.ElementAt(i - 2).Length - 1] == '-')
						{
							//Aqui es cuando hay mas de tres elementos y esta al final con una resta
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add("+");
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) * (-1) + double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else if ((i == partesFuncion.Count - 2 && partesFuncion.Count - 1 > 2)
							&& char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
							&& (partesFuncion.ElementAt(i - 2).ToCharArray()[partesFuncion.ElementAt(i - 2).Length - 1] == '+'
							|| partesFuncion.ElementAt(i - 2).ToCharArray()[(partesFuncion.ElementAt(i - 2).Length - 1)] == '('))
						{
							//Aqui es cuando hay mas de tres elementos y esta al final con una suma
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) + double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else if ((partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).Length - 1] == '+'))
					{
						if (char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
							&& (partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == '-' || partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == '+'
							|| partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == ')')
							&& (partesFuncion.ElementAt(i - 2).ToCharArray()[(partesFuncion.ElementAt(i - 2).Length - 1)] == '(' || partesFuncion.ElementAt(i - 2).ToCharArray()[partesFuncion.ElementAt(i - 2).Length - 1] == '+'))
						{
							//Aqui estas en el centro de la ecuacion y tienes una suma al inicio
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) + double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else if (char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
							&& (partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == '-' || partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == '+'
							|| partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == ')')
							&& partesFuncion.ElementAt(i - 2).ToCharArray()[partesFuncion.ElementAt(i - 2).Length - 1] == '-')
						{
							//Aqui estas en el centro de la ecuacion y tienes una resta al inicio
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add("+");
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) * (-1) + double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				sumContador--;
			}


			while (restContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count; i++)
				{
					if ((i <= 1 || i >= partesFuncion.Count - 2) && (partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).Length - 1] == '-'))
					{
						if ((i == 1 && partesFuncion.Count - 1 <= 2) && char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0])))
						{
							//Aqui solo es cuando hay tres elemntos y esta en el final e inicio al mismo tiempo
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) - double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else if ((i == 1 && partesFuncion.Count - 1 >= 3) && char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
						  && char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
						  && ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
						  && ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
						  && (partesFuncion.ElementAt(i + 2).ToCharArray()[(partesFuncion.ElementAt(i + 2).Length - 1)] == '-' || partesFuncion.ElementAt(i + 2).ToCharArray()[(partesFuncion.ElementAt(i + 2).Length - 1)] == '+'
						  || partesFuncion.ElementAt(i + 2).ToCharArray()[(partesFuncion.ElementAt(i + 2).Length - 1)] == ')')
						  )
						{
							//Aqui es cuan hay mas de tres elementos y esta al inicio
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) - double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else if ((i == partesFuncion.Count - 2 && partesFuncion.Count - 1 > 2)
						  && char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
						  && char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
						  && ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
						  && ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
						  && partesFuncion.ElementAt(i - 2).ToCharArray()[partesFuncion.ElementAt(i - 2).Length - 1] == '-')
						{
							//Aqui es cuando hay mas de tres elementos y esta al final con una resta
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add("+");
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) * (-1) - double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else if ((i == partesFuncion.Count - 2 && partesFuncion.Count - 1 > 2)
							&& char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
							&& (partesFuncion.ElementAt(i - 2).ToCharArray()[partesFuncion.ElementAt(i - 2).Length - 1] == '+'
							|| partesFuncion.ElementAt(i - 2).ToCharArray()[(partesFuncion.ElementAt(i - 2).Length - 1)] == '('))
						{
							//Aqui es cuando hay mas de tres elementos y esta al final con una suma
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) - double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else if ((partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).Length - 1] == '-'))
					{
						if (char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
							&& (partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == '-' || partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == '+'
							|| partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == ')')
							&& (partesFuncion.ElementAt(i - 2).ToCharArray()[(partesFuncion.ElementAt(i - 2).Length - 1)] == '(' || partesFuncion.ElementAt(i - 2).ToCharArray()[partesFuncion.ElementAt(i - 2).Length - 1] == '+'))
						{
							//Aqui estas en el centro de la ecuacion y tienes una suma al inicio
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) - double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else if (char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1])
							&& char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1])
							&& ((partesFuncion.ElementAt(i - 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[0]))
							&& ((partesFuncion.ElementAt(i + 1).ToCharArray()[0]) == '-' || char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[0]))
							&& (partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == '-' || partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == '+'
							|| partesFuncion.ElementAt(i + 2).ToCharArray()[partesFuncion.ElementAt(i + 2).Length - 1] == ')')
							&& partesFuncion.ElementAt(i - 2).ToCharArray()[partesFuncion.ElementAt(i - 2).Length - 1] == '-')
						{
							//Aqui estas en el centro de la ecuacion y tienes una resta al inicio
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add("+");
							partesFuncionMove.Add((double.Parse(partesFuncion.ElementAt(i - 1)) * (-1) - double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sumContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				restContador--;
			}
			partesFuncionMove.Clear();


			partesFuncionMove.Clear();
			while (logContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).ToCharArray().Length - 1] == 'l')
					{

						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							partesFuncionMove.Add(Math.Log10((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}


					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				logContador--;
			}
			while (cosContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).ToCharArray().Length - 1] == 'c')
					{

						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							partesFuncionMove.Add(Math.Cos((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}


					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				cosContador--;
			}
			while (powContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[partesFuncion.ElementAt(i).ToCharArray().Length - 1] == '^')
					{

						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]) && char.IsDigit(partesFuncion.ElementAt(i - 1).ToCharArray()[partesFuncion.ElementAt(i - 1).Length - 1]))
						{
							partesFuncionMove.RemoveAt(partesFuncionMove.Count - 1);
							partesFuncionMove.Add(Math.Pow(double.Parse(partesFuncion.ElementAt(i - 1)), double.Parse(partesFuncion.ElementAt(i + 1))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}


					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				powContador--;
			}

			while (sqrtContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					//MessageBox.Show("Si entras al metodo "+i);
					if (partesFuncion.ElementAt(i).ToCharArray()[0] == 'r')
					{

						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							//			MessageBox.Show("Estas en la pocision "+ i);
							partesFuncionMove.Add(Math.Sqrt((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							sqrtContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				sqrtContador--;
			}

			while (absContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					//MessageBox.Show("Si entras al metodo "+i);
					if (partesFuncion.ElementAt(i).ToCharArray()[0] == 'a')
					{

						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							partesFuncionMove.Add(Math.Abs((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							absContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				absContador--;
			}
			while (senoContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[0] == 's')
					{
						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							partesFuncionMove.Add(Math.Sin((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							senoContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				senoContador--;
			}

			while (tanContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[0] == 't')
					{
						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							partesFuncionMove.Add(Math.Tan((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							tanContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				tanContador--;
			}

			while (arcCosContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[0] == 'v')
					{
						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							partesFuncionMove.Add(Math.Acos((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							arcCosContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				arcCosContador--;
			}
			while (arcSenContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[0] == 'd')
					{
						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							partesFuncionMove.Add(Math.Asin((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							arcSenContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				arcSenContador--;
			}
			while (arcTanContador > 0)
			{
				for (int i = 0; i < partesFuncion.Count - 1; i++)
				{
					if (partesFuncion.ElementAt(i).ToCharArray()[0] == 'y')
					{
						if (char.IsDigit(partesFuncion.ElementAt(i + 1).ToCharArray()[partesFuncion.ElementAt(i + 1).Length - 1]))
						{
							partesFuncionMove.Add(Math.Atan((double.Parse(partesFuncion.ElementAt(i + 1)))).ToString());
							if ((i + 2) < partesFuncion.Count)
							{
								for (int a = i + 2; a < partesFuncion.Count; a++)
								{
									partesFuncionMove.Add(partesFuncion.ElementAt(a));
								}
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							else
							{
								partesFuncion.Clear();
								for (int b = 0; b < partesFuncionMove.Count; b++)
								{
									partesFuncion.Add(partesFuncionMove.ElementAt(b));
								}
								partesFuncionMove.Clear();
							}
							arcTanContador++;
							break;
						}
						else
						{
							partesFuncionMove.Add(partesFuncion.ElementAt(i));
						}
					}
					else
					{
						partesFuncionMove.Add(partesFuncion.ElementAt(i));
					}
				}
				arcTanContador--;
			} 

			return partesFuncion;			
		}
	}
}


