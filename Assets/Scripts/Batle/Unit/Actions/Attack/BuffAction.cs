using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Battle.System;

namespace Battle.UnitCore.Actions {
	public class BuffAction : Action, IAction {
		private Unit target;
		bool _isLock = false;
		public void Action() {
			if (target) {
				StartCoroutine(Buff());
			}
		}

		protected IEnumerator Buff() {
			_isLock = true;
			yield return unit.state.WaitPlaySinglton(nameAnimation);
			var buffs = GetComponentsInChildren<IBuff>();
			unit.AddBuff(buffs.ToList());
			unit.Action((int)price);
			_isLock = false;
		}

		public bool isActive() {
			return false;
		}

		public bool isLock() {
			return _isLock;
		}

		public void OnAwake(Unit unit) {
			this.unit = unit;
		}

		public void OnChoiceSelf() {
			
		}

		public void OnDestroy() {
			
		}

		public void OnEndStep() {
			
		}

		public void OnResetSelf() {
			
		}

		public void OnStartStep() {
			
		}

		public void OnUpdate() {
			target = BattleSystem.GetUnitMouse(unit.team, UnitFindFlag.Without);
		}
	}
}
