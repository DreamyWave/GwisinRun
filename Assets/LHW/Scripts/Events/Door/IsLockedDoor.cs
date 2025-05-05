using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsLockedDoor : MonoBehaviour
{
    private bool isLocked = true;

    public bool IsLocked(bool isPlayer = false)
    {
        if(isLocked == false)
        {
            return isLocked;
        }
        
        if(isPlayer == true)
            TryOpen();
        return isLocked;
    }

    private void TryOpen()
    {
        // RemoveKey()�� �����ͼ� �̰� True�� ������, �ƴϸ� �ȿ���
        if (GameManager.Instance.Inventory.RemoveKey())
        {
            isLocked = false;
        }
    }
}
