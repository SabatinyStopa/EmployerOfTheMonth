using Unity.VisualScripting;
using Unity.AI.Navigation;
using UnityEngine.AI;
using UnityEngine;
using System;

namespace EmployerOfTheMonth.AI
{
    public class Customer : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Animator animator;
        public Action OnArriveInDestination;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
            Wander();
        }

        private void Update()
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                OnArriveInDestination?.Invoke();
                Wander();
            }

            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        public void PlayAnimation(string animationToPlay)
        {
            animator.Play(animationToPlay);
        }

        public float AnimationLenght(string animationName)
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == animationName) return clip.length;
            }

            return 0;
        }

        private void Wander()
        {
            var surface = agent.navMeshOwner.GetComponent<NavMeshSurface>();
            var longerSide = surface.size.x >= surface.size.z ? surface.size.x : surface.size.z;

            if (RandomPoint(surface.center, longerSide * 2, out Vector3 position)) agent.SetDestination(position);
        }

        private bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            var randomPoint = center + UnityEngine.Random.insideUnitSphere * range;

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }
}