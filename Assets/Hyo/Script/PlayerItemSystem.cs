using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemSystem : MonoBehaviour
{
    // ������ ȹ��
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemPickup pickup = other.GetComponent<ItemPickup>();
            if (pickup == null) return;

            bool added = false;

            Item newItem = new Item(pickup.type.ToString());
            newItem.type = pickup.type;
            newItem.value = pickup.value;

            switch (pickup.type)
            {
                case Item.ItemType.Key:
                case Item.ItemType.HeartPotion:
                case Item.ItemType.SpeedPotion:
                case Item.ItemType.Repellent:
                    added = Inventory.Instance.AddItem(newItem);
                    break;
                    // �ʿ��ϸ� ���⼭ �� �߰�
            }

            if (added)
            {
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("�κ��丮�� ���� á���ϴ�.");
            }
        }
    }

}
public class ItemUser : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inventory.Instance.UseItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Inventory.Instance.UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Inventory.Instance.UseItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Inventory.Instance.UseItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Inventory.Instance.UseItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Inventory.Instance.UseItem(5);
        }
    }
}
