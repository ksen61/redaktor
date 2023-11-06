using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Figure
{
    public string name { get; set; }
    public int width { get; set; }
    public int length { get; set; }

    public Figure(string name, int width, int length)
    {
        this.name = name;
        this.width = width;
        this.length = length;
    }
}

public class MainGlobal
{
    public static List<string> set = new List<string>();

    static void Main()
    {
        SerilOrDesirelClass.Seril(Starta());
        StrelochkiClass.MenuText();
    }

    private static string Starta()
    {
        Console.WriteLine("Введите путь до файла, который хотите открыть");
        string a = Console.ReadLine();
        return a;
    }

    public static class SerilOrDesirelClass
    {
        public static void Seril(string a)
        {
            if (a.EndsWith(".txt") == true)
            {
                TxtSer(a);
            }
            else if (a.EndsWith(".json") == true)
            {
                JsonSer(a);
            }
            else if (a.EndsWith(".xml") == true)
            {
                XmlSer(a);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Кажется вы неправильно ввели путь!");
            }
        }

        private static void TxtSer(string a)
        {
            try
            {
                string[] filmtxt = File.ReadAllLines(a);
                MainGlobal.set.AddRange(filmtxt);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Кажется вы неправильно ввели путь!");
            }
        }

        private static void JsonSer(string a)
        {
            string txt = File.ReadAllText(a);
            List<Figure> result = JsonConvert.DeserializeObject<List<Figure>>(txt);
            foreach (Figure i in result)
            {
                MainGlobal.set.Add(i.name);
                MainGlobal.set.Add(i.width.ToString());
                MainGlobal.set.Add(i.length.ToString());
            }
        }

        private static void XmlSer(string a)
        {
            List<Figure> b;
            XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
            using (FileStream fs = new FileStream(a, FileMode.Open))
            {
                b = (List<Figure>)xml.Deserialize(fs);
            }
            foreach (Figure i in b)
            {
                MainGlobal.set.Add(i.name);
                MainGlobal.set.Add(i.width.ToString());
                MainGlobal.set.Add(i.length.ToString());
            }
        }

        public static void Desirel(string a)
        {
            if (a.EndsWith(".txt") == true)
            {
                foreach (string i in MainGlobal.set)
                {
                    File.AppendAllText(a, i + "\n");
                }
                Environment.Exit(0);
            }
            else if (a.EndsWith(".json") == true)
            {
                List<Figure> Figures = new List<Figure>();
                int duration;
                int rating;
                for (int i = 0; i < MainGlobal.set.Count; i = i + 3)
                {
                    Figure b = new Figure(MainGlobal.set[i], duration = Convert.ToInt32(MainGlobal.set[i + 1]), rating = Convert.ToInt32(MainGlobal.set[i + 2]));
                    Figures.Add(b);
                }
                string json = JsonConvert.SerializeObject(Figures);
                File.WriteAllText(a, json);
                Environment.Exit(0);
            }
            else if (a.EndsWith(".xml") == true)
            {
                List<Figure> Figures = new List<Figure>();
                int duration;
                int rating;
                for (int i = 0; i < MainGlobal.set.Count; i = i + 3)
                {
                    Figure b = new Figure(MainGlobal.set[i], duration = Convert.ToInt32(MainGlobal.set[i + 1]), rating = Convert.ToInt32(MainGlobal.set[i + 2]));
                    Figures.Add(b);
                }
                XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                using (FileStream fs = new FileStream(a, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, Figures);
                }
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Кажется вы неправильно ввели путь!");
            }
        }
    }

    public static class StrelochkiClass
    {
        public static void MenuText()
        {
            Console.Clear();
            Console.WriteLine("Нажмите F1 для сохранения файла\nНажмите Esc для закрытия программы");
            foreach (string i in MainGlobal.set)
            {
                Console.WriteLine("  " + i);
            }
            var position = 2;
            while (true)
            {
                ConsoleKeyInfo UserButton;
                Console.SetCursorPosition(0, position);
                Console.WriteLine("->");
                UserButton = Console.ReadKey();
                Console.SetCursorPosition(0, position);
                Console.WriteLine("  ");
                if (UserButton.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    Console.WriteLine("Введите путь, куда хотите сохранить измененный файл");
                    string a;
                    a = Console.ReadLine();
                    SerilOrDesirelClass.Desirel(a);
                }
                else if (UserButton.Key == ConsoleKey.DownArrow & position != MainGlobal.set.Count + 1)
                {
                    position++;
                }
                else if (UserButton.Key == ConsoleKey.UpArrow & position != 2)
                {
                    position--;
                }
                else if (UserButton.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (UserButton.Key == ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("                                                                                                                ");
                    Console.SetCursorPosition(2, position);
                    MainGlobal.set[position - 2] = Console.ReadLine();
                }
            }
        }
    }
}