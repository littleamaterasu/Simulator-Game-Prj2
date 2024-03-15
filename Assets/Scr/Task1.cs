using UnityEngine;

public class Task1 : MonoBehaviour
{
    public Material ori, hl;
    public Material[] mr;
    public void Highlight()
    {
        if (TryGetComponent<MeshRenderer>(out var mr))
        {
            mr.material = hl;
        }
    }

    public void Origin()
    {
        if (TryGetComponent<MeshRenderer>(out var mr))
        {
            mr.material = ori;
        }
    }
}
