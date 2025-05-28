using UnityEngine;

[CreateAssetMenu(fileName = "New Pool",menuName = "Gamer Wolf Utilities/Object Pooling/Pool")]
public class PoolSO : ScriptableObject {
    public GameObject prefabs;
    public int size;
}
