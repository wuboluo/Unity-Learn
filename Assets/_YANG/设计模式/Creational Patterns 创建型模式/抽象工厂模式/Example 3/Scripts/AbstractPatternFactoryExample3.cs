using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example3
{
    public class AbstractPatternFactoryExample3 : MonoBehaviour
    {
        private void Start()
        {
            // 美的厂家
            IFactory mdFactory = new MDFactory();
            // 美的冰箱、空调
            var mdFridge = mdFactory.GetFridge();
            var mdAirConditioner = mdFactory.GetAriConditioner();
            // 生产
            mdFridge.Produce();
            mdAirConditioner.Produce();

            // 格力厂家
            IFactory glFactory = new GLFactory();
            // 格力冰箱、空调
            var glFridge = glFactory.GetFridge();
            var glAirConditioner = glFactory.GetAriConditioner();
            // 生产
            glFridge.Produce();
            glAirConditioner.Produce();
        }
    }
}