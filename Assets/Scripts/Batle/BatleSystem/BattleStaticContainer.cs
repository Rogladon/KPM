using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Battle {
	public class BattleStaticContainer : MonoBehaviour {
		public static BattleStaticContainer instance;

		public LineRenderer lineMove;
		public GameObject aimMove;
		public float heightUI;

		public void Awake() {
			instance = this;
		}
	}
}
