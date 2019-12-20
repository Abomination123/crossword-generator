using System;
using System.Collections.Generic;
using System.IO;

namespace crossw
{

    class Program
    {

        static void Main(string[] args)
        {

            List<String> slova = new List<string>();
            List<String> blackList = new List<string>();
            using (StreamReader sr = new StreamReader(@"C:\Users\Дмитрий\spisok.txt"))
            {
                while (!sr.EndOfStream)
                    foreach (string s in sr.ReadLine().Split(' '))
                    {
                        slova.Add(s);
                    }
            }
            slova.Sort((a, b) => b.Length - a.Length);

            char[,] Board = new char[40, 40];

            try
            {
                for (int z = 0; z < slova[0].Length; z++)
                {
                    Board[14, z + 13] = slova[0][z];
                }
                blackList.Add(slova[0]);


                foreach (string w in slova)
                {
                    for (int z = 0; z < w.Length; z++)
                    {
                        EachCell(w, z);
                    }
                }

                void EachCell(string w, int z)
                {
                    for (int i = 0; i < Board.GetLength(0); i++)
                    {
                        for (int j = 0; j < Board.GetLength(1); j++)
                        {
                            PlaceIfMatches(w, z, i, j);
                        }
                    }
                }




                void PlaceIfMatches(string w, int z, int i, int j)
                {
                    if (Board[i, j] == w[z] && Board[i + 1, j] == '\0' && Board[i - 1, j] == '\0' && CanBePlacedHorizont(w, i, j, z) && !blackList.Contains(w))
                    {
                        for (int p = 0; p < w.Length; p++)
                        {
                            Board[i - z + p, j] = w[p];

                        }
                        blackList.Add(w);
                    }
                    else if (Board[i, j] == w[z] && Board[i, j + 1] == '\0' && Board[i, j - 1] == '\0' && CanbePlacedVertical(w, i, j, z) && !blackList.Contains(w))
                    {
                        for (int p = 0; p < w.Length; p++)
                        {
                            Board[i, j - z + p] = w[p];

                        }
                        blackList.Add(w);
                    }

                }

                bool CanBePlacedHorizont(string w, int i, int j, int z)
                {
                    bool b = true;
                    for (int p = -1; p < w.Length + 1; p++)
                    {
                        if (p != -1 && p != w.Length) { b = Board[i - z + p, j] != w[p]; } else { b = true; }
                        if (p == z) continue;
                        if (Board[i - z + p, j - 1] != '\0' ||
                            Board[i - z + p, j + 1] != '\0' ||
                            (Board[i - z + p, j] != '\0' && b)) return false;
                    }
                    return true;
                }
                bool CanbePlacedVertical(string w, int i, int j, int z)
                {
                    bool b = true;
                    for (int p = -1; p < w.Length + 1; p++)
                    {
                        if (p != -1 && p != w.Length) { b = Board[i , j - z + p] != w[p]; } else { b = true; }
                        if (p == z) continue;
                        if (Board[i - 1, j - z + p] != '\0' ||
                            Board[i + 1, j - z + p] != '\0' ||
                            (Board[i, j - z + p] != '\0' && b)) return false;
                    }
                    return true;
                }



            }
            catch
            {
                Console.WriteLine("\n**");
            }
            finally
            {
                for (int i = 0; i < Board.GetLength(0); i++)
                {
                    for (int j = 0; j < Board.GetLength(1); j++)
                    {
                        if (j == Board.GetLength(1) - 1)
                        {
                            Console.Write($"{Board[i, j]} \n");
                        }
                        else
                        {
                            Console.Write($"{Board[i, j]}  ");
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
