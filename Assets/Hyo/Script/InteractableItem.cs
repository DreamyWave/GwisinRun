using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public Item.ItemType itemType; // �ν����Ϳ��� ���� ����
    private string itemName;       // �ڵ����� ������

    public float interactDistance = 2f; // �÷��̾�� �Ÿ�
    private bool isMouseOver = false;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // �÷��̾� ��ġ ã��
        itemName = itemType.ToString();
    }

    void Update()
    {
        if (isMouseOver && Vector3.Distance(transform.position, player.position) <= interactDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Item newItem = new Item(itemName);
                newItem.type = itemType;

                bool added = Inventory.Instance.AddItem(newItem); // �κ��丮�� �߰�
                if (added)
                {
                    Destroy(gameObject); // ������ ����
                }
                else
                {
                    Debug.Log("�κ��丮�� ���� á���ϴ�.");
                }
            }
        }
    }

    void OnMouseEnter()
    {
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }
}

