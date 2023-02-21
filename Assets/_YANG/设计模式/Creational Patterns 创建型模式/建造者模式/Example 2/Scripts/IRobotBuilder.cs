namespace Yang.DesignPattern.Builder.Example2
{
    public interface IRobotBuilder
    {
        Robot GeRobot();

        void BuildRobotHead();
        void BuildRobotTorso();
        void BuildRobotArms();
        void BuildRobotLegs();
    }
}