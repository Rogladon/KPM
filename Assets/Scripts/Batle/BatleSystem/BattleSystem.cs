using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.UnitCore;
using System.Linq;

namespace Battle.System {
	public enum UnitFindFlag {
		With,
		Without
	}
	public class BattleSystem : MonoBehaviour {
		private static BattleSystem instnce;

		[SerializeField]
		BattleHUD battleHUD;
		[SerializeField]
		private int countTeam;
		[SerializeField]
		private int currentTeam = 1;

		private Unit currentUnit;

		[SerializeField]
		private List<Unit> AllUnits;

		public void Init() {

		}
		private void Awake() {
			instnce = this;
		}
		//Test
		void Start() {
			StartCoroutine(Step());
			AllUnits = FindObjectsOfType<Unit>().ToList();
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

			AllUnits.Where(p => p.team == currentTeam).ToList().
				ForEach(p => p.OnStartStep());
			yield return new WaitUntil(() => {
				Unit unit = GetUnitMouse(currentTeam, UnitFindFlag.With);
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
			AllUnits.Where(p => p.team == currentTeam).ToList().
				ForEach(p => p.OnEndStep());
			currentTeam = currentTeam == countTeam ? 1 : currentTeam+1;
			if(currentUnit)
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
		public static Unit GetUnitMouse(bool death = false) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				Unit unit;
				if (hit.transform.TryGetComponent(out unit)) {
					if(!(!death && unit.isDeath))
						return unit;
				}
			}
			return null;
		}
		public static Unit GetUnitMouse(int team, UnitFindFlag flag, bool death = false) {
			Unit unit = GetUnitMouse(death);
			if (unit == null) return null;
			if(flag == UnitFindFlag.With) {
				if (unit.team != team)
					unit = null;
			} 
			if(flag == UnitFindFlag.Without) {
				if (unit.team == team)
					unit = null;
			}
			if (unit)
				unit.HoverOn();
			return unit;
		}

		public static void SetApHud(int ap) {
			instnce.battleHUD.SetApHud(ap);
		}
		#endregion
	}
}
