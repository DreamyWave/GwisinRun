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
    
    //# ���� ����(20250503) -- ����
    private int m_selectedItemIndex;
    public int SelectedItemIndex => m_selectedItemIndex;

    void Update()
    {
        if (GameManager.Instance.IsPaused || GameManager.Instance.IsCleared || GameManager.Instance.IsGameOver)
            return;
        
        if (GameManager.Instance.Input.ItemsActionPressed)
        {
            m_selectedItemIndex = GameManager.Instance.Input.LastPressedKey;
            Debug.Log($"{m_selectedItemIndex+ 1} ��° ���� ����");
        }
    }
    //# ���� ����(20250503) -- ��

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

    //# ���� ����(20250503) -- ����
    public void UseItem()
    {
        if (m_selectedItemIndex < 0 || m_selectedItemIndex >= items.Count)
        {
            Debug.Log("�ش� ���Կ� �������� �����ϴ�.");
            return;
        }

        if (items[m_selectedItemIndex] is IUsable)
        {
            UsableItem item = items[m_selectedItemIndex] as UsableItem;
            if (item == null)
                return;
            
            OnUseItem?.Invoke(item.ItemName, item.Value); 
            items.RemoveAt(m_selectedItemIndex);
            item.Use();
            UpdateInventoryUI();
            Debug.Log($"{m_selectedItemIndex + 1} ���Կ� �������� ����մϴ�.");
        }
    }
    //# ���� ����(20250503) -- ��
}
