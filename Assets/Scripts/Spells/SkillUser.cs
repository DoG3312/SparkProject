using System.Collections.Generic;
using UnityEngine;

public class SkillUser : MonoBehaviour 
{
    [SerializeField] private Controller ownerController;
    [SerializeField] private LayerMask targetLayer;
    private Inventory inventory;

    private List<BaseSkill> skills = new();
    private BaseSkill currentSkill;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        inventory = ownerController.GetInventory();
        inventory.InitSkills();
        skills.AddRange(inventory.GetSkills());
    }

    public void Initialization()
    {
        skills.AddRange(inventory.GetSkills());
    }

    public void OnClickSkillButton(int skillIndex)
    {
        currentSkill?.CancelCast();
        switch (skills[skillIndex].Status)
        {
            case SkillStatus.Ready:
                currentSkill = skills[skillIndex];
                currentSkill.StartCast();
                break;
            case SkillStatus.Cooldown:
                break;
        }
    }

    private void Update()
    {
        for (int i = 0; i < skills.Count; ++i)
        {
            skills[i].EventTick(Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnClickSkillButton(1);
                CursorManager.Instance.SetCursor(CursorType.Attack);
            }

        }

        if(currentSkill != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                currentSkill.CancelCast();
                currentSkill = null;
                CursorManager.Instance.SetCursor(CursorType.Default);
                return;
            }

            Vector3 location = Vector3.zero;
            Controller target = null;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hitResult = Physics.RaycastAll(ray, 500f, targetLayer);

            for (int i = 0;i < hitResult.Length;++i)
            {
                hitResult[i].collider.TryGetComponent<Controller>(out target);

                if (hitResult[i].collider.CompareTag("Ground"))
                {
                    location = hitResult[i].point;
                }
            }

            if (currentSkill.CheckCondition(ownerController, target, location))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    currentSkill.ApplayCast();
                    CursorManager.Instance.SetCursor(CursorType.Default);
                    currentSkill = null;
                }
            }
        }
    }
}
