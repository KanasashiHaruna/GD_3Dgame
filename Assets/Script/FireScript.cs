using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    [Header("弾を撃つ")]
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
            //lineRendererの終点
            Vector3 endPosition = lineRenderer.GetPosition(1);
        
            //lineRendererの終点に向かって動く
            Vector3 direction=(endPosition-transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            lineRenderer.SetPosition(0,transform.position);

            //endPositionの位置ほぼ同じになったらDestroyします
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
