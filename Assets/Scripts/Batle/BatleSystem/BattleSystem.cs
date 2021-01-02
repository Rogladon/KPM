using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.UnitCore;

namespace Battle.System {
	public class BattleSystem : MonoBehaviour {

		[SerializeField]
		BattleHUD battleHUD;
		[SerializeField]
		private int countTeam;
		[SerializeField]
		private int currentTeam = 1;

		private Unit currentUnit;

		public void Init() {

		}
		//Test
		void Start() {
			StartCoroutine(Step());
		}
		//Test
		IEnumerator Step() {
			battleHUD.SetTeam(currentTeam);
			Debug.Log($"Start step team {currentTeam}");
			yield return new WaitUntil(() => {
				Unit unit = ChoiceTarget();
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

		private bool ChoiceUnit() {
			//Выбор юнита можно перенести в корутину
			return true;
		}
		public static Vector3? ChoicePosition() {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				if (hit.transform.CompareTag("Terrain")) {
					return hit.point;
				}
			}
			return null;
		}
		public static Unit ChoiceTarget() {
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
		public void EndStep() {
			currentTeam = currentTeam == countTeam ? 1 : currentTeam+1;
			currentUnit.Disactive();
			battleHUD.ResetUnit();
			StartCoroutine(Step());
		}
	}
}
