using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    public string itemName = "Key"; // ������ �̸�
    public float showInfoDelay = 1.5f; // �̸� ���̱���� �ɸ��� �ð�
    public float detectRadius = 3f; // �÷��̾� ���� �Ÿ�

    private bool isMouseOver = false;
    private bool isPlayerNearby = false;
    private Coroutine showInfoCoroutine;

    public GameObject namePanel; // UI �г� (Text ����)
    public Text nameText; // ������ �̸� ǥ���� �ؽ�Ʈ
    public Transform player; // �÷��̾� Ʈ������

    void Start()
    {
        namePanel.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        isPlayerNearby = distance <= detectRadius;
    }

    void OnMouseEnter()
    {
        isMouseOver = true;

        if (isPlayerNearby)
        {
            showInfoCoroutine = StartCoroutine(ShowItemInfoAfterDelay());
        }
    }

    void OnMouseExit()
    {
        isMouseOver = false;

        if (showInfoCoroutine != null)
        {
            StopCoroutine(showInfoCoroutine);
        }

        namePanel.SetActive(false);
    }

    IEnumerator ShowItemInfoAfterDelay()
    {
        yield return new WaitForSeconds(showInfoDelay);

        if (isMouseOver && isPlayerNearby)
        {
            namePanel.SetActive(true);
            nameText.text = itemName;
        }
    }
}

