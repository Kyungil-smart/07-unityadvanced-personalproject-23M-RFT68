using UnityEngine;

public class DeadState: IState
{
    PlayerController _player;
    
    public DeadState(PlayerController player)
    {
        _player = player;
    }
    public void Enter()
    {
        
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }
}
