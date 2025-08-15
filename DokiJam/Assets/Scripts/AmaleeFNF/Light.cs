using UnityEditor.ShaderGraph;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Light : MonoBehaviour
{

    [SerializeField]
    private float Speed = 3;
    [SerializeField]
    private int num = 2;

    private float alfa = 0;

    [SerializeField]
    private Image image;

    [SerializeField]
    public InputAction controls;

    private void OnEnable()
    {
        controls.Enable();
        
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(image.material.color.a <= 0))
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alfa);
        }

        var movement = controls.ReadValue<Vector2>();

        if (num == 1)
        {
            if (movement.x == 1)
            {
                colorChange();
            }
        }
        if (num == 2)
        {
            if (movement.x == -1)
            {
                colorChange();
            }
        }
        if (num == 3)
        {
            if (movement.y == 1)
            {
                colorChange();
            }
        }
        if (num == 4)
        {
            if (movement.y == -1)
            {
                colorChange();
            }
        }
        alfa -= Speed * Time.deltaTime;
    }

    void colorChange()
    {
        alfa = 0.3f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alfa);
    }
}
