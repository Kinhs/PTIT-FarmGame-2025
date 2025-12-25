using UnityEngine;

public class CaveBlocker : MonoBehaviour
{
    [SerializeField] private string blockerId;

    public string BlockerId => blockerId;

    private void Awake()
    {
        MaterialInfo.instance.RegisterCaveBlocker(blockerId);
        ApplyState();
    }

    public void ApplyState()
    {
        bool active = MaterialInfo.instance.IsCaveBlockerActive(blockerId);
        gameObject.SetActive(active);
    }
}
