using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
    class Table
    {
        public void GetTableSize(out int xMax, out int yMax)
        {
            xMax = Convert.ToInt32(ConfigurationManager.AppSettings["TableLength"]);
            yMax = Convert.ToInt32(ConfigurationManager.AppSettings["TableHeight"]);
        }
    }
}
