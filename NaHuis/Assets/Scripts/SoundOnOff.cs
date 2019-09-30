using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnOff : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    
    private void changeSprite()
    {
        if (this.GetComponent<SpriteRenderer>().sprite == soundOnSprite)
        {
            this.GetComponent<SpriteRenderer>().sprite = soundOffSprite;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = soundOnSprite;
        }
    }
    

}
