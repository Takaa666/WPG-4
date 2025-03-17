using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


[Category("Custom")]
public class CheckTaskStep : ConditionTask
{
    public BBParameter<int> currentTaskStep;
    public int requiredStep;

    protected override bool OnCheck()
    {
        return currentTaskStep.value == requiredStep;
    }
}
