using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InputField : MonoBehaviour
{
    private PlayerManager thePlayer;

    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            thePlayer.characterName = text.text;
            Destroy(this.gameObject);
        }
    }
}
