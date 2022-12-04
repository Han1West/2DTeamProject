using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public int playerHP;
        public int playerCurrentHP;

        public int playerATK;

        public int added_atk;

        public List<int> playerItemInventory;
        public List<int> playerItemInventoryCount;
        public List<int> playerEquipItem;


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
