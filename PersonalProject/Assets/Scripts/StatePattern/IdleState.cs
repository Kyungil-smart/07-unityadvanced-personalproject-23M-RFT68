using UnityEngine;

public class IdleState : IState
{
    PlayerController _player;
    
    public IdleState(PlayerController player)
    {
        _player = player;
    }
    
    public void Enter()
    {
        
    }

    public void Update()
    {
        if (_player.MoveInput != 0)
        {
            
            
        }


    }

    public void Exit()
    {
        
    }
}
