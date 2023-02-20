using UnityEngine;

namespace Yang.DesignPattern.State.Example
{
    public class StandState : IPlayerBaseState
    {
        private Player _player;

        public StandState(Player player)
        {
            _player = player;
            Debug.Log("-------------------- Player 站立");
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

            if (Input.GetKeyDown(KeyCode.A))
            {
                _player.SetPlayerState(new AttackState(_player));
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                _player.SetPlayerState(new DefendState(_player));
            }
        }
    }
}