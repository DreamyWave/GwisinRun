using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] Animator m_doorAnimator;
    private bool m_isOpen = false;
    private bool m_isOpen2 = false;
    private bool m_close = true;
    [SerializeField] DoorTrigger1 m_doortrigger1;
    [SerializeField] DoorTrigger2 m_doortrigger2;
    [SerializeField] IsLockedDoor m_isLockedDoor;
    private bool m_isClosed = true;

    public void Awake()
    {
        m_doorAnimator.SetBool("Close", true);
    }

    public void Update()
    {
        MonsterInteract();
    }

    // ���� ��ȣ�ۿ�(�ٰ����� �˾Ƽ� �� ����)
    public void MonsterInteract()
    {
        // ������� ���� ���̰ų� ����� Ǯ���� ��
        if (m_isLockedDoor == null || !m_isLockedDoor.IsLocked())
        {
            m_isClosed = m_doorAnimator.GetBool("Close");
            if (m_doortrigger1.MonsterDetected() && m_isClosed)
            {
                GameManager.Instance.Audio.PlaySound(SoundType.DoorOpen);
                gameObject.layer = 0;
                OpenDoorCounterClockwise();
            }
            else if (m_doortrigger2.MonsterDetected() && m_isClosed)
            {
                GameManager.Instance.Audio.PlaySound(SoundType.DoorOpen);
                gameObject.layer = 0;
                OpenDoorClockwise();
            }
            else if (m_doortrigger1.MonsterDetected() || m_doortrigger2.MonsterDetected())
            {
                return;
            }
        }
    }

    // ĳ���� ��ȣ�ۿ�
    public void Interact()
    {
        // ������� ���� ���̰ų� ����� Ǯ���� ��
        if (m_isLockedDoor == null || !m_isLockedDoor.IsLocked(true))
        {
            if (m_close == true && m_doortrigger1.PlayerDetected())
            {
                GameManager.Instance.Audio.PlaySound(SoundType.DoorOpen);
                gameObject.layer = 0;
                OpenDoorCounterClockwise();
            }
            else if (m_close == true && m_doortrigger2.PlayerDetected())
            {
                GameManager.Instance.Audio.PlaySound(SoundType.DoorOpen);
                gameObject.layer = 0;
                OpenDoorClockwise();
            }
            else if (m_close == false)
            {
                CloseDoor();
                gameObject.layer = 7;
                StartCoroutine(CloseDoorCoroutine());
            }
            else
            {
                return;
            }
        }
    }

    IEnumerator CloseDoorCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        GameManager.Instance.Audio.PlaySound(SoundType.DoorClose);
    }

    // �ִϸ����� ó�� �κ�
    private void OpenDoorClockwise()
    {
        m_isOpen2 = true;
        m_close = false;
        m_doorAnimator.SetBool("IsOpen2", m_isOpen2);
        m_doorAnimator.SetBool("Close", m_close);
    }

    private void OpenDoorCounterClockwise()
    {
        m_isOpen = true;
        m_close = false;
        m_doorAnimator.SetBool("IsOpen", m_isOpen);
        m_doorAnimator.SetBool("Close", m_close);
    }
    private void CloseDoor()
    {
        m_isOpen = false;
        m_isOpen2 = false;
        m_close = true;
        m_doorAnimator.SetBool("IsOpen", m_isOpen);
        m_doorAnimator.SetBool("IsOpen2", m_isOpen2);
        m_doorAnimator.SetBool("Close", m_close);
    }
}
