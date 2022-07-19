using UnityEngine;
public class EntityBaseData : MonoBehaviour
{
    [SerializeField]
    private long _id;
    [SerializeField]
    private eEntityType _type;

    public long Id => _id;
    public eEntityType Type => _type;

    public void Init(long id, eEntityType type)
    {
        _id = id;
        _type = type;
    }

    public void Reset()
    {
        _id = 0;
        _type = eEntityType.unknown;
    }
}