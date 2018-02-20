using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {
	public enum State {
		RUNNING ,
		PAUSED,
		STOPPED
	}
	public static State gameState;
}
