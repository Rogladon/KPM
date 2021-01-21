using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Battle.System;

namespace Battle.UnitCore.Actions {
	class DistanceAttack : Action, IAction{
		[SerializeField]
		float _maxDistance;
		[SerializeField]
		float _damage;
		[SerializeField]
		Arrow arrow;

		Transform area;

		protected float maxDistance => _maxDistance + unit.radius;
		protected int damage => (int)(_damage * unit.unitState.strenght);

		protected Unit target;
		BattleStaticContainer cont => BattleStaticContainer.instance;

		bool _isLock = false;
		public void Action() {
			if (!target) return;
			if (price > unit.ap) return;
			//TOODOO
			if (Vector3.Distance(target.position, unit.position) < maxDistance) {
				unit.transform.LookAt(target.position);
				StartCoroutine(Attack(target));
				unit.Action((int)price);
			}
		}

		private IEnumerator Attack(Unit target) {
			_isLock = true;
			yield return unit.state.WaitPlaySinglton(nameAnimation);
			Debug.Log("Distance Attack animation complete");
			if (a == null) {
				a = arrow.Create(unit.position, target.position);
				
			}
			yield return a.WaitTargetHit();
			target.Hit(damage);
			a = null;
			_isLock = false;
		}
		private Arrow a = null;
		private void CreateArrow() {
			a = arrow.Create(unit.position+Vector3.up, target.position);
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
			BattleSystem.SetApHud((int)price);
			area = Instantiate(cont.circleArea).transform;
			AreaSetPosition();
			area.localScale = Vector3.one * maxDistance*2;
			unit.state.animEvent.AddFunc("shoot", CreateArrow);
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
