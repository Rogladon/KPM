using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Battle.System;

namespace Battle.UnitCore.Actions {
	public class BuffAction : Action, IAction {
		public enum Type {
			self,
			enemy,
			friendly,
			all
		}
		[SerializeField]
		Type type;
		private Unit target;
		bool _isLock = false;
		public void Action() {
			if (price > unit.ap) return;
			if (target) {
				StartCoroutine(Buff());
				unit.HoverOut();
				unit.ResetAllAction();
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
			return true;
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
			switch (type) {
				case Type.self:
					target = unit;
					unit.HoverOn();
					break;
				case Type.enemy:
					target = BattleSystem.GetUnitMouse(unit.team, UnitFindFlag.Without);
					break;
				case Type.friendly:
					target = BattleSystem.GetUnitMouse(unit.team, UnitFindFlag.With);
					break;
				case Type.all:
					target = BattleSystem.GetUnitMouse();
					break;
			}
			
		}
	}
}
