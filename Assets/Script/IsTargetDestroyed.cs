using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Custom")]
public class IsTargetDestroyed : ConditionTask
{
    public BBParameter<GameObject> target;

    protected override bool OnCheck()
    {
        return target.value == null;
    }
}
