using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace linq24
{
    public class car
    {
        public int name { get; set; }
        public string division { get; set; }
        public string carline { get; set; }
        public double displacement { get; set; }
        public int cyl { get; set; }
        public int cfe { get; set; }
        public int hfe { get; set; }
        public int ccfe { get; set; }

        internal static car ParseFromcsv(string line)
        {
            var columns = line.Split(',');
            return new car
            {

            
                division = columns[1],
                carline = columns[2],
                displacement = double.Parse(columns[3]),
                cyl = int.Parse(columns[4]),
                cfe = int.Parse(columns[5]),
                hfe = int.Parse(columns[6]),
                ccfe = int.Parse(columns[7])

            };
        }
    }
}
        
           