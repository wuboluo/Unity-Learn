using UnityEngine;

namespace Yang.DesignPattern.State.Example
{
    public class DefendState : IPlayerBaseState
    {
        private readonly Player _player;

        public DefendState(Player player)
        {
            _player = player;
            Debug.Log("-------------------- Player 进入防御状态");
        }

        public void Update()
        {
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _player.SetPlayerState(new StandState(_player));
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                _player.SetPlayerState(new AttackState(_player));
            }
        }
    }
}