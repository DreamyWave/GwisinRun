using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    [Header("Speed Settings")]
    // 걷기 속도
    public static float m_walkSpeed = 5f;
    // 뛰기 속도
    private float m_runSpeed = 10f;
    // 현재 속도를 걷기나 뛰기 속도를 저장
    public static float m_speed;
    // 떨어지는 속도
    private float m_fallSpeed = -28.0f;
    // 전체적인 이동 속도 계산
    private Vector3 m_velocity;
    
    [Header("Player Sit Setting")]
    private float m_sitHeight = 1.0f;
    // 캐릭터 원래 높이
    [SerializeField] private float m_originalHeight;
    // 캐릭터 원래 중심 위치
    private Vector3 m_originalCenter;
    // 프레이어 크기 저장
    private Vector3 m_originalPlayerScale;
    // 앉은 상태의 스케일
    private Vector3 m_sitPlayerScale;
    // 앉은 상태인지 확인
    public static bool IsSit;

    [Header("Jump Settings")]
    // 점프 높이
    private float m_jumpHeight = 1.2f;


    void Start()
    {
        
        PlayerController.PlayerTransform = transform;
        if (PlayerController.PlayerTransform == null)
        {
            Debug.LogError("PlayerTransform이 초기화되지 않았습니다!");
            return;
        }
        
        // m_originalHeight = PlayerController.PlayerCont.height;
        // 일어났을때 위치
        m_originalHeight = PlayerController.PlayerCont.height;
        // 중심 위치
        m_originalCenter = PlayerController.PlayerCont.center;

        m_originalPlayerScale = PlayerController.PlayerTransform.localScale;
        m_sitPlayerScale = new Vector3(m_originalPlayerScale.x, m_originalPlayerScale.y * 0.5f, m_originalPlayerScale.z);

    }

    void Update()
    {
        //# 수정 사항(20250502) -- 시작
        if (GameManager.Instance.IsPaused || GameManager.Instance.IsCleared || GameManager.Instance.IsGameOver)
        {
            PlayerController.PlayerCont.Move(Vector3.zero);
            return;
        }
        //# 수정 사항(20250502) -- 끝
        Move();
        Jump();
        SitPlayer();
    }

    void Move()
    {
        Vector2 moveInput = GameManager.Instance.Input.MoveInput;
        float moveX = moveInput.x;
        float moveZ = moveInput.y;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (GameManager.Instance.Input.RunKeyBeingHeld)
        {
            m_speed = m_runSpeed;
            if (moveX != 0 || moveZ != 0) PlayerHealth.StaminaMinus();
            else PlayerHealth.StaminaPlus();
        }
        else
        {
            m_speed = m_walkSpeed;
            PlayerHealth.StaminaPlus();
        }

        PlayerController.PlayerCont.Move( m_speed * Time.deltaTime * move);
        
        m_velocity.y += m_fallSpeed * Time.deltaTime;
        PlayerController.PlayerCont.Move(m_velocity * Time.deltaTime);

        if (PlayerController.PlayerCont.isGrounded && m_velocity.y < 0)
            m_velocity.y = -2f;
    }

    void Jump()
    {
        if (GameManager.Instance.Input.JumpKeyPressed && PlayerController.PlayerCont.isGrounded)
        {
            m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_fallSpeed);
        }
    }

    void SitPlayer()
    {
        // 앉기 키가 눌려있는지 확인
        bool sitKeyHeld = GameManager.Instance.Input.SitKeyBeingHeld;
        
        // 키가 눌려 있거나
        if (sitKeyHeld)
        {
            IsSit = true;
        }
        // 키가 안눌려있으면
        else 
        {
            IsSit = false;
        }

        // 목표 높이와 중심 위치, 스케일 설정
        float targetHeight = IsSit ? m_sitHeight : m_originalHeight;
        
        // 캐릭터 앉아 있는 상태 계산 아니면 일어서기
        Vector3 targetCenter = IsSit ? new Vector3(0, m_originalCenter.y - (m_originalHeight - m_sitHeight) / 6f , 0) : m_originalCenter;

        // 캐릭터 크기
        Vector3 targetScale = IsSit ? m_sitPlayerScale : m_originalPlayerScale;

        // Lerp를 통해 부드럽게 보간 처리 (Time.deltaTime * 10f → 속도 조절용)
        PlayerController.PlayerCont.height = Mathf.Lerp(PlayerController.PlayerCont.height, targetHeight, Time.deltaTime * 10f);
        PlayerController.PlayerCont.center = Vector3.Lerp(PlayerController.PlayerCont.center, targetCenter, Time.deltaTime * 10f);
        PlayerController.PlayerTransform.localScale = Vector3.Lerp(PlayerController.PlayerTransform.localScale, targetScale, Time.deltaTime * 10f);
    }

}
