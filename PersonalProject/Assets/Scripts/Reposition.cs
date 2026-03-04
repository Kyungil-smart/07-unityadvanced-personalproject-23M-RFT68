using System;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;
        
        Vector3 PlayerPos = GameManager.instance.player.transform.position;
        Vector3 MyPos = transform.position;
        
        float diffX = MathF.Abs(PlayerPos.x - MyPos.x);
        float diffY = MathF.Abs(PlayerPos.y - MyPos.y);

        Vector3 PlayerDir = GameManager.instance.player.inputVec;
        float dirX = PlayerDir.x < 0 ? -1 : 1;
        float dirY = PlayerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                
                break;
        }
    }
}
