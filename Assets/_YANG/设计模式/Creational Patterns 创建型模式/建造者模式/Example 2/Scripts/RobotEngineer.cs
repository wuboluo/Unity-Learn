namespace Yang.DesignPattern.Builder.Example2
{
    public class RobotEngineer
    {
        private IRobotBuilder RobotBuilder { get; set; }

        public RobotEngineer(IRobotBuilder builder)
        {
            RobotBuilder = builder;
        }

        public Robot GetRobot()
        {
            return RobotBuilder.GeRobot();
        }

        public void MakeRobot()
        {
            RobotBuilder.BuildRobotHead();
            RobotBuilder.BuildRobotTorso();;
            RobotBuilder.BuildRobotArms();
            RobotBuilder.BuildRobotLegs();
        }
    }
}