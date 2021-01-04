using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.System {
	public class CameraControll : MonoBehaviour {
		[SerializeField]
		float zoomSpeed;
		[SerializeField]
		float speed;
		[SerializeField]
		float angularSpeed;
		[SerializeField]
		float angularMin;
		[SerializeField]
		float angularMax;

		private Vector3 deltaPos;
		private Vector3 pos;
		void Update() {
			if(Input.mouseScrollDelta != Vector2.zero) {
				transform.position += transform.forward * Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
			}
			if (Input.GetMouseButtonDown(1)) {
				pos = Input.mousePosition;
				deltaPos = Vector3.zero;
			}
			if (Input.GetMouseButton(1)) {
				Vector3 euler = transform.eulerAngles;
				euler.x += deltaPos.y * Time.deltaTime * angularSpeed;
				euler.y += -deltaPos.x * Time.deltaTime * angularSpeed;
				if (euler.x < angularMin)
					euler.x = angularMin;
				if (euler.x > angularMax)
					euler.x = angularMax;
				transform.eulerAngles = euler;
				//transform.RotateAround(Vector3.up, -deltaPos.x*Time.deltaTime*angularSpeed);
				//transform.RotateAround(transform.right, deltaPos.y * Time.deltaTime * angularSpeed);

				//if(transform.eulerAngles.x < angularMin) {
				//	Vector3 euler = transform.eulerAngles
				//	transform.eulerAngles = 
				//}
				deltaPos = pos - Input.mousePosition;
				pos = Input.mousePosition;
			}
			Vector2 viewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			if(viewport.x <= 0) {
				transform.position += -transform.right * speed * Time.deltaTime;
			}
			if (viewport.x >= 1) {
				transform.position += transform.right * speed * Time.deltaTime;
			}
			if(viewport.y >= 1) {
				Vector3 direction = transform.forward;
				direction.y = 0;
				direction = direction.normalized;
				transform.position += direction * speed * Time.deltaTime;
			}
			if (viewport.y <= 0) {
				Vector3 direction = -transform.forward;
				direction.y = 0;
				direction = direction.normalized;
				transform.position += direction * speed * Time.deltaTime;
			}
		}
	}
}
