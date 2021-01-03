﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.UnitCore;

namespace Battle.System {
	public class BattleSystem : MonoBehaviour {
		private static BattleSystem instnce;

		[SerializeField]
		BattleHUD battleHUD;
		[SerializeField]
		private int countTeam;
		[SerializeField]
		private int currentTeam = 1;

		private Unit currentUnit;

		public void Init() {

		}
		private void Awake() {
			instnce = this;
		}
		//Test
		void Start() {
			StartCoroutine(Step());
		}
		//Test
		Unit unitInfo;
		void Update() {
			if (Input.GetMouseButtonDown(1)) {
				unitInfo = GetUnitMouse();
				if (unitInfo) {
					battleHUD.GetInfoUnit(unitInfo, Camera.main.ScreenToViewportPoint(Input.mousePosition));
				}
			}
			if (unitInfo) {
				if (Input.GetMouseButtonUp(1)) {
					battleHUD.ResetInfoUnit();
				}
			}
		}
		IEnumerator Step() {
			battleHUD.SetTeam(currentTeam);
			Debug.Log($"Start step team {currentTeam}");
			yield return new WaitUntil(() => {
				Unit unit = GetUnitMouse();
				if (!unit) return false;
				if (unit.team == currentTeam) {
					if (Input.GetMouseButtonDown(0)) {
						currentUnit = unit;
						Debug.Log(currentUnit.name);
						return true;
					}
				}
				return false;
			});
			currentUnit.Activate();
			battleHUD.SetUnit(currentUnit);
		}
		
		public void EndStep() {
			currentTeam = currentTeam == countTeam ? 1 : currentTeam+1;
			currentUnit.Disactive();
			battleHUD.ResetUnit();
			StartCoroutine(Step());
		}

		#region Static
		public static Vector3? GetPositionMouse() {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if (hit.transform.CompareTag("Terrain")) {
					return hit.point;
				}
			}
			return null;
		}
		public static Unit GetUnitMouse() {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				Unit unit;
				if (hit.transform.TryGetComponent(out unit)) {
					return unit;
				}
			}
			return null;
		}

		public static void SetApHud(int ap) {
			instnce.battleHUD.SetApHud(ap);
		}
		#endregion
	}
}
