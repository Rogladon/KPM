using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Battle {
	public class BattleStaticContainer : MonoBehaviour {
		public static BattleStaticContainer instance;

		public LineRenderer lineMove;
		public GameObject aimMove;
		public GameObject circleArea;
		public float heightUI;

		public Texture2D attackCursor;
		public Texture2D defaultCursor;
		public Texture2D noAttackCursor;

		public void Awake() {
			instance = this;
		}
	}
}
