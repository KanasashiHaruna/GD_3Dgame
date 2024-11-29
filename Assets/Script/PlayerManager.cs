using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("�v���C���[�̈ړ��֘A")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Vector3 distance;
    [SerializeField] Rigidbody rb;
    [SerializeField] private float angleSpeed = 4.0f;

    [Header("�e�����֘A")]
    [SerializeField] BulletScript bullet;
    [SerializeField] private float shotInterval = 1.0f;
    [SerializeField] private float lastShotTime = 0.0f;

    [Header("�����q����֘A")]
    [SerializeField] LineRenderer lineRenderer;
    private List<LineRenderer> lineRenderers=new List<LineRenderer>();
    private List<Transform> enemyTransforms = new List<Transform>();
    private Vector3 playerPosition;
    private Vector3 enemyposition;
    //[SerializeField] private Transform shotPosition;


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        #region ����
        Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100.0f))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 direction=(targetPosition - transform.position).normalized;
            Quaternion lookRotation=Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angleSpeed);
        }
        #endregion

        //�ړ�------------------------
        Move();

        //�e����----------------------
        if (Input.GetMouseButton(0))
        {
            Shot();
        }

        playerPosition = transform.position;
        UpdateLineRenderers();
    }


    #region �ړ�
    private void Move()
    {
        distance = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            distance = Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            distance = Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            distance = Vector3.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            distance = Vector3.left;
        }

        rb.velocity = distance * moveSpeed;
    }
    #endregion

    #region �e������
    private void Shot()
    {
        if (Time.time >= lastShotTime + shotInterval)
        {
            BulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);
            obj.SetDirection(transform.forward);
            obj.Initialize(this);
            lastShotTime = Time.time;
        }
    }
    #endregion

    #region lineRenderer��start��end�̈ʒu������
    public void DrawLine(Vector3 enemypos, Transform enemyTransform)
    {
        LineRenderer newLineRenderer = Instantiate(lineRenderer, transform.position, Quaternion.identity);
        newLineRenderer.positionCount = 2;
        newLineRenderer.SetPosition(0, playerPosition); //�v���C���[�̈ʒu
        newLineRenderer.SetPosition(1, enemypos);       //�G�̈ʒu
        newLineRenderer.startWidth = 0.05f;
        newLineRenderer.endWidth = 0.05f;

        lineRenderers.Add(newLineRenderer);             //���X�g�ɒǉ�
        enemyTransforms.Add(enemyTransform);
    }
    #endregion

    #region Renderer�̈ʒu���X�V
    private void UpdateLineRenderers()
    {
        if (lineRenderers.Count == enemyTransforms.Count)
        {
            for (int i = 0; i < lineRenderers.Count; i++)
            {
                lineRenderers[i].SetPosition(0, transform.position);
                lineRenderers[i].SetPosition(1, enemyTransforms[i].position);
            }
        }
        //else
        //{
        //    Debug.LogError("LineRenderers��EnemyTransforms�̃��X�g���������Ă��܂���I");
        //}
    }
    #endregion
}
