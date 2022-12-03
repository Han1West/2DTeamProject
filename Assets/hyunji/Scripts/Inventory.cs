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

    public string keySound;
    public string enterSound;
    public string cancelSound;
    public string openSound;
    public string beepSound;
    public string useSound;

    private InventorySlot[] slots;

    private List<Item> inventoryItemList; //�÷��̾ ������ ������ ����Ʈ
    private List<Item> inventoryTabList; //������ �ǿ� �ش��ϴ� ������ ����Ʈ

    public TMP_Text Description_Text;
    public string[] TabDescription; //�� �ο�����
    
    public Transform tf; // slot �θ�ü(grid slot)

    public GameObject go; //SetActive�� ����
    public GameObject[] selectedTabImages;
    public GameObject go_OOC; //������ Ȱ��ȭ ��Ȱ��ȭ
    public GameObject prefab_Floating_Text;

    private int selectedItem; //���õ� ������
    private int selectedTab;

    private bool activated; //�κ��丮 Ȱ��ȭ�� true
    private bool TabActivated;
    private bool itemActivated;
    private bool stopKeyInput; //�Һ�� Ű�Է� ����
    private bool preventExec; //�ߺ����� ����

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
        for(int i = 0; i < theDatabase.itemList.Count; i++) //�����ͺ��̽������� �˻�
        {
            if(_itemID == theDatabase.itemList[i].itemID) //DB�� ������ �߰�
            {
                var clone = Instantiate(prefab_Floating_Text, PlayerManager.instance.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = theDatabase.itemList[i].itemName + " " + _count + "�� ȹ�� + ";
                clone.transform.SetParent(this.transform);

                for(int j =0; j < inventoryItemList.Count; j++) // ����ǰ �˻�
                if(inventoryItemList[j].itemID == _itemID) //�̹� ������
                    {
                        if(inventoryItemList[j].itemType == Item.ItemType.Use)
                        {
                            inventoryItemList[j].itemCount += _count;
                            return;
                        }
                        else
                        {
                            inventoryItemList.Add(theDatabase.itemList[j]);
                        }
                        return;
                    }
                inventoryItemList.Add(theDatabase.itemList[i]); //���� ���
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("�����ͺ��̽��� �ش� ID���� ���� �������� �������� �ʽ��ϴ�."); //DB�� ����
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
    } //�� Ȱ��ȭ
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
    } //���õ� �� ���� �ٸ� ��� ���� �÷� ���İ� 0���� ����
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
    } // ���õ� �� ��¦�� ȿ��

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

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
        } //�ǿ� ���� ������ �з�, for���� ���� ã�� ����Ʈ�� �߰�
    
        for(int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        } //�κ��丮 �� ����Ʈ�� ������ �κ��丮 ���Կ� �߰�

        SelectedItem();
    } //������ Ȱ��ȭ (Inventorytablist�� ���ǿ� �´� �������� �ְ�, ���Կ� ���
    public void SelectedItem()
    {
        StopAllCoroutines();
        if(inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for(int i = 0; i < inventoryTabList.Count; i++)            
                slots[i].selected_Item.GetComponent<Image>().color = color;            
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
        {
            Description_Text.text = "�ش� Ÿ���� �������� �����ϰ� ���� �ʽ��ϴ�.";
        }
    } // ���õ� ������ ���� �ٸ� ��� ���� �÷� ���İ� 0���� ����
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
    } // ���õ� ������ ��¦�� ȿ��
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
                } //�� Ȱ��ȭ�� Ű�Է� ó��

                else if(itemActivated)
                {
                    if(inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 2)
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            theAudio.Play(keySound);
                            SelectedItem();
                        }
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 2;
                            else
                                selectedItem = inventoryTabList.Count - 2 - selectedItem;
                            theAudio.Play(keySound);
                            SelectedItem();
                        }
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            theAudio.Play(keySound);
                            SelectedItem();
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 2;
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
                } //������ Ȱ��ȭ�� Ű�Է� ó��

                if (Input.GetKeyUp(KeyCode.F)) //�ߺ� ���� ����
                    preventExec = false;
            }
        }
    }

    IEnumerator OOCCoroutine()  // O or Cancel
    {
        go_OOC.SetActive(true);
        theOOC.ShowTwoChoice("���", "���");
        yield return new WaitUntil(() => !theOOC.activated);
        if(theOOC.GetResult())
        {
            for(int i = 0; i < inventoryItemList.Count; i++)
            {
                if(inventoryItemList[i].itemID == inventoryTabList[selectedItem].itemID)
                {
                    theDatabase.UseItem(inventoryItemList[i].itemID);

                    if (inventoryItemList[i].itemCount > 1)
                        inventoryItemList[i].itemCount--;
                    else
                        inventoryItemList.RemoveAt(i);

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