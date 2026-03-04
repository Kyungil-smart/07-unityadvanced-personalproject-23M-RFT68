using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                Batch();
                break;
            default:
                break;
        }

    }

    void Batch()
    {
        for (int j = 0; j < count; j++)
        {
            Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
            bullet.parent = transform;
            
            Vector3 rotVec = Vector3.forward * 360 * j / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1); // 무한으로 관통하는 로직
        }
    }
}
