using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobot;

namespace ToyRobotTest
{
    [TestClass]
    public class CommandProcessorTest
    {
        [TestMethod]
        public void CommandProcessor_PlaceRobot_Success()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.Place("PLACE 1,1,NORTH");
            bool isPlaced = commandProcessor.IsPlaced;
            Assert.AreEqual(true, isPlaced);
        }

        [TestMethod]
        public void CommandProcessor_PlaceRobot_Fail()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.Place("PLACE 5,1,NORTH");
            bool isPlaced = commandProcessor.IsPlaced;
            Assert.AreEqual(false, isPlaced);
        }

        [TestMethod]
        public void CommandProcessor_Right_Success()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.IsPlaced = true;
            commandProcessor.FaceDirection = Direction.SOUTH;
            commandProcessor.Right();
            Assert.AreEqual(Direction.WEST, commandProcessor.FaceDirection);
        }

        [TestMethod]
        public void CommandProcessor_ExecuteCommand()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.Commands = new string[] {"Place 0,1,North","Move","Right","Report"} ;
            commandProcessor.ExecuteCommand();
            Assert.AreEqual("0,2,EAST", commandProcessor.XPosRobot + "," + commandProcessor.YPosRobot + "," + commandProcessor.FaceDirection);

        }

    }
}