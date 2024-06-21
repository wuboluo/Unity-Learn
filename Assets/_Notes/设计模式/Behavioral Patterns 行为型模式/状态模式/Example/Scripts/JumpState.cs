using UnityEngine;

namespace Yang.DesignPattern.State.Example
{
    public class JumpState : IPlayerBaseState
    {
        private readonly Player _player;

        public JumpState(Player player)
        {
            _player = player;
            Debug.Log("-------------------- Player 跳跃");
        }

        public void Update()
        {
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) _player.SetPlayerState(new StandState(_player));
        }
    }
}