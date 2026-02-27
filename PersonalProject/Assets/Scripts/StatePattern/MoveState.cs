using UnityEngine;

public class MoveState: IState
{
    PlayerController _player;
    
    public MoveState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.SetSpeed(1f);
    }

    public void Update()
    {
        float move = _player.MoveInput;
        _player.SetSpeed(move);
        
        if (_player.MoveInput == 0)
        {
            _player.ChangeState(_player.Idle);
        }
    }

    public void Exit()
    {
        
    }
}
