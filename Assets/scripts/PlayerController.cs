using UnityEngine;

// 1. 점프, 뛰기 : 위로 속도를 갖게한다. => RigidBody의 속도를 설정한다.
// 2. 애니메이션 제어 : 애니메이터에게 정확한 정보를 주면 => 그에 따라 플레이할 클립이 정해진다.
// 3. 죽음 처리 : 


public class PlayerController : MonoBehaviour
{
   [Header("점프 기능")]     // 유니티에서 섹션 나눠줌
   [SerializeField] 
   private Rigidbody2D rigid;
   [SerializeField] [Range(0, 20f)] float jumpSpeed;
   [SerializeField] 
   private AudioSource jumpSound;
   [SerializeField] 
   private bool isGrounded;  
   [SerializeField] 
   private int jumpCount;  
    [SerializeField] 
   private Animator animator;

   [Header("게임 오버")]
   [SerializeField] 
   private AudioSource dieSound;

   private GameManager gameManager;

   private void Awake()     // 시작하기 전부터 활성화
   {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
   }

   private void Start()
   {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
   }

   private void Update()
   {
        if (Input.GetMouseButtonDown(0) && jumpCount <= 1)      // 점프 버튼을 누르고 더블점프가 아닐때
        {
            Jump();
        }
   }

    private void Jump()
    {
        rigid.linearVelocityY = jumpSpeed;      // 위로 스피드 주기
        jumpSound.Play();       // 점프 사운드 플레이하고
        jumpCount++;        // 점프카운트 증가(더블점프 용)
    }

    private void Die()
    {
        animator.SetTrigger("Die");     // 죽는 애니메이션 실행
        // rigid.linearVelocity = Vector2.zero;
        rigid.simulated = false;
        dieSound.Play();        // 죽는 사운드 플레이

        gameManager.OnPlayerDead();
    }

    private void OnCollisionEnter2D(Collision2D collision)      // 닿았을때
        {            
            if (collision.collider.tag == "Platform" &&
                collision.contacts[0].normal.y > 0.7f)   // 닿은게 플랫폼이고 충돌한 부분이 윗면인지
            {
                isGrounded = true;      // 닿았음 트루
                jumpCount = 0;          // 점프카운트 0으로 초기화 => 다시 점프 가능
                animator.SetBool("Grounded", true);     // 애니메이터에 있는 트리거 발동
            }
            
            
        }

    private void OnCollisionExit2D(Collision2D collision)       // 떨어졌을 때
    {       
        if (collision.collider.tag == "Platform")       // 떨어진게 플랫폼일때
        {
            isGrounded = false;     // 닿았음 X
            animator.SetBool("Grounded", false);         // 그냥 땅을 걸어가다가 떨어졌을때도 점프모션재생
        } 
              
    }

    private void OnTriggerEnter2D(Collider2D collision) // 장애물에 닿았을때 
    {
        if (collision.tag == "Dead")    // Dead 태그 붙은거 (데드존이랑 장애물)
        {
            Die();
        }
    }
}
