using UnityEngine;
public class EntityBaseData : MonoBehaviour
{
    [SerializeField]
    private long _id;
    [SerializeField]
    private MelandGame3.EntityType _type;

    public long Id => _id;
    public MelandGame3.EntityType Type => _type;

    public void Init(long id, MelandGame3.EntityType type)
    {
        _id = id;
        _type = type;
    }

    public void Reset()
    {
        _id = 0;
        _type = MelandGame3.EntityType.EntityTypeAll;
    }
}