using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobGroup
{
    public string name;
    public Sprite icon;
    public int tableID;
    public GameObject character;
}

public class SelectScene : MonoBehaviour
{
    private Transform rootTransform;
    private CharacterMovement movement;
    private UISelector uiSelector;
    private RenderTexture texturePrefab;
    private CharacterGroup characterGroupPrefab;

    // UI�� ����ϱ� ���� 3D ĳ���� �׷�
    private Transform renderTextureCameraGroup;

    private float intervalDistance = 4;
    private int characterNumber = 0;

    private int current = 0;
    private int next = 2;
    public float speed = 1;
    public bool usedGraph = false;
    public float characterGroupDistance = 10;

    // ��������¿� ���� ĳ���� �׷�
    private List<CharacterGroup> groups = new List<CharacterGroup>();

    // ù��°���� �����ε���, �ι�°���� �̸��� �����ܵ�����
    private Dictionary<int, JobGroup> charJobGroup = new Dictionary<int, JobGroup>();

    //private Dictionary<JobBit, Sprite> jobSprites = new Dictionary<JobBit, Sprite>();
    private Dictionary<AttackInfo, Sprite> attackSprites = new Dictionary<AttackInfo, Sprite>();


    public void AddCharacterGroup( CharacterGroup prefab, UnityEngine.Object characterPrefab, RenderTexture texture)
    {
        if (prefab == null) return;
        CharacterGroup newGroup = Instantiate(prefab, renderTextureCameraGroup);
        newGroup.Initialize();
        newGroup.Create(characterPrefab, texture);
        newGroup.transform.localPosition = new Vector3(groups.Count * characterGroupDistance, 0, 0);


        groups.Add(newGroup);
    }

    public void Create()
    {
        ICollection<int> keys = DataManager.GetKeys(TableType.SelectTable);

        if ( keys == null ) return;
        characterNumber = keys.Count;

        GameObject characters = GameObject.Find("Characters");

        if ( characters == null ) return;
        rootTransform = characters.transform;

        float distance = 0;
        int slotIndex = 0;

        foreach( var key in keys )
        {
            int charTableID = DataManager.ToInteger(TableType.SelectTable, key, "CHARID");
            float localScale = DataManager.ToFloat(TableType.SelectTable, key, "LOCALSCALE");

            string prefabName = DataManager.ToString(TableType.CharacterTable, charTableID, "MODEL");

            JobBit jobBit = DataManager.ToEnum<JobBit>(TableType.CharacterTable, charTableID, "JOB");
            AttackInfo attackBit = DataManager.ToEnum<AttackInfo>(TableType.CharacterTable, charTableID, "ATTACKINFO");

            int localTableID = DataManager.ToInteger(TableType.CharacterTable, charTableID, "NAME");
            string charName = DataManager.ToString(TableType.LocalTable, localTableID, "KOREA");

            UnityEngine.Object loadPrefab = Resources.Load($"Prefabs/Characters/{prefabName}");
            if (loadPrefab == null)
                continue;

            GameObject created = Instantiate(loadPrefab as GameObject, rootTransform);
            created.transform.localPosition = new Vector3(0, 0, distance);

            created.transform.localRotation = Quaternion.Euler(0, 0, 0);
            created.transform.localScale = new Vector3(localScale, localScale, localScale );  
            distance -= intervalDistance;

            // �Ʒ��� �ڵ�� UI�󿡼� �����ֱ� ���� �κ��� �����ϴ� �ڵ�
            // 1. RenderTexture
            // 2. ĳ���͸޸�
            RenderTexture copyTexture = Instantiate(texturePrefab);
            uiSelector.AddIcon(charTableID, slotIndex, charName, copyTexture);
            AddCharacterGroup(characterGroupPrefab, loadPrefab, copyTexture);

            JobGroup jobGroup = new JobGroup();
            jobGroup.name = attackBit.ToString();
            jobGroup.icon = attackSprites[attackBit];
            jobGroup.character = created;
            jobGroup.tableID = charTableID;

            charJobGroup.Add(slotIndex, jobGroup);

            ++slotIndex;
        }

        movement.Initialize(characterNumber, intervalDistance);

        movement.SetCompleted(SetCurrentNameGroup);

        uiSelector.SetClickEvent(OnIconClicked);

        uiSelector.SetArrowClickEvent(OnLeftClicked, OnRightClicked);
        uiSelector.SetNumberText(current, characterNumber);
        uiSelector.SetFocusActive(current);

        SetCurrentNameGroup();

    } 

    public void SetCurrentNameGroup()
    {
        uiSelector.SetNameGroup(charJobGroup[current].character.transform.Find("Name").position, charJobGroup[current].name, charJobGroup[current].icon);

        uiSelector.ShowNameGroup();

    }

    // �������� Ŭ������ �� ȣ��� �Լ�
    private void OnIconClicked(IconGroup iconGroup)
    {
        if (current == iconGroup.SlotIndex) 
            return;

        if (movement.IsUpdate)
            return;

        movement.Move(current, iconGroup.SlotIndex, speed, usedGraph);
        current = iconGroup.SlotIndex;

        uiSelector.SetNumberText(current, characterNumber);
        uiSelector.SetFocusActive(current);

        // �������� Ŭ���ϸ� �̸��� �Ⱥ��̵��� ó��
        uiSelector.HideNameGroup();

    }

    private void OnLeftClicked()
    {
        if (movement.IsUpdate) return;

        int localCurrent = current;
        --current;

        if (current < 0)
            current = characterNumber-1;

        int localNext = current;

        if (localNext == localCurrent)
            return;

        movement.Move(localCurrent, localNext, speed, usedGraph);

        uiSelector.SetNumberText(current, characterNumber);
        uiSelector.SetFocusActive(current);

        uiSelector.HideNameGroup();
    }

    private void OnRightClicked()
    {
        if (movement.IsUpdate) return;

        int localCurrent = current;
        ++current;

        if (current >= characterNumber)
            //current = characterNumber - 1;
            current = 0;

        int localNext = current;

        if (localNext == localCurrent)
            return;

        movement.Move(localCurrent, localNext, speed, usedGraph);

        uiSelector.SetNumberText(current, characterNumber);
        uiSelector.SetFocusActive(current);

        uiSelector.HideNameGroup();
    }

    public void LoadTable()
    {
        DataManager.Load(TableType.CharacterTable);
        DataManager.Load(TableType.LocalTable);
        DataManager.Load(TableType.SelectTable);
        DataManager.Load(TableType.Item);
    }

    public void Initialize()
    {
        movement=GameObject.FindFirstObjectByType<CharacterMovement>();

        uiSelector = GameObject.FindFirstObjectByType<UISelector>();
        uiSelector.Initialize();
        uiSelector.SetSelectClickEvent(OnSelectClicked);
        
        texturePrefab = Resources.Load<RenderTexture>("RenderTexture/1");
        characterGroupPrefab = Resources.Load<CharacterGroup>("RenderTexturePrefabs/CharacterGroup");

        GameObject characterGroup = GameObject.Find("RenderTextureCameraGroup");
        if (characterGroup != null)
            renderTextureCameraGroup = characterGroup.transform;

        //jobSprites.Add(JobBit.ARCHER, Resources.Load<Sprite>("Images/JobIcon/role_icon_archer"));
        //jobSprites.Add(JobBit.ASSASSIN, Resources.Load<Sprite>("Images/JobIcon/role_icon_assassin"));
        //jobSprites.Add(JobBit.KNIGHT, Resources.Load<Sprite>("Images/JobIcon/role_icon_paladin"));
        //jobSprites.Add(JobBit.PRIEST, Resources.Load<Sprite>("Images/JobIcon/role_icon_priest"));
        //jobSprites.Add(JobBit.WARRIOR, Resources.Load<Sprite>("Images/JobIcon/role_icon_warrior"));
        //jobSprites.Add(JobBit.WIZARD, Resources.Load<Sprite>("Images/JobIcon/role_icon_wizard"));

        attackSprites.Add(AttackInfo.BRONZE, Resources.Load<Sprite>("Images/RankIcons/Bronze_1"));
        attackSprites.Add(AttackInfo.SILVER, Resources.Load<Sprite>("Images/RankIcons/Silver_1"));
        attackSprites.Add(AttackInfo.GOLD, Resources.Load<Sprite>("Images/RankIcons/Gold_1"));
        attackSprites.Add(AttackInfo.PLATINUM, Resources.Load<Sprite>("Images/RankIcons/Gold_2"));
        attackSprites.Add(AttackInfo.DIAMOND, Resources.Load<Sprite>("Images/RankIcons/Medal_Gold"));
    }

    // ���ù�ư�� ������ �� ȣ��Ǵ� �Լ�
    // ������ ĳ������ ��� ���
    // ����Ʈ�ƿ��� �����ϰ� �ٸ������� �����Ѵ�.
    void OnSelectClicked()
    {
        if ( charJobGroup.ContainsKey(current))
        {
            Animator ani = charJobGroup[current].character.GetComponentInChildren<Animator>();
            ani.SetTrigger("Select");
            GameDB.AddCharacter(charJobGroup[current].tableID);
            GameDB.Save("userInfo.txt");
        }
    }

    void Start()
    {
        LoadTable();

        GameDB.Load("userInfo.txt");

        Initialize();
        Create();
    }

}
