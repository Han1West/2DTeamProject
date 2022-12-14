using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    private PlayerStat thePlayerStat;
    private Inventory theInv;
    private Truth truth;

    public GameObject prefabs_Floating_Text;
    public GameObject parent;


    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton
    // 1. 씬 이동 A(이벤트 진행정보(true false) 저장) <-> A ---> database, 어떤 변수의 값을 true ( 전역변수)
    // 2. 세이브와 로드
    // 3. 미리 만들어두면 편함(아이템)
    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;  //1번째 스위치 (event1 --> true) 이 정보를 배열에 저장후 사용

    public List<Item> itemList = new List<Item>();

    public void UseItem(int _itemID)
    {
        switch(_itemID)
        {
            case 20000:
                if (thePlayerStat.hp >= thePlayerStat.currentHp + 5)
                    thePlayerStat.currentHp += 5;
                else
                    thePlayerStat.currentHp = thePlayerStat.hp;
                FloatText(5);
                break;
            case 20001:
                if (thePlayerStat.hp >= thePlayerStat.currentHp + 10)
                    thePlayerStat.currentHp += 10;
                else
                    thePlayerStat.currentHp = thePlayerStat.hp;
                FloatText(10);
                break;
            case 00:
                switches[1] = true;
                break;
        }
    }

    public void FloatText(int number)
    {
        Vector3 vector = thePlayerStat.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefabs_Floating_Text, vector, Quaternion.Euler(Vector3.zero));

        clone.GetComponent<FloatingText>().text.color = Color.green;
        clone.GetComponent<FloatingText>().text.text = number.ToString();
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        truth = FindObjectOfType<Truth>();
        theInv = FindObjectOfType<Inventory>();
        thePlayerStat = FindObjectOfType<PlayerStat>();
        itemList.Add(new Item(10000, "묵직한 가방", "왠지 모르겠지만, 무거운 가방이다. 맞으면 꽤나 아플것 같다.", Item.ItemType.Equip,2));
        itemList.Add(new Item(10001, "장난감 검", "겉보기엔 영락없는 검이지만, 장난감에 불가하다.", Item.ItemType.Equip,4));
        itemList.Add(new Item(10002, "바인더", "모서리가 꽤나 날카로운 바인더이다.", Item.ItemType.Equip,6));
        itemList.Add(new Item(20003, "붕붕드링크", "우리가 만들어낸 폭탄드링크, 교수님께 드리자.", Item.ItemType.Equip));
        itemList.Add(new Item(20000, "피로회복제", "먹으면 약간의 체력을 회복할수 있는 회복제이다.", Item.ItemType.Use));
        itemList.Add(new Item(20001, "커피", "먹으면 체력을 회복할 수 있는 커피이다.", Item.ItemType.Use));
        itemList.Add(new Item(20002, "숙취해소제", "술이 깨는 맛이다.", Item.ItemType.Use));
        itemList.Add(new Item(30000, "USB1", "정보가 담겨있는 USB이다.", Item.ItemType.Use));
        itemList.Add(new Item(30001, "USB2", "정보가 담겨있는 USB이다.", Item.ItemType.Use));
        itemList.Add(new Item(30002, "USB2", "정보가 담겨있는 USB이다.", Item.ItemType.Use));
        itemList.Add(new Item(30003, "열쇠", "어딘가에 쓰일 것 같은 열쇠이다.", Item.ItemType.Use));
        itemList.Add(new Item(30004, "플래시", "어두운 곳에서 사용할 수 있을 듯 하다.", Item.ItemType.Use));
        itemList.Add(new Item(30005, "자물쇠", "어딘가에 사용할 수 있을 것 같은 자물쇠이다.", Item.ItemType.Use));
        itemList.Add(new Item(01, "쪽지1", "2", Item.ItemType.Quest));
        itemList.Add(new Item(02, "쪽지2", "0", Item.ItemType.Quest));
        itemList.Add(new Item(03, "쪽지3", "2", Item.ItemType.Quest));
        itemList.Add(new Item(04, "쪽지4", "2", Item.ItemType.Quest));
        itemList.Add(new Item(05, "쪽지5", "3", Item.ItemType.Quest));
        itemList.Add(new Item(06, "쪽지6", "5", Item.ItemType.Quest));
        itemList.Add(new Item(07, "쪽지7", "7", Item.ItemType.Quest));
        itemList.Add(new Item(08, "쪽지8", "8", Item.ItemType.Quest));
        itemList.Add(new Item(09, "쪽지9", "5", Item.ItemType.Quest));
        itemList.Add(new Item(10, "쪽지10", "2", Item.ItemType.Quest));
        itemList.Add(new Item(20, "비밀 쪽지", "내컴퓨터, 20180", Item.ItemType.Quest));
        itemList.Add(new Item(00, "연구 일지", "미래를 위한 가상 현실세계", Item.ItemType.Use));
    }
}
