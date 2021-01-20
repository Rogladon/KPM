using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Battle.UnitCore {
	public interface IBuff {
		void OnAwake(Unit unit);
		void OnStartStep();
		void OnActive();
		void OnEndStep();
		void OnUpdate();
		void OnDestroy();
	}
}
