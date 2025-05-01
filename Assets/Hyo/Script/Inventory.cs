using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 6;
    public List<Item> items = new List<Item>();
    public UnityEvent<string, int> OnUseItem;

    void Update()
    {
        // for (int i = 0; i < 6; i++)
        // {
        //     if (Input.GetKeyDown(KeyCode.Alpha1 + i))
        //     {
        //         Debug.Log($"{i + 1}�� ���� ������ ��� �õ�");
        //         UseItem(i);
        //     }
        // }
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
            return false;
        }

        items.Add(item);
        item.gameObject.SetActive(false);
        Debug.Log($"{item.ItemName} �������� ȹ���߽��ϴ�.");
        UpdateInventoryUI();
        return true;
    }

    void UpdateInventoryUI() // ���⼭ UI ����
    {
        if (items.Count == 0)
            return;
        
        Debug.Log($"�κ��丮 ����: {string.Join(", ", items)}");
    }

    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            Debug.Log("�ش� ���Կ� �������� �����ϴ�.");
            return;
        }

        if (items[slotIndex] is IUsable)
        {
            UsableItem item = items[slotIndex] as UsableItem;
            if (item == null)
                return;
            
            OnUseItem?.Invoke(item.ItemName, item.Value); 
            items.RemoveAt(slotIndex);
            item.Use();
            UpdateInventoryUI();
        }

    }
}
