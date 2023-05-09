using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButton: MonoBehaviour
{
    public Image imageToToggle;

    void Start()
    {
        imageToToggle.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        imageToToggle.gameObject.SetActive(true);
    }
}
