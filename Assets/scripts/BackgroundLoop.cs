using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{   
    public float width = 20.48f;

    private void Update()
    {
        if ( transform.position.x <= -width)
        {
            transform.position += new Vector3(width * 2f, 0f, 0f);
        }
    }
}
