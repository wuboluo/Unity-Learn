namespace Yang.DesignPattern.Builder.Example1
{
    // 交通工具图纸
    public abstract class VehicleBuilder
    {
        protected Vehicle _vehicle;

        public Vehicle Vehicle => _vehicle;

        // 建造框架
        public abstract void BuildFrame();
        // 建造引擎
        public abstract void BuildEngine();
        // 建造轮子
        public abstract void BuildWheels();
        // 建造门
        public abstract void BuildDoors();
    }
}