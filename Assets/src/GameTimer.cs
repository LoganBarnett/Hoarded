using UnityEngine;

public class GameTimer : MonoBehaviour {
	public float gameDuration = 5.0f * 60.0f;
	
	public float TimeLeft {
		get {
			return gameDuration - Time.timeSinceLevelLoad;
		}
	}
	
	void Update() {
		if (Time.timeSinceLevelLoad > gameDuration) {
			// TODO: End game by loading level.
			
		}
	}
}
