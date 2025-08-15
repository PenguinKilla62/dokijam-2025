using UnityEngine;

public class Notes : MonoBehaviour
{

    public int speed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
