using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _takeItemsCount = 1;

    private static int SPEED_HASH = Animator.StringToHash("Speed_f");

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (!_agent.isActiveAndEnabled)
            return;

        if (_agent.remainingDistance <= 0.1f)
        {
            var point = RandomNavmeshLocation();
            _agent.SetDestination(RandomNavmeshLocation());
        }
        SetSpeed(Mathf.Clamp(_agent.velocity.magnitude, 0, 0.5f));
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(SPEED_HASH, value);
    }

    public Vector3 RandomNavmeshLocation(float radius = 50f)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            for (int i = 0; i < _takeItemsCount; i++)
            {
                if (player.Inventory.TryRemoveItem(transform))
                {

                }
            }
        }
    }
}
