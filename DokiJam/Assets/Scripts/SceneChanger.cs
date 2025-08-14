using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    public void ChangeToStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Office");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
