using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsLockedDoor : MonoBehaviour
{
    private bool isLocked = true;

    public bool IsLocked()
    {
        if(isLocked == false)
        {
            return isLocked;
        }
        TryOpen();
        return isLocked;
    }

    private void TryOpen()
    {
        // RemoveKey()�� �����ͼ� �̰� True�� ������, �ƴϸ� �ȿ���
        // if(GameManager.Instance.Inventory.RemoveKey())
        // { islocked = false}
    }
}
