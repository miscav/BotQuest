using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{
    public Transform playerTransform;
    NavMeshAgent agent;
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = playerTransform.position;
    }


}
