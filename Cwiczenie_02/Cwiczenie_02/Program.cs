using Cwiczenie_02.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Cwiczenie_02
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "dane.csv";

            FileInfo f  = new FileInfo(file);
            // StreamReader stream = new StreamReader(f.OpenRead());


            var m = new Dictionary<String, int>();


            
            //virtual - override
            string file2 = @"Data\dane.csv";
            using (StreamReader stream = new StreamReader(f.OpenRead())) {
                String line = "";
                while ((line = stream.ReadLine()) != null)
                {

                    string[] studentWiersz = line.Split(',');
                    Console.WriteLine(line);
                }

            }
            // stream.Dispose();


            //xml

            FileStream writer = new FileStream(@"data.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>), 
                                       new XmlRootAttribute("uczelnia"));

            var list = new List<Student>();
            var st = new Student
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Email = "kowalski@wp.pl"
            };
            list.Add(st);
            serializer.Serialize(writer, list);

        }
    }
}
