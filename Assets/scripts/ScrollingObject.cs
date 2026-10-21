using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 6f;

    private GameManager gameManager;

    private void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }

    private void Update()
    {        
        if (gameManager.IsGameOver)
        {
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);      //이동량
    }               
}
