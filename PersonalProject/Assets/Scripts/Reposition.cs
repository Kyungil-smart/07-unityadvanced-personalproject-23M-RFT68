using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

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
        
        switch (transform.tag)
        {
            case "Ground":
                float diffX = (PlayerPos.x - MyPos.x);
                float diffY = (PlayerPos.y - MyPos.y);
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
                
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
                    Vector3 dist = PlayerPos - MyPos;
                    Vector3 ran = new Vector3(UnityEngine.Random.Range(-3,3), UnityEngine.Random.Range(-3,3), 0);
                    transform.Translate(ran + dist * 2);
                }
                break;
        }
    }
}
