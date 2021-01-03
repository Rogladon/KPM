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
				if(value <= 0) {
					_hp = 0;
					Death();
				}
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
		public bool isActive {
			get {
				return _isActive && !isDeath;
			}
			set {
				_isActive = value;
			}
		}
		private bool _isActive;
		public bool isDeath { get; private set; }

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
		public float radius {
			get {
				return GetComponent<CapsuleCollider>().radius;
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
			if (currentAction != null) {
				currentAction.OnUpdate();
				if (Input.GetMouseButtonDown(0)) {
					currentAction.Action();
				}
			}
		}

		public void OnStartStep() {
			HeelAp();
			actions.ForEach(p => p.OnStartStep());
		}
		public void OnEndStep() {
			if(currentAction != null)
				currentAction.OnResetSelf();
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
			isDeath = true;
			//TOODOO
			agent.enabled = false;
			transform.Rotate(new Vector3(90, 0, 0));
		}
		public void HeelAp() {
			ap += heelAp;
		}

		public void SetAction(int index) {
			if(currentAction != null)
				currentAction.OnResetSelf();
			currentAction = actions[index];
			currentAction.OnChoiceSelf();
		}
		public void SetAction(IAction action) {
			currentAction = action;
			currentAction.OnChoiceSelf();
		}
		public void Activate() {
			isActive = true;
			currentAction = null;
		}
		public void Disactive() {
			isActive = false;
		}
	}
}