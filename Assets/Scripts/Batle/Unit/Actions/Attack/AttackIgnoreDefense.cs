using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.UnitCore.Actions {
	public class AttackIgnoreDefense : SimpleAttack{
		new int damage => (int)(_damage*unit.unitState.strenght+(_damage * unit.unitState.strenght)* ((float)target.unitState.defense / 100f));
	}
}
