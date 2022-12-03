using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    //Tile Map 저장.
    public GameObject map;
    
    //맵안에 배치된 시작 포인트.
    public Transform StartPoint;
    
    //맵 안에 배치된 바깥 포인트.
    public Transform OutPoint;

    public void EnableMap()
    {
        map.SetActive(true);
        OutPoint.gameObject.SetActive(true);
        StartPoint.gameObject.SetActive(false);
    }

    public void DisableMap()
    {
        map.SetActive(false);
        OutPoint.gameObject.SetActive(false);
        StartPoint.gameObject.SetActive(false);
    }

}
