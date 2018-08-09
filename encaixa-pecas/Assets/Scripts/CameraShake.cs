using UnityEngine;
using Pixelplacement;

public class CameraShake : MonoBehaviour {

	public static CameraShake instance;

	Vector3 _cameraInitialPosition;

	void Awake ()
	{
		_cameraInitialPosition = Camera.main.transform.position;
		instance = this;
	}

	public void Shake (float forceX, float forceY, float duration)
	{
		Tween.Shake(Camera.main.transform, _cameraInitialPosition, new Vector2(forceX, forceY), duration, 0);
	}
}
