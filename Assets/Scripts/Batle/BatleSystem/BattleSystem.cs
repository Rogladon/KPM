using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Batle.UnitCore;

namespace Batle.System {
	public class BattleSystem : MonoBehaviour {

		[SerializeField]
		BattleHUD battleHUD;

		private int countTeam;

		private int currentTeam = 1;

		private Unit currentUnit;

		public void Init() {

		}

		void Start() {
			StartCoroutine(Step());
		}

		IEnumerator Step() {
			yield return new WaitUntil(ChoiceUnit);
			currentUnit.Activate();
			battleHUD.SetUnit(currentUnit);
		}

		private bool ChoiceUnit() {
			//Выбор юнита можно перенести в корутину
			return true;
		}
		public static Vector3? ChoicePosition() {
			//Поиск позиции свободной
			return null;
		}
		public static Unit ChoiceTarget() {
			//поиск юнита
			return null;
		}
		public void EndStep() {
			currentTeam = currentTeam == countTeam ? 1 : currentTeam++;
			StartCoroutine(Step());
		}
	}
}
