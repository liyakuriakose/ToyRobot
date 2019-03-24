using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
    class ToyRobot
    {
        static void Main(string[] args)
        {
            Util util = new Util();
            CommandProcessor commandProcessor = new CommandProcessor();
            util.DisplayMessage("Hi There! Let's go for a walk!");
            commandProcessor.ReadCommand();
            commandProcessor.DisplayCommand();
            commandProcessor.ExecuteCommand();
            Console.ReadKey();
        }
    }
}
