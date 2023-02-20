using UnityEngine;

namespace Yang.DesignPattern.State.Example
{
    public class AttackState : IPlayerBaseState
    {
        private readonly Player _player;

        public AttackState(Player player)
        {
            _player = player;
            Debug.Log("-------------------- Player 进入攻击状态");
        }
        
        public void Update()
        {
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _player.SetPlayerState(new JumpState(_player));
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                _player.SetPlayerState(new DefendState(_player));
            }
        }
    }
}