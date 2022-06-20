using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seidel_Implementation
{
    public static class Gauss
    {
        private static double[] SolveLinearEquations(string[] input)
        {
            double[][] rows = new double[input.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = (double[])Array.ConvertAll(input[i].Split(' '), double.Parse);
            }
            return SolveLinearEquations(rows);
        }

        public static double[] SolveLinearEquations(double[][] rows)
        {

            int length = rows[0].Length;

            for (int i = 0; i < rows.Length - 1; i++)
            {
                if (rows[i][i] == 0 && !Swap(rows, i, i))
                {
                    return null;
                }

                for (int j = i; j < rows.Length; j++)
                {
                    double[] d = new double[length];
                    for (int x = 0; x < length; x++)
                    {
                        d[x] = rows[j][x];
                        if (rows[j][i] != 0)
                        {
                            d[x] = d[x] / rows[j][i];
                        }
                    }
                    rows[j] = d;
                }

                for (int y = i + 1; y < rows.Length; y++)
                {
                    double[] f = new double[length];
                    for (int g = 0; g < length; g++)
                    {
                        f[g] = rows[y][g];
                        if (rows[y][i] != 0)
                        {
                            f[g] = f[g] - rows[i][g];
                        }

                    }
                    rows[y] = f;
                }
            }

            return CalculateResult(rows);
        }

        private static bool Swap(double[][] rows, int row, int column)
        {
            bool swapped = false;
            for (int z = rows.Length - 1; z > row; z--)
            {
                if (rows[z][row] != 0)
                {
                    double[] temp = new double[rows[0].Length];
                    temp = rows[z];
                    rows[z] = rows[column];
                    rows[column] = temp;
                    swapped = true;
                }
            }

            return swapped;
        }
        private static double[] CalculateResult(double[][] rows)
        {
            double val = 0;
            int length = rows[0].Length;
            double[] result = new double[rows.Length];
            for (int i = rows.Length - 1; i >= 0; i--)
            {
                val = rows[i][length - 1];
                for (int x = length - 2; x > i - 1; x--)
                {
                    val -= rows[i][x] * result[x];
                }
                result[i] = val / rows[i][i];

                if (!IsValidResult(result[i]))
                {
                    return null;
                }
            }
            return result;
        }

        private static bool IsValidResult(double result)
        {
            return !(double.IsNaN(result) || double.IsInfinity(result));
        }

        private static string ConvertToString(double[] result)
        {
            StringBuilder sb = new StringBuilder(1024);
            for (int i = 0; i < result.Length; i++)
            {
                sb.AppendFormat("X{0} = {1}\r\n", i + 1, Math.Round(result[i], 10));
            }
            return sb.ToString();
        }

    }
}
