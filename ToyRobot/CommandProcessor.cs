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
            try
            {
                //Getting the table size from config file
                Table table = new Table();
                table.GetTableSize(out XMax, out YMax);
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
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
                    DisplayCommand();
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
                    util.DisplayMessage("==========================================\n");
                    foreach (string command in Commands)
                    {
                        util.DisplayMessage(command);
                    }
                    util.DisplayMessage("==========================================");
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
            try
            {
                if (Commands != null)
                {
                    if (Commands.Count() > 0)
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
                                    util.DisplayMessage($"The command '{command}' was ignored as is not valid!!");
                                }
                            }
                        }
                        if (!IsPlaced)
                        {
                            util.DisplayMessage("Please place Robot at a valid position to start traversing!!");
                        }
                    }
                    else
                    {
                        util.DisplayMessage("Command File Empty!! Please enter a commmand to Execute!!");
                    }
                }
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        //Report the position (X,Y,F) of the Robot
        public void Report()
        {
            try
            {
                util.DisplayMessage($"\tRobot is at: {XPosRobot},{YPosRobot},{FaceDirection}");
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        //Turn left of the facing direction
        public void Left()
        {
            try
            {
                int value = (int)FaceDirection;
                //Moving the Robot in the circular direction
                var name = Enum.GetName(typeof(Direction), (--value + NumberOfDirections) % NumberOfDirections);
                FaceDirection = (Direction)Enum.Parse(typeof(Direction), name);
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        //Turn right of the face direction
        public void Right()
        {
            try
            {
                int value = (int)FaceDirection;
                var name = Enum.GetName(typeof(Direction), (++value) % NumberOfDirections);
                FaceDirection = (Direction)Enum.Parse(typeof(Direction), name);
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        //Move the robot one step ahead in the direction of the face direction
        public void Move()
        {
            try
            {
                int xPosBefore = XPosRobot;
                int yPosBefore = YPosRobot;
                switch (FaceDirection)
                {
                    case Direction.NORTH:
                        YPosRobot++;
                        break;
                    case Direction.SOUTH:
                        YPosRobot--;
                        break;
                    case Direction.WEST:
                        XPosRobot--;
                        break;
                    case Direction.EAST:
                        XPosRobot++;
                        break;
                    default:
                        break;
                }
                if (!IsPositionValid())
                {
                    XPosRobot = xPosBefore;
                    YPosRobot = yPosBefore;
                    //util.DisplayMessage("Couldn't move Robot!! Position out of bounds!!!");
                }
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        //Placing the robot at (X,Y,F)
        public void Place(string command)
        {
            try
            {
                string[] placeCommand = command.Split(new Char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (placeCommand != null && placeCommand.Count() == 4)
                {
                    int xPosBefore = XPosRobot;
                    int yPosBefore = YPosRobot;
                    Direction faceDirection = FaceDirection;
                    XPosRobot = Convert.ToInt32(placeCommand[1].Trim());
                    YPosRobot = Convert.ToInt32(placeCommand[2].Trim());
                    FaceDirection = (Direction)Enum.Parse(typeof(Direction), placeCommand[3].Trim().ToUpper());
                    if (!IsPositionValid())
                    {
                        XPosRobot = xPosBefore;
                        YPosRobot = yPosBefore;
                        FaceDirection = faceDirection;
                        util.DisplayMessage("Couldn't place the Robot in the position!! Position out of bounds!!!");
                    }
                    else
                    {
                        //placed the robot
                        IsPlaced = true;
                    }
                }
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
            }
        }

        //checking whether the placed or moved position is valid
        private bool IsPositionValid()
        {
            try
            {
                if ((XMax > XPosRobot && XPosRobot >= 0) && (YMax > YPosRobot && YPosRobot >= 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                util.DisplayMessage("Sorry! An unexpected error occured!");
                return false;
            }
        }

    }
}
