using UnityEngine;
using System.Collections;

public class CraneWheelPlayerControl : MonoBehaviour {
	public float speed = 1.0f;
	public float leftLimit = -10.0f;
	public float rightLimit = 10.0f;
	public float upLimit = 12.0f;
	public float downLimit = 10.0f;
	public GameObject wheel;
	public GameObject verticalSound;
	public GameObject horizontalSound;
	
	
	// Update is called once per frame
	void FixedUpdate () {
		var inputX = Input.GetAxis("Horizontal");
		horizontalSound.audio.volume = Mathf.Abs(inputX);
		wheel.transform.Translate(inputX * speed * Time.fixedDeltaTime, 0.0f, 0.0f);
		
		var inputY = Input.GetAxis("Vertical");
		verticalSound.audio.volume = Mathf.Abs(inputY);
		wheel.transform.Translate(0.0f, 0.0f, -inputY * speed * Time.fixedDeltaTime);
		
		if (wheel.transform.position.x < leftLimit)
		{
			wheel.transform.position = new Vector3(leftLimit, wheel.transform.position.y, 0.0f);
		}
		
		if (wheel.transform.position.x > rightLimit)
		{
			wheel.transform.position = new Vector3(rightLimit, wheel.transform.position.y, 0.0f);
		}
		
		if (wheel.transform.position.y > upLimit)
		{
			wheel.transform.position = new Vector3(wheel.transform.position.x, upLimit, 0.0f);
		}
		
		if (wheel.transform.position.y < downLimit)
		{
			wheel.transform.position = new Vector3(wheel.transform.position.x, downLimit, 0.0f);
		}
	}
}
