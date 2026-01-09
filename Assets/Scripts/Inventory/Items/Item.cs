using UnityEngine;

public enum ItemType
{
    Weapon,
    Skill,
    Throwable,
    Potion,
    Artifact
}


public abstract class Item : ScriptableObject
{
    [Header("Item")]
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite IconImage { get; private set; }
    public ItemType type;
    public bool stackable = false;
    public int maxStack = 1;
    public int currentStack = 1;
}
