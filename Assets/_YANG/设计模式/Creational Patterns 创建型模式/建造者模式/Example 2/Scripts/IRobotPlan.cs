namespace Yang.DesignPattern.Builder.Example2
{
    // 机器人图纸抽象接口
    public interface IRobotPlan
    {
        // 头 躯干 胳膊 腿
        void SetRobotHead(string head);
        void SetRobotTorso(string torso);
        void SetRobotArms(string arms);
        void SetRobotLegs(string legs);
    }
}