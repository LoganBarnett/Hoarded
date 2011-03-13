using UnityEngine;
using System.Linq;

public class GreedManager : MonoBehaviour {
	const int GREED_LOSS_PER_FOOD_TAKEN = 10;
	const int GREED_OF_MURDER = 1000;
	
	static int greed;
	static int stockPileGreed; // running tally from 
	
	public static void TakeFood() {
		greed -= GREED_LOSS_PER_FOOD_TAKEN;
	}
	
	public static void PersonKilled() {
		greed += GREED_OF_MURDER;
	}
	
	public static int TallyGreed() {
		var foodWeight = GameObject.FindGameObjectsWithTag("food").Sum(f => System.Convert.ToInt32(Mathf.Round(f.rigidbody.mass)));
		return greed + foodWeight;
	}
	
}
