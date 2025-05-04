using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Custom")]
public class MoveToPlayerWhenTargetDestroyed : ActionTask
{
    public BBParameter<GameObject> target;
    public BBParameter<GameObject> player;
    public float moveSpeed = 3f;

    private bool targetDestroyed = false;

    protected override void OnUpdate()
    {
        if (!targetDestroyed)
        {
            if (target.value == null)
            {
                targetDestroyed = true;
            }
            else
            {
                // Target masih ada, idle saja
                EndAction(false); 
                return;
            }
        }

        // Target sudah destroy, mulai bergerak ke player
        if (player.value != null)
        {
            Vector3 direction = (player.value.transform.position - agent.transform.position).normalized;
            agent.transform.position += direction * moveSpeed * Time.deltaTime;
        }

        EndAction(false); // Biar task ini tetap jalan tiap frame (loop)
    }
}
