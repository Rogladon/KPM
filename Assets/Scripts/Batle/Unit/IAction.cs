using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.UnitCore {
	public interface IAction {
		void OnUpdate();
		void OnAwake(Unit unit);
		void OnStartStep();
		void OnEndStep();
		void OnDestroy();
		void Action();
		void OnChoiceSelf();
		void OnResetSelf();
		bool isActive();
		bool isLock();
	}
}
