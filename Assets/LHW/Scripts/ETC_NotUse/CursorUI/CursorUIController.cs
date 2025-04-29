using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorUIController : MonoBehaviour
{
    [SerializeField] Transform m_cursor;

    void Update()
    {
        CursorMoving();
    }

    private void CursorMoving()
    { 
        // ���콺 Ŀ�� ��ġ ������
        float x = Input.mousePosition.x - (Screen.width / 2 ) + 40;
        float y = Input.mousePosition.y - (Screen.height / 2 ) - 55;
        m_cursor.localPosition = new Vector2(x, y);
       

        // ���콺�� Ƣ����� �ʵ��� ��
        float tempCursorPosX = m_cursor.localPosition.x;
        float tempCursorPosY = m_cursor.localPosition.y;

        float tempCursorMinWidth = -Screen.width / 2;
        float tempCursorMaxWidth = Screen.width / 2;
        float tempCursorMinHeight = -Screen.height / 2;
        float tempCursourMaxHeight = Screen.height / 2;

        // Ƣ����� ���� ������
        int padding = 20;

        tempCursorPosX = Mathf.Clamp(tempCursorPosX, tempCursorMinWidth + padding, tempCursorMaxWidth - padding);
        tempCursorPosY = Mathf.Clamp(tempCursorPosY, tempCursorMinHeight + padding, tempCursourMaxHeight - padding);

        m_cursor.localPosition = new Vector2(tempCursorPosX, tempCursorPosY);
    }
}
