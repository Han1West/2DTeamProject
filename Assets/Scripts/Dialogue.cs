using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(1, 2)]    
    public string[] sentences;
    [TextArea(1, 10)]
    public string[] sentences2;
    public Sprite[] dialougueWindows;
}
