using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum InventoryListType
{
    WeaponSlots,
    SkillSlots,
    ThrowableSlots,
    PotionSlots,
    Weapons,
    Skills,
}

public class Inventory 
{
    [Header("Inventory")]
    public WeaponItem[] weaponSlots;
    public SkillConfig[] skillSlots;
    public Item[] throwablesSlots;
    public Item[] potionsSlots;
    [Header("QuickAccessToolbar")]
    public WeaponItem[] weapons;
    public SkillConfig[] skills;
    [Header("Artifacts")]
    public List<Artifact> artifacts;

    private List<BaseSkill> currentSkills = new();

    public event Action OnWeaponsChanged;

    public SkillUser skillUser; //надеюсь временно

    public Controller controller; //возможно здесь его не должно быть

    public Inventory(Controller controller, SkillUser skillUser)
    {
        weaponSlots = new WeaponItem[10];
        skillSlots = new SkillConfig[10];
        throwablesSlots = new Item[5];
        potionsSlots = new Item[5];
        weapons = new WeaponItem[2];
        skills = new SkillConfig[2];
        artifacts = new List<Artifact>();
        this.controller = controller;
        this.skillUser = skillUser;
    }

    public void AddItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.Weapon:
                if (!AddItemToSlot(weapons, item))
                {
                    AddItemToSlot(weaponSlots, item);
                }
                else
                {
                    OnWeaponsChanged();
                }
                break;
            case ItemType.Skill:
                if (!AddItemToSlot(skills, item))
                {
                    AddItemToSlot(skillSlots, item);
                }
                else
                {
                    InitSkills();
                }
                break;
            case ItemType.Throwable:
                AddItemToSlot(throwablesSlots, item);
                break;
            case ItemType.Potion:
                AddItemToSlot(potionsSlots, item);
                break;
            case ItemType.Artifact:
                if (item is ArtifactItem artifact)
                {
                    AddArtifact(artifact);
                }
                break;
        }
    }

    bool AddItemToSlot(Item[] inventorySlots, Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                inventorySlots[i] = item;
                return true;
            }
        }
        return false;
    }

    public void AddArtifact(ArtifactItem artifact)
    {
        artifacts.Add(artifact.artifact);
        artifact.artifact.ApplyEffect(controller);
    }

    public void MoveItem(InventoryListType from, int fromIndex, InventoryListType to, int toIndex)
    {
        //откуда
        Item[] sourceList = GetInventoryList(from);

        //откуда
        Item itemToMove = sourceList[fromIndex];

        //куда
        Item[] destinationList = GetInventoryList(to);

        // Перемещаем предмет в слот куда
        destinationList[toIndex] = itemToMove;

        sourceList[fromIndex] = null;

        OnWeaponsChanged();
        InitSkills();
    }

    private Item[] GetInventoryList(InventoryListType listType)
    {
        switch (listType)
        {
            case InventoryListType.WeaponSlots: return weaponSlots;
            case InventoryListType.SkillSlots: return skillSlots;
            case InventoryListType.ThrowableSlots: return throwablesSlots;
            case InventoryListType.PotionSlots: return potionsSlots;
            case InventoryListType.Weapons: return weapons;
            case InventoryListType.Skills: return skills;
        }
        Debug.Log("Тут ошибочка с enum");
        return null;
    }

    public void InitSkills()
    {
        for (int i = 0; i < skills.Length; ++i)
        {
            if(skills[i] == null) return;
            SkillBuilder skillBuilder = skills[i].GetBuildet();
            currentSkills.Clear();
            skillBuilder.Make();
            currentSkills.Add(skillBuilder.GetResult());
            skillUser.Initialization();
        }
    }

    public BaseSkill[] GetSkills() => currentSkills.ToArray();
}
