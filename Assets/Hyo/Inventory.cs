using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public int maxSlots = 6;
    public List<string> items = new List<string>();

    public bool AddItem(Item newItem)
    {
        if (items.Count >= maxSlots)
        {
            return false; // �κ��丮 �� á���� �߰� ����
        }

        items.Add(newItem.itemName);
        UpdateInventoryUI();
        return true;
    }

    void UpdateInventoryUI() // ���⿡ UI �ڵ� ������ ��
    {
        Debug.Log("�κ��丮 ����: " + string.Join(", ", items));
    }

    public void UseItem(int slot)
    {
        if (slot < 0 || slot >= items.Count)
            return;

        Debug.Log("������ ���: " + items[slot]);
        // ���⿡ ���� ������ ��� ���� ����
        items.RemoveAt(slot);
        UpdateInventoryUI();
    }

}
