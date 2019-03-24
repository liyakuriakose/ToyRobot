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
            
            try
            {
                util.DisplayMessage("\n Hi There! Let's go for a walk!!!" +
                    "\n Please enter the commands in the command text file." +
                    "\n PLACE X,Y,F \n MOVE \n LEFT \n RIGHT\n REPORT");
                Init();
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        static void Init()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.ReadCommand();
            commandProcessor.DisplayCommand();
            commandProcessor.ExecuteCommand();
            ContinueConfirmation();
        }

        static void ContinueConfirmation()
        {
            try
            {
                Util util = new Util();
                util.DisplayMessage("Do you wish to continue?(Y/N)");
                string strContinue = Console.ReadLine().ToString();
                if (strContinue.ToLower().Equals("y"))
                {
                    Init();
                }
                else
                {
                    util.DisplayMessage("Thank you for using the application!");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry! An unexpected error occured!");
            }
        }

    }
}
