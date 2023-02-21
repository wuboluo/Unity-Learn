using UnityEngine;

namespace Yang.DesignPattern.Builder.Example1
{
    public class BuilderPatternExample1 : MonoBehaviour
    {
        private VehicleBuilder _builder;

        private void Start()
        {
            // 一个汽车图纸
            _builder = new CarVehicle();
            // 车厂根据图纸生产汽车
            Shop.Construct(_builder);
            // 图纸车辆信息展示
            _builder.Vehicle.Show();
            
            _builder = new MotorCycleBuilder();
            Shop.Construct(_builder);
            _builder.Vehicle.Show();
            
            _builder = new ElectricCarVehicle();
            Shop.Construct(_builder);
            _builder.Vehicle.Show();
        }
    }
}