using UnityEngine;

[CreateAssetMenu(fileName = "NormalDragoon", menuName = "Scriptable Objects/NormalDragoon")]
public class NormalDragoon : ScriptableObject
{
    public int damage = 10;
    public float speed = 5f;

    [Header("Prefab Reference")]
    public GameObject dragoonPrefab;
}
