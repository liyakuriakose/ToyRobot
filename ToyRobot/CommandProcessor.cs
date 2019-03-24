using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
    // enum for avaialble Directions for Toy Robot
    public enum Direction
    {
        EAST,
        SOUTH,
        WEST,
        NORTH
    }
    public class CommandProcessor
    {
        Util util = new Util();
        public string[] Commands { get; set; }
        public int XMax = 0;
        public int YMax = 0;
        public bool IsPlaced = false;
        public int XPosRobot = 0;
        public int YPosRobot = 0;
        public  Direction FaceDirection;
        public int NumberOfDirections = Enum.GetValues(typeof( Direction)).Length;

        public CommandProcessor()
        {
            Table table = new Table();
            table.GetTableSize(out XMax, out YMax);
        }

        //Reads the command from the ToyRobotCommand Text file
        public void ReadCommand()
        {
            try
            {
                string txtCommandFile = ConfigurationManager.AppSettings["CommandFile"];
                if (!String.IsNullOrEmpty(txtCommandFile) && File.Exists(txtCommandFile))
                {
                    Commands = File.ReadAllLines(txtCommandFile);
                }
                else
                {
                    util.DisplayMessage("The command file doesn't exist!");
                }
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        //Displays the command being executed
        public void DisplayCommand()
        {
            try
            {
                if (Commands != null && Commands.Count() > 0)
                {
                    util.DisplayMessage("\t");
                    util.DisplayMessage("I'm moving with the following instructions:");
                    util.DisplayMessage("========================================================");
                    foreach (string command in Commands)
                    {
                        util.DisplayMessage("\t" + command);
                    }
                    util.DisplayMessage("========================================================");
                    util.DisplayMessage("\t");
                }
                else
                {
                    util.DisplayMessage("Sorry! There are no commands in the command file!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry! An unexpected error occured!");
            }
        }

        //Parses the command and executes it
        public void ExecuteCommand()
        {
            foreach (string command in Commands)
            {

                if (command.Trim().ToUpper().StartsWith("PLACE"))
                {
                    Place(command);
                }
                else if (IsPlaced)
                {
                    if (command.Trim().ToUpper().Equals("MOVE"))
                    {
                        Move();
                    }
                    else if (command.Trim().ToUpper().Equals("LEFT"))
                    {
                        Left();
                    }
                    else if (command.Trim().ToUpper().Equals("RIGHT"))
                    {
                        Right();

                    }
                    else if (command.Trim().ToUpper().Equals("REPORT"))
                    {
                        Report();
                    }
                    else
                    {
                        util.DisplayMessage($"The command {0} was ignored as is not valid!!, command");
                    }
                }
                else
                {
                    util.DisplayMessage("Please place Robot at a valid position to start traversing!!");
                }
            }
        }
        private void Report()
        {
            //util.DisplayMessage
            // Console.WriteLine
            Console.WriteLine("{0},{1},{2}", XPosRobot, YPosRobot, FaceDirection);
        }

        private void Left()
        {
            int value = (int)FaceDirection;
            var name = Enum.GetName(typeof( Direction), (--value + NumberOfDirections) % NumberOfDirections);
            FaceDirection = ( Direction)Enum.Parse(typeof( Direction), name);
        }

        private void Right()
        {
            int value = (int)FaceDirection;
            var name = Enum.GetName(typeof( Direction), (++value) % NumberOfDirections);
            FaceDirection = ( Direction)Enum.Parse(typeof( Direction), name);
        }


        private void Move()
        {
            int xPosBefore = XPosRobot;
            int yPosBefore = YPosRobot;
            switch (FaceDirection)
            {
                case  Direction.NORTH:
                    YPosRobot++;
                    break;
                case  Direction.SOUTH:
                    YPosRobot--;
                    break;
                case  Direction.WEST:
                    XPosRobot--;
                    break;
                case  Direction.EAST:
                    XPosRobot++;
                    break;
                default:
                    break;
            }
            if (!IsPositionValid())
            {
                XPosRobot = xPosBefore;
                YPosRobot = yPosBefore;
                util.DisplayMessage("Couldn't move Robot!! Position out of bounds!!!");
            }
        }

        private void Place(string command)
        {
            try
            {
                string[] placeCommand = command.Split(new Char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (placeCommand != null && placeCommand.Count() == 4)
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        int xPosBefore = XPosRobot;
                        int yPosBefore = YPosRobot;
                         Direction faceDirection = FaceDirection;
                        XPosRobot = Convert.ToInt32(placeCommand[1].Trim());
                        YPosRobot = Convert.ToInt32(placeCommand[2].Trim());
                        
                        FaceDirection = ( Direction)Enum.Parse(typeof( Direction), placeCommand[3].Trim().ToUpper());
                        if (!IsPositionValid())
                        {
                            XPosRobot = xPosBefore;
                            YPosRobot = yPosBefore;
                            FaceDirection = faceDirection;
                            util.DisplayMessage("Couldn't place the Robot in the position!! Position out of bounds!!!");
                        }
                        else
                        {
                            IsPlaced = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        private bool IsPositionValid()
        {
            if ((xMax > XPosRobot && XPosRobot >=0) && (yMax > YPosRobot && YPosRobot>=0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
