using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fixgcode
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Count() > 0)
         {
            Console.Write("argument passed\n");
            string filename = args[0];


            StreamReader sr = new StreamReader(filename);

            StreamWriter sw = new StreamWriter(filename + ".fix.gcode");

            int countFix = 0;

            while (sr.Peek() >= 0)
            {
               string line = sr.ReadLine();
               Console.WriteLine(line);

               int posY = line.IndexOf("Y");
               if (posY > 0)
               {
                  //check char before y
                  string charBefore = line.Substring(posY - 1, 1);
                  if (!charBefore.Equals(" "))
                  {
                     line = line.Replace("Y", " Y");

                     Console.WriteLine(line);
                  }

               }
               // i file generati con inkscape non hanno hop z

               int posG0F3 = line.IndexOf("G0 F3000");
               if (posG0F3 >= 0)
               {
                  sw.WriteLine("G0 Z1");
                  sw.WriteLine("G0 F3000");
                  string l = sr.ReadLine();
                  sw.WriteLine(l);
                  sw.WriteLine("G0 Z0");
                  line = "";

               }
                  

               countFix++;

               sw.WriteLine(line);
            }

            Console.WriteLine("Fixed " + countFix + " lines");

            sr.Close();
            sw.Close();
         }
      }
   }
}
