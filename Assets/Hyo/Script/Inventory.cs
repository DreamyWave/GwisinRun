using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 6;
    public Item[] items;
    private int m_itemCount;
    
    
    public UnityEvent<string, int> OnUseItem;
    public UnityEvent<int> OnDropOrUseItem;
    public UnityEvent<int, Item> OnAddItem;
    public UnityEvent<int> OnSelectedItemChanged;
    
    //# ���� ����(20250503) -- ����
    private int m_selectedItemIndex;
    public int SelectedItemIndex => m_selectedItemIndex;

    private void Awake()
    {
        items = new Item[maxSlots];
    }

    void Update()
    {
        if (GameManager.Instance.IsPaused || GameManager.Instance.IsCleared || GameManager.Instance.IsGameOver)
            return;
        
        if (GameManager.Instance.Input.ItemsActionPressed)
        {
            m_selectedItemIndex = GameManager.Instance.Input.LastPressedKey;
            OnSelectedItemChanged?.Invoke(m_selectedItemIndex);
        }
    }
    //# ���� ����(20250503) -- ��

    public bool AddItem(Item item)
    {
        if (m_itemCount >= maxSlots)
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
            return false;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
                continue;
            
            items[i] = item;
            item.gameObject.SetActive(false);
            OnAddItem?.Invoke(i, items[i]);
            
            Debug.Log($"{item.ItemName} �������� ȹ���߽��ϴ�.");
            m_itemCount++;
            
            break;
        }
        
        return true;
    }

    //# ���� ����(20250503) -- ����
    public void UseItem()
    {
        if (m_selectedItemIndex < 0 || m_selectedItemIndex >= items.Length)
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
            OnDropOrUseItem?.Invoke(m_selectedItemIndex);
            
            items[m_selectedItemIndex] = null;
            item.Use();
            m_itemCount--;
            Debug.Log($"{m_selectedItemIndex + 1} ���Կ� �������� ����մϴ�.");
        }
    }

    public bool RemoveKey()
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i].ItemName == "Key")
            {
                OnDropOrUseItem?.Invoke(i);
                items[i] = null;
                Destroy(items[i].gameObject);
                m_itemCount--;
                return true;
            }
        }
        return false;
    }
    //# ���� ����(20250503) -- ��
}
