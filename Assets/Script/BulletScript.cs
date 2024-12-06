using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("íeÇåÇÇ¬")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] Rigidbody rb;


    private Vector3 direction;
    public PlayerManager player;
    // Start is called before the first frame update

    
    void Start()
    {
        
    }

    public void Initialize(PlayerManager pl)
    {
        player = pl;
    }
    // Update is called once per frame
    void Update()
    {
       // rb.velocity=direction*speed;
    }

    //void FixedUpdate() { rb.velocity = direction * speed; }

    public void SetDirection(Vector3 dir)
    {
        rb.velocity = dir*speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy != null && enemy.isHit == false)
            {
                //Debug.Log("íeÇ∆ìGÇ™ìñÇΩÇ¡ÇΩÇÊ");
                Vector3 enemyPosition = collision.transform.position;
                player.DrawLine(enemyPosition, collision.transform);
                enemy.isHit = true;
            }
            Destroy(gameObject);
        }

        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    //Debug.Log("ï«Ç…è’ìÀÇµÇΩ");
        //    //rb.velocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
        //    //direction = reflecDIr;
        //}
    }
 

    
}
