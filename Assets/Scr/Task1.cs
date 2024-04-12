using UnityEngine;

public class Task1 : MonoBehaviour
{
    enum myEnum
    {
        Item0,
        Item1,
        Item2,
        Item3,
        Item4,
        Item5,
        Item6
    }

    [SerializeField]
    private myEnum MyEnum;

    public Material ori, hl;
    public Material[] mr;
    public bool inTask = false;
    public int require;

    private void Start()
    {
        if(GetComponent<Demo_npc>() != null)
        {
            MyEnum = myEnum.Item0;
        }
    }

    public void Reward()
    {
        if (MyEnum == myEnum.Item0)
        {
            return;
        }

        int itemQuantity = PlayerPrefs.GetInt(MyEnum.ToString(), 0);
        int reward = UnityEngine.Random.Range(1, 5);
        itemQuantity += reward;

        PlayerPrefs.SetInt(MyEnum.ToString(), itemQuantity);
    }

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
