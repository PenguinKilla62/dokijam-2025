using System.Collections.Generic;
using UnityEngine;

public class characterAnimator : MonoBehaviour
{

    [SerializeField]
    public enum AnimationState {
        Idle,
        Down,
        Up,
        Left,
        Right
    }

    [SerializeField]
    public AnimationState currentAnimation = AnimationState.Idle;

    [SerializeField]
    public Dictionary<AnimationState, List<Sprite>> spriteDict;

    [SerializeField]
    public List<Sprite> IdleList;

    [SerializeField]
    public List<Sprite> DownList;

    [SerializeField]
    public List<Sprite> UpList;

    [SerializeField]
    public List<Sprite> RightList;

    [SerializeField]
    public List<Sprite> LeftList;

    public float IdleTimeout = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IdleTimeout > 0)
        {
            
            IdleTimeout -= Time.deltaTime;
        }
        else
        { 
            currentAnimation = AnimationState.Idle;
        }

        if (currentAnimation == AnimationState.Idle)
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = IdleList[(int)(Time.time * 5) % IdleList.Count];
        }
        else if (currentAnimation == AnimationState.Down)
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = DownList[(int)(Time.time * 5) % DownList.Count];
        }
        if (currentAnimation == AnimationState.Up)
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = UpList[(int)(Time.time * 5) % UpList.Count];
        }
        if (currentAnimation == AnimationState.Left)
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = LeftList[(int)(Time.time * 5) % LeftList.Count];
        }
        if (currentAnimation == AnimationState.Right)
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = RightList[(int)(Time.time * 5) % RightList.Count];
        }
    }
}
