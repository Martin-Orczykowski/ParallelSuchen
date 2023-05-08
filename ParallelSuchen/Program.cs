using System.Text.RegularExpressions;

namespace ParallelSuchen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pfad = @"D:\test\";
            string suchwort = "der";
            List<string> list = new List<string>();

            string[] Files = Directory.GetFiles(pfad,"*.txt");

            Parallel.ForEach(Files, file =>
            {
                Arbeit(file,suchwort,list);
            });

            Ausgabe(list);
        }

        public static void Ausgabe(List<string> list)
        {
            string pfad = "";
            string[] liste = list.Distinct().ToArray();
            Array.Reverse(liste);
            foreach (string item in liste)
            {
                string tmp = Path.GetFileName(item);
                Console.WriteLine("Gefunden in : " + tmp);
                pfad = item;
            }

            pfad = Path.GetDirectoryName(pfad);

            Console.WriteLine("In Verzeichnis gesucht : " + pfad);
        }

        public static void Arbeit(string file, string suchwort,List<string> list)
        {
            using (StreamReader sr = new StreamReader(file))
            {
               while(sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();

                    line = Regex.Replace(line, "[^\\w ]", string.Empty);

                    string[] zeile = line.Split(' ');

                    foreach(string item in zeile)
                    {
                        if(suchwort == item)
                        {

                                list.Add(file);
                                break;
                        }
                    }
                }
            }
        }
    }
}