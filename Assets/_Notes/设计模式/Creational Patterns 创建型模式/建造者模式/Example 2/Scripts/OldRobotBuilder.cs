namespace Yang.DesignPattern.Builder.Example2
{
    public class OldRobotBuilder : IRobotBuilder
    {
        public OldRobotBuilder()
        {
            robot = new Robot();
        }

        private Robot robot { get; }

        public Robot GeRobot()
        {
            return robot;
        }

        public void BuildRobotHead()
        {
            robot.SetRobotHead("Old head");
        }

        public void BuildRobotTorso()
        {
            robot.SetRobotTorso("Old torso");
        }

        public void BuildRobotArms()
        {
            robot.SetRobotArms("Old arms");
        }

        public void BuildRobotLegs()
        {
            robot.SetRobotLegs("Old legs");
        }
    }
}