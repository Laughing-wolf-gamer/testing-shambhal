using UnityEngine;
using UnityEngine.UI;
public class UIButtonToggles : MonoBehaviour {
    
    [SerializeField] private Sprite enableColor;
    [SerializeField] private Sprite desableColor;


    [SerializeField] private Image graphics;

    private void Awake(){
        if(graphics == null){
            graphics = GetComponent<Image>();
        }
    }
    public void Toggle(bool on){

        if(on){
            graphics.sprite = enableColor;
        }else{
            graphics.sprite = desableColor;
        }
    }
    
}
