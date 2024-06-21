using UnityEngine;

namespace Yang.DesignPattern.State.Example
{
    public class StatePatternExample : MonoBehaviour
    {
        private Player _player;

        private void Start()
        {
            _player = new Player();
        }

        private void Update()
        {
            _player.Update();
        }
    }
}