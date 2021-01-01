using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Batle.UnitCore {
	public class Unit : MonoBehaviour {
		public UnitInfo unitInfo;

		public List<IAction> actions { get; private set; }
		//Buffs

		public int hp { get; private set; }
		public int ap { get; private set; }

		private int strenght;
		private int defense;

		private bool isLock = false;
		public bool isActive { get; private set; } = false;

		public IAction currentAction { get; set; }
		public int team { get; private set; }

		public void Init(int team) {
			hp = unitInfo.state.hp;
			ap = unitInfo.state.ap;
			strenght = unitInfo.state.strenght;
			defense = unitInfo.state.defense;

			actions = GetComponents<IAction>().ToList();
			actions.ForEach(p => p.OnAwake());
			this.team = team;
		}

		public void Update() {
			if (isLock) return;
			currentAction.OnUpdate();
		}

		public void Hit(int dmg) {
			hp -= dmg;
		}
		public void Action(int ap) {
			this.ap -= ap;
		}
		public void Death() {
			isLock = true;
			Debug.Log($"Died {name}");
		}

		public void SetAction(int index) {
			currentAction = actions[index];
		}
		public void Activate() {
			isActive = true;
			actions.ForEach(p => p.OnStartStep());
		}
		public void Disactive() {
			isActive = false;
			actions.ForEach(p => p.OnEndStep());
		}
	}
}