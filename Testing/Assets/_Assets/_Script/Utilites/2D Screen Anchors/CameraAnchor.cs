using UnityEngine;
using System.Collections;


[ExecuteInEditMode,DefaultExecutionOrder(-2)]
public class CameraAnchor : MonoBehaviour {
	public enum AnchorType {
		BottomLeft,
		BottomCenter,
		BottomRight,
		MiddleLeft,
		MiddleCenter,
		MiddleRight,
		TopLeft,
		TopCenter,
		TopRight,
	};
	[SerializeField] private bool runOnce;
	[SerializeField] private AnchorType anchorType;
	[SerializeField] private Vector3 anchorOffset;

	private IEnumerator updateAnchorRoutine; //Coroutine handle so we don't start it if it's already running
	private void Start () {
		updateAnchorRoutine = UpdateAnchorAsync();
		StartCoroutine(updateAnchorRoutine);
	}

	/// <summary>
	/// Coroutine to update the anchor only once ViewportHandler.Instance is not null.
	/// </summary>
	private IEnumerator UpdateAnchorAsync() {
		uint cameraWaitCycles = 0;
		while(ViewportHandler.Instance == null) {
			++cameraWaitCycles;
			yield return new WaitForEndOfFrame();
		}
		if (cameraWaitCycles > 0) {
			Debug.Log(string.Format("CameraAnchor found ViewportHandler instance after waiting {0} frame(s). You might want to check that ViewportHandler has an earlie execution order.", cameraWaitCycles));
		}
		UpdateAnchor();
		updateAnchorRoutine = null;
	}

	private void UpdateAnchor() {
		switch(anchorType) {
		case AnchorType.BottomLeft:
			SetAnchor(ViewportHandler.Instance.BottomLeft);
			break;
		case AnchorType.BottomCenter:
			SetAnchor(ViewportHandler.Instance.BottomCenter);
			break;
		case AnchorType.BottomRight:
			SetAnchor(ViewportHandler.Instance.BottomRight);
			break;
		case AnchorType.MiddleLeft:
			SetAnchor(ViewportHandler.Instance.MiddleLeft);
			break;
		case AnchorType.MiddleCenter:
			SetAnchor(ViewportHandler.Instance.MiddleCenter);
			break;
		case AnchorType.MiddleRight:
			SetAnchor(ViewportHandler.Instance.MiddleRight);
			break;
		case AnchorType.TopLeft:
			SetAnchor(ViewportHandler.Instance.TopLeft);
			break;
		case AnchorType.TopCenter:
			SetAnchor(ViewportHandler.Instance.TopCenter);
			break;
		case AnchorType.TopRight:
			SetAnchor(ViewportHandler.Instance.TopRight);
			break;
		}
	}

	private void SetAnchor(Vector3 anchor) {
		Vector3 newPos = anchor + anchorOffset;
		if (!transform.position.Equals(newPos)) {
			transform.position = newPos;
		}
	}

	private void Update () {
#if UNITY_EDITOR
		if (updateAnchorRoutine == null) {
			updateAnchorRoutine = UpdateAnchorAsync();
			StartCoroutine(updateAnchorRoutine);
		}
#endif
		if(Application.isPlaying){
			if(runOnce){
				Destroy(this);
			}
		}

	}
}
