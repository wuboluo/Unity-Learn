using UnityEngine;

namespace Yang.DesignPattern.Builder.Example2
{
    public class BuilderPatternExample2 : MonoBehaviour
    {
        private void Start()
        {
            IRobotBuilder oldRobot = new OldRobotBuilder();
            RobotEngineer engineer = new RobotEngineer(oldRobot);
            engineer.MakeRobot();

            Robot firstRobot = engineer.GetRobot();
            Debug.Log("First robot built");
            Debug.Log(firstRobot.ToStringEX());
        }
    }
}