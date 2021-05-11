using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] GameObject go_RicochetEffect;
    [SerializeField] int damage;




    Rigidbody m_rigid = null;
    Transform m_tfTarget = null;

    [SerializeField] float m_speed = 0f;
    float m_currentSpeed = 0f;
    [SerializeField] LayerMask m_layerMask = 0;
    [SerializeField] ParticleSystem m_psEffect = null;

    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        StartCoroutine(LaunchDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_tfTarget != null)
        {
            if (m_currentSpeed <= m_speed)
                m_currentSpeed += m_speed * Time.deltaTime;
            transform.position += transform.up * m_currentSpeed * Time.deltaTime;

            Vector3 t_dir = (m_tfTarget.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, t_dir, 0.1f);
        }
    }

    void SearchEnemy()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 100f, m_layerMask);

        if (t_cols.Length > 0 )
        {
            m_tfTarget = t_cols[Random.Range(0, t_cols.Length)].transform; 
        }
    }

    IEnumerator LaunchDelay()
    {
        yield return new WaitWhile(() => m_rigid.velocity.y < 0f);
        yield return new WaitForSeconds(0.1f);

        SearchEnemy();
        m_psEffect.Play();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ContactPoint contactPoint = collision.contacts[0];
        //var clone = Instantiate(go_RicochetEffect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        if (collision.transform.CompareTag("Player2"))
        {
            Debug.Log("게임오버");
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
        }
    }
}
