using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Button muteButton;
    
    public void changeMuteButtonSprite()
    {
        if (muteButton.image.sprite == soundOnSprite)
        {
            muteButton.image.sprite = soundOffSprite;
        }
        else
        {
            muteButton.image.sprite = soundOnSprite;
        }
    }
    

}
