using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections.Generic;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Custom")]
    public class MoveToTargetsWithAnimation : ActionTask<Transform>
    {
        public BBParameter<List<Transform>> targetTransforms;  // Daftar target
        public BBParameter<float> speed = 5f;                  // Kecepatan tunggal
        public BBParameter<string> animationName;              // Nama animasi
        public BBParameter<int> currentTaskStep;               // Task step saat ini
        public int stepToComplete;                             // Step untuk menyelesaikan task
        private Animator animator;                             // Animator karakter
        private Transform target;                              // Target saat ini

        protected override void OnExecute()
        {
            animator = agent.GetComponent<Animator>();
            if (animator != null && !string.IsNullOrEmpty(animationName.value))
            {
                animator.Play(animationName.value);
            }

            FindClosestTarget();  // Cari target terdekat
        }

        protected override void OnUpdate()
        {
            if (target == null)
            {
                EndAction(false);  // Hentikan aksi jika tidak ada target
                return;
            }

            // Pindahkan agent menuju target dengan kecepatan yang ditentukan
            agent.position = Vector3.MoveTowards(agent.position, target.position, speed.value * Time.deltaTime);

            // Rotasi agent menghadap target
            Vector3 direction = (target.position - agent.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                agent.rotation = Quaternion.Slerp(agent.rotation, lookRotation, Time.deltaTime * speed.value);
            }

            // Jika jarak sudah dekat, aksi selesai
            if (Vector3.Distance(agent.position, target.position) < 0.1f)
            {
                currentTaskStep.value = stepToComplete;
                EndAction(true);
            }
        }

        // Mencari target terdekat dari daftar
        private void FindClosestTarget()
        {
            float closestDistance = Mathf.Infinity;
            Transform closestTarget = null;

            // Iterasi semua target dalam list
            foreach (Transform potentialTarget in targetTransforms.value)
            {
                float distance = Vector3.Distance(agent.position, potentialTarget.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = potentialTarget;
                }
            }

            // Set target terdekat
            target = closestTarget;
        }
    }
}
