using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;

    public float obstaclesRatio = 0.3f;

    private bool stepped = false;

    private GameManager gameManager;

    private int score = 10;

    private void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }

   private void OnEnable()
   {
        stepped = false;

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(Random.value < obstaclesRatio);      // 같은 코드임 두 개가
            // obstacle.SetActive(Random.Range(0f, 1f) < obstaclesRatio);
        }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
    if ( collision.collider.CompareTag("Player") && !stepped)
    {
        stepped = true;
        gameManager.AddScore(score);
    }
   }

  


}
