using UnityEngine;

public class Notes : MonoBehaviour
{

    public int speed = 5;

    public bool isPaused = true;

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }
}
