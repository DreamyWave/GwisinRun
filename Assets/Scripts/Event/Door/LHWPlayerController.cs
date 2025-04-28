using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHWPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement = new Vector3(horizontal, 0, vertical).normalized;

        // �̵�
        transform.position += m_Movement * moveSpeed * Time.deltaTime;

        // ȸ��
        if (m_Movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(m_Movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
        Interact();
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("DoorPrefeb").GetComponent<DoorController>().Interact();
        }
    }
}
