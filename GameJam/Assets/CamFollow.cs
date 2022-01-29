using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Transform m_FollowTarget;
    private void Awake()
    {
        m_FollowTarget = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        transform.position = new Vector3(m_FollowTarget.position.x,m_FollowTarget.position.y, -10);
    }
}
