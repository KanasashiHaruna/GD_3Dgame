using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    [Header("�e������")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] LineRenderer lineRenderer;
    
    public void Initialize(LineRenderer lineRenderer)
    {
        this.lineRenderer = lineRenderer;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lineRenderer != null)
        {
            //lineRenderer�̏I�_
            Vector3 endPosition = lineRenderer.GetPosition(1);
        
            //lineRenderer�̏I�_�Ɍ������ē���
            Vector3 direction=(endPosition-transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            lineRenderer.SetPosition(0,transform.position);

            //endPosition�̈ʒu�قړ����ɂȂ�����Destroy���܂�
            if(Vector3.Distance(transform.position, endPosition) < 0.1f)
            {
                Destroy(gameObject);
                Destroy(lineRenderer.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
           
        }
    }
}
