using System;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

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
                if (_collider2D.enabled)
                {
                    transform.Translate(PlayerDir * 20 + new Vector3(UnityEngine.Random.Range(-3f, 3f),UnityEngine.Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
