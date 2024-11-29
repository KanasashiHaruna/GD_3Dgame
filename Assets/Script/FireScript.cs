using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    [Header("íeÇåÇÇ¬")]
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
            //lineRendererÇÃèIì_
            Vector3 endPosition = lineRenderer.GetPosition(1);
        
            Vector3 direction=(endPosition-transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            lineRenderer.SetPosition(0,transform.position);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(lineRenderer.gameObject);
            Destroy(gameObject);
        }
    }
}
