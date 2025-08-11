using UnityEngine;

[CreateAssetMenu(fileName = "LongDragoon", menuName = "Scriptable Objects/LongDragoon")]
public class LongDragoon : ScriptableObject
{
    public int damage = 15;
    public float speed = 7f;
    public int[] attacks = { 0, 1, 2 };
    [Header("Prefab Reference")]
    public GameObject dragoonPrefab;
}
