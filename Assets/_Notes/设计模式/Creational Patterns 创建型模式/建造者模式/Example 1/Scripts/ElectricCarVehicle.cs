namespace Yang.DesignPattern.Builder.Example1
{
    // 电动车
    public class ElectricCarVehicle : VehicleBuilder
    {
        public ElectricCarVehicle()
        {
            _vehicle = new Vehicle("ElectricCar");
        }

        public override void BuildFrame()
        {
            _vehicle["frame"] = "ElectricCar Frame";
        }

        public override void BuildEngine()
        {
            _vehicle["engine"] = "50 cc";
        }

        public override void BuildWheels()
        {
            _vehicle["wheels"] = "2";
        }

        public override void BuildDoors()
        {
            _vehicle["doors"] = "0";
        }
    }
}