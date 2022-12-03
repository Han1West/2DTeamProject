using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestFloating : MonoBehaviour
{

    public TMP_Text text;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "쪽지 개수\n" + count + " /  10";
    }
}
