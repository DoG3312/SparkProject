using System;
using UnityEngine;

public enum SkillStatus : byte
{
    None,
    Ready,
    Cooldown
}
public abstract class BaseSkill
{
    public event Action<float, float> EventChangeCooldownTimer;
    public string Title {  get; private set; }
    public string Description { get; private set; }
    public Sprite IconImage { get; private set; }
    public float CooldownTime { get; private set; }
    public float CooldownTimer { get; private set; }
    public SkillStatus Status { get; private set; }

    public void SetDescription(string title, string description, Sprite iconImage)
    {
        Title = title;
        Description = description;
        IconImage = iconImage;
    }

    public float SetCooldownTime(float cooldown) => CooldownTime = cooldown;
    public void ChangeStatus(SkillStatus skillStatus) => Status = skillStatus;
    public void ChangeCooltownTimer(float timer)
    {
        CooldownTimer = Mathf.Clamp(timer, 0.0f, CooldownTime);
        EventChangeCooldownTimer?.Invoke(CooldownTimer, CooldownTime);
    }

    public virtual void StartCast()
    {

    }

    public virtual bool CheckCondition(Controller owner, Controller target, Vector3 location = default)
    {
        return false;
    }
    public virtual void ApplayCast()
    {

    }
    public virtual void EventTick(float deltaTick)
    {

    }
    public virtual void CancelCast() { }
}
