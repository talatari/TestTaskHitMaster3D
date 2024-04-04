using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Codebase.Players
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform[] _wayPoints;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private int _currentPointIndex;

        private void Update()
        {
            if (_navMeshAgent.isActiveAndEnabled && _navMeshAgent.isOnNavMesh)
                _navMeshAgent.SetDestination(_wayPoints[_currentPointIndex].position);

            // if (_wayPoints[_wayPoints.Length - 1].position == _navMeshAgent.transform.position)
            // {
            //     print("Finished");
            //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // }
        }

        public void Run()
        {
            if (_currentPointIndex < _wayPoints.Length - 1)
                SetNextPoint();
        }

        private void SetNextPoint()
        {
            _currentPointIndex++;
        }
    }
}