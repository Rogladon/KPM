using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle {
	public class SimpleArrow : Arrow {
		public override void Init() {
			transform.position = origin;
			transform.LookAt(target);
			direction = (target - origin).normalized;
		}
		public void Update() {
			transform.position += direction * speed * Time.deltaTime;
		}
	}
}
