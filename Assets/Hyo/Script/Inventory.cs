using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public int maxSlots = 6;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Debug.Log($"{i + 1}�� ���� ������ ��� �õ�");
                UseItem(i);
            }
        }
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
            return false;
        }

        items.Add(item);
        Debug.Log($"{item.itemName} �������� ȹ���߽��ϴ�.");
        UpdateInventoryUI();
        return true;
    }

    void UpdateInventoryUI() // ���⼭ UI ����
    {
        Debug.Log($"�κ��丮 ����: {string.Join(", ", items)}");
    }

    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            Debug.Log("�ش� ���Կ� �������� �����ϴ�.");
            return;
        }

        var item = items[slotIndex];
        Debug.Log($"{item.itemName} �������� ����߽��ϴ�.");
        items.RemoveAt(slotIndex);
        UpdateInventoryUI();
    }
}
