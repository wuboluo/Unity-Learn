namespace Yang.DesignPattern.Builder.Example1
{
    public static class Shop
    {
        // 建造
        public static void Construct(VehicleBuilder vehicleBuilder)
        {
            vehicleBuilder.BuildFrame();
            vehicleBuilder.BuildEngine();
            vehicleBuilder.BuildWheels();
            vehicleBuilder.BuildDoors();
        }
    }
}