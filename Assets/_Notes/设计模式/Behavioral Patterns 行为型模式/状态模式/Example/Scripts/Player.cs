namespace Yang.DesignPattern.State.Example
{
    public class Player
    {
        private IPlayerBaseState _state;

        public Player()
        {
            _state = new StandState(this);
        }

        public void SetPlayerState(IPlayerBaseState newState)
        {
            _state = newState;
        }

        public void Update()
        {
            _state.HandleInput();
        }
    }
}