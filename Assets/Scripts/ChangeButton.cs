using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    public Button button; // Reference to the button to change the sprite of
    public Sprite newSprite; // Reference to the new sprite to use for the button
    public Image imageToChange; // Reference to the image attached to the button to change

    // Call this method to change the sprite of the button and the image attached to it
    public void ChangeButtonAndImageSprite()
    {
        button.image.sprite = newSprite; // Set the button sprite to the new sprite
        imageToChange.sprite = newSprite; // Set the image sprite to the new sprite
    }
}