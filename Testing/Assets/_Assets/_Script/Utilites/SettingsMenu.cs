using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Abhishek.Utils;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameDataSO gameDataSO;
    [Header("space between menu items")]
    [SerializeField] private Vector2 spacing;

    [Space]
    [Header("Main button rotation")]
    [SerializeField] private float rotationDuration;
    [SerializeField] private Ease rotationEase;

    [Space]
    [Header("Animation")]
    [SerializeField] private float expandDuration;
    [SerializeField] private float collapseDuration;
    [SerializeField] private Ease expandEase;
    [SerializeField] private Ease collapseEase;

    [Space]
    [Header("Fading")]
    [SerializeField] private float expandFadeDuration;
    [SerializeField] private float collapseFadeDuration;

    [Space]
    [SerializeField] private Sprite sfxOn, sfxOff;
    [SerializeField] private Image _soundIconRef;
    [SerializeField] private Image _soundPanel;
    [Space]
    [SerializeField] private Sprite hapticsOn, hapticsOff;
    [SerializeField] private Image _hapticsIconRef;
    [SerializeField] private Image _hapticsPanel;


    private bool _hapticsStatus = true;
    private bool _soundStatus = true;

    private Button mainButton;
    private SettingsMenuItem[] menuItems;

    //is menu opened or not
    bool isExpanded = false;

    [SerializeField] private Vector2 mainButtonPosition;
    private int itemsCount;


    void ToggleSound()
    {
        _soundStatus = gameDataSO.GetSoundState();
        if (gameDataSO.GetSoundState())
        {
            if (_soundIconRef != null)
            {
                _soundIconRef.sprite = sfxOn;
            }
        }
        else
        {
            if (_soundIconRef != null)
            {
                _soundIconRef.sprite = sfxOff;
            }
        }
    }

    void ToggleHaptics()
    {
        _hapticsStatus = gameDataSO.GetHapticsState();
        if (gameDataSO.GetHapticsState())
        {
            if (_hapticsIconRef != null)
            {
                _hapticsIconRef.sprite = hapticsOn;
            }


        }
        else
        {
            if (_hapticsIconRef != null)
            {
                _hapticsIconRef.sprite = hapticsOff;
            }

        }
    }


    private void Start() {
        isExpanded = true;
        itemsCount = transform.childCount - 1;
        menuItems = new SettingsMenuItem[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }

        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);

        mainButton.transform.SetAsLastSibling();

        ResetPositions();
        UpdateSoundAndHaptics();
    }

    private void UpdateSoundAndHaptics() {
        ToggleHaptics();
        ToggleSound();
    }

    private void ResetPositions() {
        for (int i = 0; i < itemsCount; i++) {
            menuItems[i].SetMenuItemPos(mainButtonPosition);
        }
    }
    void ToggleMenu()
    {

        UpdateSoundAndHaptics();
        if (isExpanded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].UseDoTweenMenuItemPos(mainButtonPosition + spacing * (i + 1), expandDuration, expandEase);
                // menuItems[i].rectTrans.DOAnchorPos(mainButtonPosition + spacing * (i + 1), expandDuration).SetEase(expandEase);

                // menuItems[i].img.DOFade(1f, expandFadeDuration).From(0f);
                menuItems[i].SetImageFade(1f, expandFadeDuration);
                menuItems[i].transform.GetChild(0).GetComponent<Image>().DOFade(1f, expandFadeDuration).From(0f);
            }
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].UseDoTweenMenuItemPos(mainButtonPosition, collapseDuration, collapseEase);
                // menuItems[i].rectTrans.DOAnchorPos(mainButtonPosition + spacing * (i + 1), expandDuration).SetEase(expandEase);

                // menuItems[i].img.DOFade(1f, expandFadeDuration).From(0f);
                menuItems[i].SetImageFade(0f, collapseFadeDuration);
                // menuItems[i].rectTrans.DOAnchorPos(mainButtonPosition, collapseDuration).SetEase(collapseEase);

                // menuItems[i].img.DOFade(0f, collapseFadeDuration);
                menuItems[i].transform.GetChild(0).GetComponent<Image>().DOFade(0f, collapseFadeDuration);
            }
        }

        mainButton.transform.DORotate(Vector3.forward * 180f, rotationDuration).From(Vector3.zero).SetEase(rotationEase).OnComplete(() =>
        {
            isExpanded = !isExpanded;
        });
        AudioManager.Instance?.PlayOneShotMusic(Sounds.SoundType.btnClick);
    }

    public void CloseSettingScreenIfOpen()
    {
        if (!isExpanded)
        {
            ToggleMenu();
        }
    }
    public void OnItemClick(int index)
    {
        switch (index)
        {
            case 0:
                SoundBtnClick();
                break;
            case 1:
                HapticsBtnClick();
                break;
        }
    }
    public void SoundBtnClick()
    {
        gameDataSO.ToggelSound();
        UpdateSoundAndHaptics();
        AudioManager.Instance?.PlayOneShotMusic(Sounds.SoundType.btnClick);
    }
    public void HapticsBtnClick()
    {
        gameDataSO.ToggleHaptics();
        UpdateSoundAndHaptics();
        AudioManager.Instance?.PlayOneShotMusic(Sounds.SoundType.btnClick);
    }

    void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
