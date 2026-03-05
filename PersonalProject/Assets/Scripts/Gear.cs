using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;
        
        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }
    
    // 레벨업 로직
    public void LevelUp(float rate)
    {
        this.rate = rate;    
        ApplyGear();
    }
    
    // 만든 함수들 불러주는 역할
    private void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    // 연사력 증가 로직
    private void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    // 이속 증가 로직
    private void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.Speed = speed + speed * rate;
    }

}
