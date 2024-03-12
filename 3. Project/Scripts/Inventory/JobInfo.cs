using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JobInfo : UIBase
{
    private TMP_Text jobText;
    private List<JobIcon> jobIcons = new List<JobIcon>();
    private Dictionary<JobBit,Sprite> jobSprites = new Dictionary<JobBit,Sprite>();
    private Dictionary<AttackBit,Sprite> attackSprites = new Dictionary<AttackBit, Sprite>();
    
    public override void Initialize()
    {
        jobIcons.AddRange( GetComponentsInChildren<JobIcon>(true) );
        foreach( var icon in jobIcons )
            icon.Initialize();

        jobSprites.Add(JobBit.ARCHER, Resources.Load<Sprite>("Images/JobIcon/role_icon_archer"));
        jobSprites.Add(JobBit.ASSASSIN, Resources.Load<Sprite>("Images/JobIcon/role_icon_assassin"));
        jobSprites.Add(JobBit.KNIGHT, Resources.Load<Sprite>("Images/JobIcon/role_icon_paladin"));
        jobSprites.Add(JobBit.PRIEST, Resources.Load<Sprite>("Images/JobIcon/role_icon_priest"));
        jobSprites.Add(JobBit.WARRIOR, Resources.Load<Sprite>("Images/JobIcon/role_icon_warrior"));
        jobSprites.Add(JobBit.WIZARD, Resources.Load<Sprite>("Images/JobIcon/role_icon_wizard"));

        attackSprites.Add(AttackBit.Hwany, Resources.Load<Sprite>("Images/equip_icon_sword"));
        attackSprites.Add(AttackBit.Sangoh, Resources.Load<Sprite>("Images/equip_icon_sword"));
        attackSprites.Add(AttackBit.Jinny, Resources.Load<Sprite>("Images/equip_icon_sword"));
        attackSprites.Add(AttackBit.Angella, Resources.Load<Sprite>("Images/equip_icon_sword"));
        attackSprites.Add(AttackBit.Sunny, Resources.Load<Sprite>("Images/equip_icon_sword"));
    }

    public void SetActiveIcons(bool active)
    {
        foreach (var icon in jobIcons) icon.SetActive(active);
    }
    // 정보를 받아서 아이콘에 대한 정보를 갱신하는 함수입니다.
    public void SetInfo(ItemInfo info)
    {
        // 아이콘 그룹을 모두 꺼줍니다.
        SetActiveIcons(false);
        // 열거형에 대한 요소를 배열로 받습니다.
        JobBit[] jobArray = (JobBit[])System.Enum.GetValues(typeof(JobBit));
        int slotIndex = 0;
        for( int i = 0; i< 6; ++ i )
        {
            int jobType = (int)jobArray[i + 1];
            // 아이템을 착용할 수 있는 직업군이라면 아이콘을 보여주도록 처리합니다.
            if( (info.wearType & jobType) == jobType)
            {
                jobIcons[slotIndex].SetIcon(jobSprites[(JobBit)jobType]);
                jobIcons[slotIndex].SetActive(true);
                ++slotIndex;
            }
        }
    }


}
