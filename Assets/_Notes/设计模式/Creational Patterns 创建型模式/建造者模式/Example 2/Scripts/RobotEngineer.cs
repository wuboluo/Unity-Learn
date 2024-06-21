namespace Yang.DesignPattern.Builder.Example2
{
    public class RobotEngineer
    {
        public RobotEngineer(IRobotBuilder builder)
        {
            RobotBuilder = builder;
        }

        private IRobotBuilder RobotBuilder { get; }

        public Robot GetRobot()
        {
            return RobotBuilder.GeRobot();
        }

        public void MakeRobot()
        {
            RobotBuilder.BuildRobotHead();
            RobotBuilder.BuildRobotTorso();
            ;
            RobotBuilder.BuildRobotArms();
            RobotBuilder.BuildRobotLegs();
        }
    }
}