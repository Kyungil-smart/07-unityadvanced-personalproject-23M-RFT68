using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 2 * 10f;
    
    public PoolManager poolManager;
    public Player player;

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime >= maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    void Awake()
    {
        instance = this;
    }
}
