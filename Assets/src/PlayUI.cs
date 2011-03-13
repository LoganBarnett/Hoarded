using UnityEngine;

public class PlayUI : MonoBehaviour {
	public GameObject gameTimer;
	
	GameTimer timer;
	
	void Start() {
		timer = gameTimer.GetComponent<GameTimer>();
	}
	
	void OnGUI() {
		GUILayout.Label(string.Format("Greed: {0}", GreedManager.TallyGreed()));
		
		GUILayout.Label(string.Format("Time Left: {0}", timer.TimeLeft.ToString("N1")));
	}
}
