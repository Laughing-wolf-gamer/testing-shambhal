using DG.Tweening;
using UnityEngine ;
using UnityEngine.UI ;

public class SettingsMenuItem : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private RectTransform rectTrans;

    [SerializeField] private SettingsMenu settingsMenu;

    //item button
    private Button button;

    //index of the item in the hierarchy
    private int index;

    private void Awake()
    {
        img = GetComponent<Image>();
        rectTrans = GetComponent<RectTransform>();

        //-1 to ignore the main button
        index = rectTrans.GetSiblingIndex() - 1;

        //add click listener
        button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClick);
    }

    private void OnItemClick()
    {
        settingsMenu.OnItemClick(index);
    }

    private void OnDestroy()
    {
        //remove click listener to avoid memory leaks
        button.onClick.RemoveListener(OnItemClick);
    }
    public void SetImageFade(float start,float expandFadeDuration)
    {
        if (img != null)
        {
            img.DOFade(start, expandFadeDuration).From(0f);
        }
    }
    public void UseDoTweenMenuItemPos(Vector2 pos, float expandDuration, Ease expandEase)
    {
        rectTrans.DOAnchorPos(pos, expandDuration).SetEase(expandEase);
    }
    public void SetMenuItemPos(Vector2 pos)
    {
        rectTrans.anchoredPosition = pos;
    }
}
