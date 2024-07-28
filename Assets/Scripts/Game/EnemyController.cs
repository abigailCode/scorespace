using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _playerDetectionDistance;
    GameObject player;
    Vector3 _destination;
    NavMeshAgent agent;
    Coroutine currentCoroutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");

        if (agent != null && player != null)
        {
            currentCoroutine = StartCoroutine(Patrol());
        }
    }

    void SetRandomDestination()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * 10f, out hit, 10f, NavMesh.AllAreas))
        {
            _destination = hit.position;
            if (agent != null && agent.enabled) agent.SetDestination(_destination);
        }
    }

    IEnumerator Patrol()
    {
        SetRandomDestination();
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= _playerDetectionDistance)
            {
                if (currentCoroutine != null) StopCoroutine(currentCoroutine);
                currentCoroutine = StartCoroutine(Attack());
                yield break;
            }
            if (Vector3.Distance(transform.position, _destination) < 1.5f)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
                SetRandomDestination();
            }
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            _destination = player.transform.position;
            if (agent != null && agent.enabled) agent.SetDestination(_destination);

            if (Vector3.Distance(transform.position, player.transform.position) > _playerDetectionDistance + 1)
            {
                if (currentCoroutine != null) StopCoroutine(currentCoroutine);
                currentCoroutine = StartCoroutine(Patrol());
                yield break;
            }
            yield return null;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (GetComponent<Collider>().tag == "Player")
    //    {
    //        GameObject.Find("Pointer").GetComponent<HPController>().DecrementHp(20f);
    //    }

    //}
}
