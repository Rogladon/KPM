using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.System;

namespace Battle {
	public abstract class Arrow : MonoBehaviour {
		[SerializeField]
		protected float speed;

		protected Vector3 direction;
		protected Vector3 target;
		protected Vector3 origin;

		private float distance => Vector3.Distance(transform.position, target);
		private bool destroyed = false;

		public Arrow Create(Vector3 origin, Vector3 target) {
			Arrow arrow = BattleSystem.Instantiate(gameObject).GetComponent<Arrow>();
			arrow.origin = origin;
			arrow.target = target;
			arrow.Init();
			return arrow;
		}
		public abstract void Init();

		public virtual float TimeToTargetHit() {
			return distance / speed;
		}
		public IEnumerator WaitTargetHit() {
			yield return new WaitUntil(() => {
				if (!destroyed)
					return distance < 0.1f;
				else
					return true;
			});
		}

		private void LateUpdate() {
			if(distance < 0.99f) {
				destroyed = true;
				Invoke("Destroy", 0.1f);
			}
		}

		protected virtual void Destroy() {
			Destroy(gameObject);
		}
	}
}
