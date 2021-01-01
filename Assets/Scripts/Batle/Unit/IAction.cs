using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Batle.UnitCore {
	public interface IAction {
		void OnUpdate();
		void OnAwake();
		void OnStartStep();
		void OnEndStep();
		void OnDestroy();
		void Action();
		bool isActive();
	}
}
