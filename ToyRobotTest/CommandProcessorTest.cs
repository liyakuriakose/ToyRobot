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
        public void CommandProcessor_Right_Fail()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.IsPlaced = true;
            commandProcessor.FaceDirection = Direction.SOUTH;
            commandProcessor.Right();
            Assert.AreNotEqual(Direction.NORTH, commandProcessor.FaceDirection);
        }

        [TestMethod]
        public void CommandProcessor_Left_Success()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.IsPlaced = true;
            commandProcessor.FaceDirection = Direction.EAST;
            commandProcessor.Right();
            Assert.AreEqual(Direction.SOUTH, commandProcessor.FaceDirection);
        }

        [TestMethod]
        public void CommandProcessor_Left_Fail()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.IsPlaced = true;
            commandProcessor.FaceDirection = Direction.EAST;
            commandProcessor.Right();
            Assert.AreNotEqual(Direction.WEST, commandProcessor.FaceDirection);
        }

        [TestMethod]
        public void CommandProcessor_ExecuteCommand_Success()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.Commands = new string[] {"Place 0,1,North","Move","Right","Report"} ;
            commandProcessor.ExecuteCommand();
            Assert.AreEqual("0,2,EAST", commandProcessor.XPosRobot + "," + commandProcessor.YPosRobot + "," + commandProcessor.FaceDirection);
        }

        [TestMethod]
        public void CommandProcessor_ExecuteCommand_InvalidCommand()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.Commands = new string[] { "Rotate 0,1,North", "Move", "Right", "Report" };
            commandProcessor.ExecuteCommand();
            Assert.AreEqual(false, commandProcessor.IsPlaced);
        }

        [TestMethod]
        public void CommandProcessor_Move_InvalidPosition()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.XPosRobot = 4;
            commandProcessor.YPosRobot = 4;
            commandProcessor.FaceDirection = Direction.EAST;
            commandProcessor.Move();
            Assert.AreEqual("4,4,EAST", commandProcessor.XPosRobot + "," + commandProcessor.YPosRobot + "," + commandProcessor.FaceDirection);
        }

        [TestMethod]
        public void CommandProcessor_Move_Success()
        {
            CommandProcessor commandProcessor = new CommandProcessor();
            commandProcessor.XMax = 5;
            commandProcessor.YMax = 5;
            commandProcessor.XPosRobot = 3;
            commandProcessor.YPosRobot = 4;
            commandProcessor.FaceDirection = Direction.EAST;
            commandProcessor.Move();
            Assert.AreEqual("4,4,EAST", commandProcessor.XPosRobot + "," + commandProcessor.YPosRobot + "," + commandProcessor.FaceDirection);
        }

    }
}