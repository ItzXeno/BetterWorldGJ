using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotHolder : MonoBehaviour
{
    // Start is called before the first frame update

    public TurretData currentItem;
    public int ammountHeld;
    private Image slotImage;
    void Start()
    {
        if (currentItem != null)
        {
            slotImage = gameObject.GetComponent<Image>();
            currentItem.icon = slotImage;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
