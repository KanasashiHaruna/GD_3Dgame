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

    [Header("�΂��o�Ă���֘A")]
    [SerializeField] private float timeLastShot = 0.0f;
    [SerializeField] private float shotTime = 2.0f;
    [SerializeField] private bool readyFire = false;
    private LineRenderer currentLine;
    private Transform currentTransform;
    [SerializeField] FireScript fire;

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

        //�΂��o���邩�ǂ���----------
        if(readyFire)
        {
            timeLastShot += Time.deltaTime;
            if(timeLastShot>=shotTime)
            {
                FireBulletShot(currentLine);
                readyFire = false;
                timeLastShot = 0.0f;
            }
        }
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
        currentLine = Instantiate(lineRenderer, transform.position, Quaternion.identity);
        currentLine.positionCount = 2;
        currentLine.SetPosition(0, playerPosition); //�v���C���[�̈ʒu
        currentLine.SetPosition(1, enemypos);       //�G�̈ʒu
        currentLine.startWidth = 0.05f;
        currentLine.endWidth = 0.05f;

        lineRenderers.Add(currentLine);             //���X�g�ɒǉ�
        enemyTransforms.Add(enemyTransform);

        currentTransform=enemyTransform;
        readyFire = true;
    }
    #endregion

    #region Renderer�̈ʒu���X�V
    private void UpdateLineRenderers()
    {
        if (lineRenderers.Count == enemyTransforms.Count)
        {
            for (int i = 0; i < lineRenderers.Count; i++)
            {
                if (lineRenderers[i] != null && enemyTransforms[i] != null)
                {
                    lineRenderers[i].SetPosition(0, transform.position);
                    lineRenderers[i].SetPosition(1, enemyTransforms[i].position);
                }
            }
        }
    }
    #endregion

    #region �΂̐���
    private void FireBulletShot(LineRenderer lineRenderer)
    {
        FireScript obj = Instantiate(fire, transform.position, Quaternion.identity);
        obj.Initialize(lineRenderer);
    }
    #endregion
}
