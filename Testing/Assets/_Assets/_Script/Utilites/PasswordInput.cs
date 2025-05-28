using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PasswordInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Image eyeImage;
    [SerializeField] private Sprite hideSprite, showSprite;
    [SerializeField] private bool hide;
    private void Start() {
        ToggleHideUnHide();
    }
    public void ToggleHideUnHide()
    {
        hide = !hide;
        if (passwordInput != null)
        {
            if (hide)
            {
                eyeImage.sprite = hideSprite;
                passwordInput.inputType = TMP_InputField.InputType.Password;
            }
            else
            {
                eyeImage.sprite = showSprite;
                passwordInput.inputType = TMP_InputField.InputType.Standard;
            }
            passwordInput.ForceLabelUpdate();

        }
    }
    
}
