using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace linq24

{

    
    class Program
    {
        static void Main(string[] args)
        {
            var path = ProcessFile("fuel.csv");
            var path1 = ProcessFile1("manufacturers.csv");

          var query = from c in path            // Query for join Operation
                        join manu in path1
                        on c.division  equals manu.name
                        orderby c.carline descending, c.name ascending
                        select new
                        {
                            manu.name,
                            c.carline,
                           c.division
                       }; 

          
            var query4 = path.Join     // Method Syntax for join operation
                (  
                path1,
                c => c.division,
                m => m.name,
             (c, m) => new
             {
                 m.headquarters,
                 c.division,
                 c.carline
             }
             );
            Console.WriteLine(" \n THIS IS JOIN OPERATION THROUGH METHOD SYNTAX \n");
            foreach (var s in query4.Take(10))
            {
                Console.WriteLine($"{s.headquarters} {s.division}:{s.carline}");
            }

            Console.WriteLine("\n This data is of  Join operation  :\n  ");

            foreach (var x in query.Take(10))
            {
                           Console.WriteLine($"{x.carline} {x.name} : {x.division}");
            }

            var query2 = from car in path  // Query for  Group By Operation

                group car by car.division.ToUpper()into division
                orderby division.Key
                select division
                ;
            Console.WriteLine("\n This query is of  Group By : \n  ");

            foreach (var group in query2.Take(10))
            {
                Console.WriteLine($"{group.Key} has {group.Count()} car");

            }

            var query5 = path.Join(path1,                                 // composite key
                c => new { manufacturers = c.division, year = c.name },
                m => new { manufacturers = m.name, m.year },
                  (c, m) => new
                  {

                      c.division,
                      m.headquarters,
                      c.carline,
                      m.year
                  }

                );
            foreach (var m in query5.Take(10))
                {
                Console.WriteLine($" {m.division} {m.headquarters} {m.year}");

            }

            Console.WriteLine("\n Data fetched from CSV File : \n  "); // Data from CSV (fuel.csv)

            foreach (var car in path.Take(10))
            {
                Console.WriteLine($" {car.division}  {car.ccfe}");
            }

            Console.WriteLine("\n Data Fetched From CSV file : \n"); // Data from CSV (manufacturers.csv)
           foreach(var man in path1.Take(10))
            {   
                
                Console.WriteLine($"{man.name} {man.headquarters} {man.year}");
            }
            Console.ReadLine();

        }

        private static List<manufacturers> ProcessFile1(string path1)
        {
            var query =
                File.ReadAllLines(path1)
                .Where(line => line.Length > 1)
                .Select(line =>
                {
                    var columns = line.Split(',');
                    return new manufacturers
                    {
                        name = columns[0],
                        headquarters = columns[1],
                        year = int.Parse(columns[2])
                    };
                });
            return query.ToList();
        }

        private static List<car> ProcessFile(string path)
        {
            var query =
                from line in File.ReadAllLines(path).Skip(1)
                where line.Length > 1
                select car.ParseFromcsv(line);
                return query.ToList();
                       

        }

     


    }
}

