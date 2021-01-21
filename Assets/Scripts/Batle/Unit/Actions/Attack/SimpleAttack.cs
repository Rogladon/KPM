using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.System;

namespace Battle.UnitCore.Actions {
	public class SimpleAttack : Action, IAction {
		[SerializeField]
		protected float _maxDistance;
		[SerializeField]
		protected float _damage;

		Transform area;

		protected float maxDistance => _maxDistance + unit.radius;
		protected int damage => (int)(_damage * unit.unitState.strenght);

		protected Unit target;
		BattleStaticContainer cont => BattleStaticContainer.instance;
		public void Action() {
			if (!target) return;
			if (price > unit.ap) return;
			//TOODOO
			if(Vector3.Distance(target.position, unit.position) < maxDistance) {
				target.Hit(damage);
				Vector3 posLook = target.position;
				posLook.y = unit.position.y;
				unit.transform.LookAt(posLook);
				unit.Action((int)price);
				unit.state.PlaySinglton(nameAnimation);
			}
		}

		public bool isActive() {
			return true;
		}

		public bool isLock() {
			return false;
		}

		public void OnAwake(Unit unit) {
			this.unit = unit;
		}

		public void OnChoiceSelf() {
			BattleSystem.SetApHud((int)price);
			area = Instantiate(cont.circleArea).transform;
			AreaSetPosition();
			area.localScale = Vector3.one * maxDistance*2;
		}
		void AreaSetPosition() {
			Vector3 pos = unit.position;
			pos.y = cont.heightUI;
			area.position = pos;
		}

		public void OnDestroy() {
			
		}

		public void OnEndStep() {
		}

		public void OnResetSelf() {
			Destroy(area.gameObject);
			ResetCursor();
		}

		public void OnStartStep() {
			
		}
		bool isCursorAttack;
		public void OnUpdate() {
			target = BattleSystem.GetUnitMouse(unit.team, UnitFindFlag.Without);
			if (target) {
				AreaSetPosition();
				if (Vector3.Distance(unit.position, target.position) < maxDistance) {
					SetCursor(cont.attackCursor);
				} else {
					SetCursor(cont.noAttackCursor);
				}
			} else {
				ResetCursor();
			}
		}

		void SetCursor(Texture2D cursor) {
			if (isCursorAttack) return;
			Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
			isCursorAttack = true;
		}
		void ResetCursor() {
			if (!isCursorAttack) return;
			Cursor.SetCursor(cont.defaultCursor, Vector2.zero, CursorMode.Auto);
			isCursorAttack = false;
		}
	}
}
