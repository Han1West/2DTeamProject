using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private DatabaseManager theDatabase;
    private AudioManger theAudio;
    private OrderManager theOrder;
    private OkOrCancel theOOC;
    private PlayerStat thePlayerStat;

    public string keySound;
    public string enterSound;
    public string cancelSound;
    public string openSound;
    public string beepSound;
    public string useSound;

    private InventorySlot[] slots;

    private List<Item> inventoryItemList; //플레이어가 소지한 아이템 리스트
    private List<Item> inventoryTabList; //선택한 탭에 해당하는 아이템 리스트

    public TMP_Text Description_Text;
    public string[] TabDescription; //탭 부연설명
    
    public Transform tf; // slot 부모객체(grid slot)

    public GameObject go; //SetActive용 변수
    public GameObject[] selectedTabImages;
    public GameObject go_OOC; //선택지 활성화 비활성화
    public GameObject prefab_Floating_Text;

    private int selectedItem; //선택된 아이템
    private int selectedTab;

    private int page;
    private int slotCount;
    private const int MAX_SLOTS_COUNT = 12;

    private bool activated; //인벤토리 활성화시 true
    private bool TabActivated;
    private bool itemActivated;
    private bool stopKeyInput; //소비시 키입력 방지
    private bool preventExec; //중복실행 제한

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        thePlayerStat = FindObjectOfType<PlayerStat>();
        theOOC = FindObjectOfType<OkOrCancel>();
        theOrder = FindObjectOfType<OrderManager>();
        theAudio = FindObjectOfType<AudioManger>();
        theDatabase = FindObjectOfType<DatabaseManager>();
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
    }

    public void GetAnItem(int _itemID, int _count = 1)
    {
        for(int i = 0; i < theDatabase.itemList.Count; i++) //데이터베이스아이템 검색
        {
            if(_itemID == theDatabase.itemList[i].itemID) //DB상 아이템 발견
            {
                var clone = Instantiate(prefab_Floating_Text, PlayerManager.instance.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = theDatabase.itemList[i].itemName + " " + _count + "개 획득 + ";
                clone.transform.SetParent(this.transform);

                for(int j =0; j < inventoryItemList.Count; j++) // 소지품 검색
                if(inventoryItemList[j].itemID == _itemID) //이미 소유중
                    {
                        if(inventoryItemList[j].itemType == Item.ItemType.Use)
                        {
                            inventoryItemList[j].itemCount += _count;
                            return;
                        }
                        else
                        {
                            inventoryItemList.Add(theDatabase.itemList[i]);
                        }
                        return;
                    }
                inventoryItemList.Add(theDatabase.itemList[i]); //없는 경우
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("데이터베이스에 해당 ID값을 가진 아이템이 존재하지 않습니다."); //DB상에 없음
    }

    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Removeitem();
            slots[i].gameObject.SetActive(false);
        }
    }

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } //탭 활성화
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for(int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description_Text.text = TabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    } //선택된 탭 제외 다른 모든 탭의 컬러 알파값 0으로 조정
    IEnumerator SelectedTabEffectCoroutine()
    {
        while(TabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while(color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } // 선택된 탭 반짝임 효과

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;
        page = 0;

        switch(selectedTab)
        {
            case 0:
                for(int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 3:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.ETC == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
        } //탭에 따른 아이템 분류, for문을 통해 찾고 리스트에 추가

        ShowPage();

        SelectedItem();
    } //아이템 활성화 (Inventorytablist에 조건에 맞는 아이템을 넣고, 슬롯에 출력
    public void ShowPage()
    {
        slotCount = -1;

        for (int i = page*MAX_SLOTS_COUNT; i < inventoryTabList.Count; i++)
        {
            slotCount = i - (page * MAX_SLOTS_COUNT);
            slots[slotCount].gameObject.SetActive(true);
            slots[slotCount].Additem(inventoryTabList[i]);

            if (slotCount == MAX_SLOTS_COUNT - 1)
                break;
        } //인벤토리 탭 리스트의 내용을 인벤토리 슬롯에 추가
    }
    
    public void SelectedItem()
    {
        StopAllCoroutines();
        if(slotCount > -1)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for(int i = 0; i <= slotCount; i++)            
                slots[i].selected_Item.GetComponent<Image>().color = color;            
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
        {
            Description_Text.text = "해당 타입의 아이템을 소유하고 있지 않습니다.";
        }
    } // 선택된 아이템 제외 다른 모든 탭의 컬러 알파값 0으로 조정
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } // 선택된 아이템 반짝임 효과
    // Update is called once per frame
    void Update()
    {
        if(!stopKeyInput)
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;

                if (activated)
                {
                    theAudio.Play(openSound);
                    theOrder.NotMove();
                    go.SetActive(true);
                    selectedTab = 0;
                    TabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    theAudio.Play(cancelSound);
                    StopAllCoroutines();
                    go.SetActive(false);
                    TabActivated = false;
                    itemActivated = false;
                    theOrder.Move();
                }
            }

            if(activated)
            {
                if(TabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0;
                        theAudio.Play(keySound);
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                            selectedTab--;
                        else
                            selectedTab = selectedTabImages.Length - 1;
                        theAudio.Play(keySound);
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.F))
                    {
                        theAudio.Play(enterSound);
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        TabActivated = false;
                        preventExec = true;
                        ShowItem();
                    }
                } //탭 활서화시 키입력 처리

                else if(itemActivated)
                {
                    if(inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if(selectedItem + 2 > slotCount)
                            {
                                if(page < (inventoryItemList.Count - 1)/ MAX_SLOTS_COUNT)
                                    page++;
                                else
                                    page = 0;

                                RemoveSlot();
                                ShowPage();
                                selectedItem = -2;
                            }
                            if (selectedItem < slotCount - 1)
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            theAudio.Play(keySound);
                            SelectedItem();
                        }
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem -2 < 0)
                            {
                                if (page != 0)
                                    page--;
                                else
                                    page = (inventoryItemList.Count - 1) / MAX_SLOTS_COUNT;

                                RemoveSlot();
                                ShowPage();
                            }

                            if (selectedItem > 1)
                                selectedItem -= 2;
                            else
                                selectedItem = slotCount - selectedItem;
                            theAudio.Play(keySound);
                            SelectedItem();
                        }
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem + 1 > slotCount)
                            {
                                if (page < (inventoryItemList.Count - 1) / MAX_SLOTS_COUNT)
                                    page++;
                                else
                                    page = 0;

                                RemoveSlot();
                                ShowPage();
                                selectedItem = -1;
                            }
                            if (selectedItem < slotCount)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            theAudio.Play(keySound);
                            SelectedItem();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem - 1 < 0)
                            {
                                if (page != 0)
                                    page--;
                                else
                                    page = (inventoryItemList.Count - 1) / MAX_SLOTS_COUNT;

                                RemoveSlot();
                                ShowPage();
                            }

                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = slotCount;
                            theAudio.Play(keySound);
                            SelectedItem();
                        }
                        if (Input.GetKeyDown(KeyCode.F) && !preventExec)
                        {
                            if (selectedTab == 0)
                            {
                                theAudio.Play(enterSound);
                                stopKeyInput = true;
                                StartCoroutine(OOCCoroutine());
                            }
                            else if (selectedTab == 1)
                            {
                                theAudio.Play(enterSound);
                            }
                            else
                            {
                                theAudio.Play(beepSound);
                            }
                        }
                    }   
                if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        theAudio.Play(cancelSound);
                        StopAllCoroutines();
                        itemActivated = false;
                        TabActivated = true;
                        ShowTab();
                    }
                } //아이템 활성화시 키입력 처리

                if (Input.GetKeyUp(KeyCode.F)) //중복 실행 방지
                    preventExec = false;
            }
        }
    }

    IEnumerator OOCCoroutine()  // O or Cancel
    {
        go_OOC.SetActive(true);
        theOOC.ShowTwoChoice("사용", "취소");
        yield return new WaitUntil(() => !theOOC.activated);
        if(theOOC.GetResult())
        {
            for(int i = 0; i < inventoryItemList.Count; i++)
            {
                if(inventoryItemList[i].itemID == inventoryTabList[selectedItem].itemID)
                {
                    if(inventoryTabList[i].itemType == Item.ItemType.Use)
                    {
                        if (thePlayerStat.hp == thePlayerStat.currentHp)
                        {
                            theAudio.Play(beepSound);
                            break;
                        }
                        else
                        {
                            theDatabase.UseItem(inventoryItemList[i].itemID);

                            if (inventoryItemList[i].itemCount > 1)
                                inventoryItemList[i].itemCount--;
                            else
                                inventoryItemList.RemoveAt(i);
                        }
                    }
                    else
                    {
                        theDatabase.UseItem(inventoryItemList[i].itemID);

                        if (inventoryItemList[i].itemCount > 1)
                            inventoryItemList[i].itemCount--;
                        else
                            inventoryItemList.RemoveAt(i);
                    }

                    theAudio.Play(useSound);
                    ShowItem();
                    break;
                }    
            }
        }
        stopKeyInput = false;
        go_OOC.SetActive(false);
    }
}
