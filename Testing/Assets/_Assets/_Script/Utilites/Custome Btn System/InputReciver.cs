using UnityEngine;

public abstract class InputReciver : MonoBehaviour {
    
    protected IInputHandler[] inputHandlers;
    public abstract void OnInputRecives();
    private void Awake(){
        inputHandlers = GetComponents<IInputHandler>();
    }
}
