using UnityEngine;

public class SetResolutionObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(640, 360, true);
    }
}
