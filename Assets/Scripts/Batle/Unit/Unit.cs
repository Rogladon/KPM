using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

namespace Battle.UnitCore {
	public class Unit : MonoBehaviour {
		public UnitInfo unitInfo;

		public List<IAction> actions { get; private set; }
		//Buffs

		public int hp { get; private set; }
		public int ap { get; private set; }

		private int strenght;
		private int defense;

		private bool isLock => currentAction != null ?currentAction.isLock():false;
		public bool isActive { get; private set; } = false;

		public IAction currentAction { get; set; }
		public int team;

		public Vector3 position {
			get {
				return transform.position;
			}
			set {
				transform.position = new Vector3(value.x, transform.position.y, value.z);
			}
		}
		public NavMeshAgent agent { get; private set; }
		//Test
		private void Start() {
			Init(team);
		}
		//Test

		public void Init(int team) {
			agent = GetComponent<NavMeshAgent>();
			hp = unitInfo.state.hp;
			ap = unitInfo.state.ap;
			strenght = unitInfo.state.strenght;
			defense = unitInfo.state.defense;

			actions = GetComponents<IAction>().ToList();
			actions.ForEach(p => p.OnAwake(this));
			SetAction(0);
			this.team = team;
		}

		public void Update() {
			if (isLock) return;
			if (!isActive) return;
			currentAction.OnUpdate();
			if (Input.GetMouseButtonDown(0)) {
				currentAction.Action();
			}
			if (Input.GetMouseButtonDown(1)) {
				SetAction(0);
			}
		}

		public void Hit(int dmg) {
			hp -= dmg;
		}
		public void Action(int ap) {
			this.ap -= ap;
		}
		public void Death() {
			Debug.Log($"Died {name}");
		}

		public void SetAction(int index) {
			currentAction = actions[index];
			currentAction.OnChoiceSelf();
		}
		public void Activate() {
			isActive = true;
			actions.ForEach(p => p.OnStartStep());
			SetAction(0);
		}
		public void Disactive() {
			isActive = false;
			actions.ForEach(p => p.OnEndStep());
			currentAction.OnResetSelf();
		}
	}
}