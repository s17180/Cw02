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

            string f;
            int a = args.Length;

            if(a == 0 || args[0].Equals(""))
            {
                f = "dane.csv";
            }
            else
            {
                f = args[0];
            }

            string res;

            if (a < 2 || args[1].Equals(""))
            {
                res = "result.xml";
            }
            else
            {
                res = args[1];
            }
            if (a == 3 && !args[2].Equals("xml"))
            {
                Console.WriteLine("FILE FORMAT IS INVALID!!!!!");
                return;
            }
            string file2 = "log.txt";

            StreamWriter streamWriter = null;
            streamWriter = new StreamWriter(file2);

            FileInfo fi = new FileInfo(f);
            List<Student> los = new List<Student>();
            Dictionary<string, int> stud = new Dictionary<string, int>();
            try
            {
                using (StreamReader stream = new StreamReader(fi.OpenRead()))
                {


                    string line = "";
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] studentWiersz = line.Split(",");
                        //Console.WriteLine(line);
                        if (studentWiersz.Length == 9)
                        {
                            bool t = true;

                            foreach (string str in studentWiersz)
                            {
                                if (str.Equals(""))
                                    t = false;
                            }

                            if (t)
                            {
                                Studies s = new Studies
                                {
                                    name = studentWiersz[2],
                                    mode = studentWiersz[3],
                                };

                                var stu = new Student
                                {
                                    firstname = studentWiersz[0],
                                    surname = studentWiersz[1],
                                    index = int.Parse(studentWiersz[4]),
                                    studies = s,
                                    date = studentWiersz[5],
                                    email = studentWiersz[6],
                                    fatherName = studentWiersz[8],
                                    motherName = studentWiersz[7]
                                };
                                los.Add(stu);

                                if (stud.ContainsKey(s.name))
                                {
                                    stud[s.name]++;
                                }
                                else
                                {
                                    stud.Add(s.name, 1);
                                }
                            }
                            else
                            {
                                streamWriter.WriteLine("nieprawidłowa struktura danych:" + line);
                            }
                        }
                        else
                        {
                            streamWriter.WriteLine("nieprawidłowa struktura danych:" + line);
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("FileNotFoundException(\"Plik nazwa nie istnieje\")");
                streamWriter.WriteLine("FileNotFoundException(\"Plik nazwa nie istnieje\")");
                return;
            }
            catch (ArgumentException e1)
            {
                Console.WriteLine("ArgumentException(\"Podana ścieżka jest niepoprawna\")");
                streamWriter.WriteLine("ArgumentException(\"Podana ścieżka jest niepoprawna\")");
                return;
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.Message);
                streamWriter.WriteLine(e2.Message);
                return;
            }
            streamWriter.Flush();
            streamWriter.Close();

            FileStream writer = new FileStream(@res, FileMode.Create);
            XmlRootAttribute rotatr = new XmlRootAttribute("studenci");
        }
    }
}
