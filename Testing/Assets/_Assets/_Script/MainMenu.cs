using TMPro;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Abhishek.Utils;

public class MainMenu : MonoBehaviour {
    [SerializeField] private TMP_InputField passwordInput, phoneNumberInput;
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private Vector2 endValueButton;
    [SerializeField] private float endValueDuration = 1;
    [SerializeField] private Ease effectEase = Ease.InOutElastic;
    [SerializeField] private RectTransform logInButton;
    private Vector2 startValue;
	private void Start() {
		startValue = logInButton.sizeDelta;
    }

    public void SetPhoneNumber(string phoneNumber) {
        if (string.IsNullOrEmpty(phoneNumber)) {
            warningText.SetText("Please Provide a Phone Number");
            return;
        }
        if (IsValidPhoneNumber(phoneNumber)) {
            gameData.setPhoneNumber(phoneNumber);
            warningText.SetText("");
        }

    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            warningText.SetText("Please Provide a Phone Number");
            return;
        }
		if (!IsValidPhoneNumber(phoneNumberInput.text))
		{
			warningText.SetText("Please Provide a Valid Phone Number");
			return;
		}
        warningText.SetText("");
        gameData.setPassword(password);
    }
    private bool IsValidPhoneNumber(string phone) {
        if (!phone.All(char.IsDigit)) {
            warningText.SetText("Phone Number should be Whole Number");
            return false;
        }
        if (phone.Length < 10) {
            warningText.SetText("Phone Number should be 10 digits");
            return false;
        }
        if (phone.Length > 10) {
            warningText.SetText("Phone Number should be 10 digits");
            return false;
        }
        return true;
    }
    public void LogInGame() {
        if (string.IsNullOrEmpty(phoneNumberInput.text) || string.IsNullOrEmpty(phoneNumberInput.text)) {
            warningText.SetText("Please Provide All the Fields");
            return;
        }
		if (!IsValidPhoneNumber(phoneNumberInput.text)) {
			warningText.SetText("Please Provide a Valid Phone Number");
			return;
		}
        logInButton.DOSizeDelta(endValueButton,endValueDuration,false).SetEase(effectEase).onComplete += ()=> {
            logInButton.DOSizeDelta(startValue, endValueDuration, false);
            LevelLoader.Instance?.PlayNextLevel(SceneIndex.GameScene);
        };
    }
    public void ExitGame() {
        Debug.Log("Game Quit");
        Application.Quit();
    }
    
}
