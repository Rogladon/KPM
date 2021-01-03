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

		public int hp {
			get {
				return _hp;
			}
			private set {
				_hp = value <= unitInfo.state.hp ? value : unitInfo.state.hp;
			}
		}
		private int _hp;
		public int ap {
			get {
				return _ap;
			}
			private set {
				_ap = value <= unitInfo.state.ap ? value : unitInfo.state.ap;
			}
		}
		private int _ap;
		public int heelAp { get; private set; }

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
			heelAp = unitInfo.state.heelAp;
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

		public void OnStartStep() {
			HeelAp();
			actions.ForEach(p => p.OnStartStep());
		}
		public void OnEndStep() {
			actions.ForEach(p => p.OnEndStep());
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
		public void HeelAp() {
			ap += heelAp;
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