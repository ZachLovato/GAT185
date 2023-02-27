using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChar : MonoBehaviour
{
	[SerializeField] Animator animator;

	private Camera mainCamera;
	private NavMeshAgent m_Agent;
	private Transform m_Target;
	[SerializeField] private GameObject m_TargetGameObject;

	private State state = State.IDLE;
	private float timer = 0;
	enum State
	{
		IDLE,
		PATROL,
		CHASE,
		ACTION,
		DEATH
	}

	private void Start()
	{
		m_Target = GameObject.FindGameObjectWithTag("Player")?.transform;

		GetComponent<RollerHealth>().onDeath += OnDeath;

		mainCamera = Camera.main;
		m_Agent = GetComponent<NavMeshAgent>();
	}
	private void Update()
	{

		switch (state)
		{
			case State.IDLE:
				state = State.PATROL;
				break;
			case State.PATROL:
				m_Agent.isStopped = false;
				m_Target = GetComponent<WayPointNav>().wayPoint.transform;
				break;
			case State.CHASE:
				m_Agent.isStopped = false;
				
				break;
			case State.ACTION:
				m_Agent.isStopped = true;
				break;
			case State.DEATH:
				m_Agent.isStopped = true;
				break;
			default:
				break;
		}
		m_Agent.SetDestination(m_Target.position);
		animator.SetFloat("Speed", m_Agent.velocity.magnitude);


	}

	void OnDeath()
	{
		StartCoroutine(Death());
	}

	IEnumerator Death()
	{
		animator.SetTrigger("isDead");
		yield return new WaitForSeconds(4.0f);
		Destroy(gameObject);
	}
}
