using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Net;

namespace UsporediXML_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            XDocument xml1 = XDocument.Load(@"C:\Users\linar\Documents\programiranje\CompareXML_LINQ-main\prvi.xml");
            XDocument xml2 = XDocument.Load(@"C:\Users\linar\Documents\programiranje\CompareXML_LINQ-main\drugi.xml");


            var result1 = from xmlBooks1 in xml1.Descendants("book")
                          from xmlBooks2 in xml2.Descendants("book")
                          select new
                          {
                              book1 = new
                              {
                                  id = xmlBooks1.Attribute("id").Value,
                                  image = xmlBooks1.Attribute("image").Value,
                                  name = xmlBooks1.Attribute("name").Value
                              },
                              book2 = new
                              {
                                  id = xmlBooks2.Attribute("id").Value,
                                  image = xmlBooks2.Attribute("image").Value,
                                  name = xmlBooks2.Attribute("name").Value
                              }
                          };

            
            var result2 = from i in result1
                          where (i.book1.id == i.book2.id
                                 || i.book1.image == i.book2.image
                                 || i.book1.name == i.book2.name) &&
                                 !(i.book1.id == i.book2.id
                                 && i.book1.image == i.book2.image
                                 && i.book1.name == i.book2.name)
                          select i;

            Console.WriteLine("Issued\tIssue type\t\tIssueInFirst\tIssueInSecond");
            int greska = 0;
            foreach (var aa in result2)
            {
                greska++;
                string message = Convert.ToString(greska);


                if (aa.book1.id != aa.book2.id)
                {
                    message += "\tid is different\t\t" + aa.book1.id + "\t\t" + aa.book2.id;
                }
                if (aa.book1.image != aa.book2.image)
                {
                    message += "\timage is different\t" + aa.book1.image + "\t\t" + aa.book2.image;
                }

                if (aa.book1.name != aa.book2.name)
                {
                    message += "\tname is different\t" + aa.book1.name + "\t\t" + aa.book2.name;
                }

                Console.WriteLine(message);
            }

            Console.ReadKey();
        }
    }
}
