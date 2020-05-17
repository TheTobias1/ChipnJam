using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextOnEmpty : MonoBehaviour
{
    public InventoryUnlocker inventory;

    public bool loadNextLevel;
    public GameObject nextObject;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            if(transform.GetChild(i).gameObject.activeSelf)
            {
                return;
            }
        }

        if(loadNextLevel)
        {
            inventory.LoadNextLevel();
        }
        else
        {
            nextObject.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

}
