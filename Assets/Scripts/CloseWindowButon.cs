using UnityEngine;
using UnityEngine.UI;


public class CloseWindowButon : MonoBehaviour
{

    public Image imageToToggle;

    public void OnButtonClick()
    {
        imageToToggle.gameObject.SetActive(false);
    }


}
