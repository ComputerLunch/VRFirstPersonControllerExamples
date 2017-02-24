using UnityEngine;
using System.Collections;

public class MoveToAPoint : MonoBehaviour {

	// This enum represents the way in which the camera will warp
	public enum MoveStyle
	{
		Smooth, Warp, WarpWithFade,
	}

	public float moveSpeed = 3.0f;
	private Vector3 _targetPoint;

	public MoveStyle moveStyle = MoveStyle.Warp;

	[SerializeField] VRStandardAssets.Utils.VRCameraFade m_CameraFade; 
	public float m_WarpFadeDuration = 0.2f;



	CharacterController cc;

	void Start(){

		cc = GetComponent<CharacterController>();

		_targetPoint = cc.transform.position;
	}

	void Update () {
		//Given some means of determining a target point.

		if(moveStyle == MoveStyle.Smooth){
			MoveTowardsTarget (_targetPoint);
		}
	}

	public void SetTarget(Vector3 targetPoint){

		_targetPoint = targetPoint;

		if(moveStyle == MoveStyle.Warp ){
			WarpTowardsTarget(_targetPoint);
		}else if(moveStyle == MoveStyle.WarpWithFade ){
			StartCoroutine("WarpCamera" , _targetPoint );
		}
	}
		
	void MoveTowardsTarget(Vector3 target) {
		
		var offset = target - transform.position;
		//Get the difference.
		if(offset.magnitude > .1f) {
			//If we're further away than .1 unit, move towards the target.
			//The minimum allowable tolerance varies with the speed of the object and the framerate. 
			// 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
			offset = offset.normalized * moveSpeed;
			//normalize it and account for movement speed.
			cc.Move(offset * Time.deltaTime);
			//actually move the character.
		}
	}

	void WarpTowardsTarget(Vector3 target) {

		cc.transform.position = target;
	}
		
	private IEnumerator WarpCamera(Vector3 target)
	{
		yield return StartCoroutine(m_CameraFade.BeginFadeOut(m_WarpFadeDuration, false));
			cc.transform.position = target;
		yield return StartCoroutine(m_CameraFade.BeginFadeIn(m_WarpFadeDuration, false));

	}
}
