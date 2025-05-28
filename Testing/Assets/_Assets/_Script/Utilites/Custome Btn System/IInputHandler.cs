using UnityEngine;

public interface IInputHandler{
    void ProcessesInput(Vector3 inputPosition,GameObject selectedObject,System.Action callBack);
    
}
