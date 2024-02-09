using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    Animator myanimator;
    Rigidbody2D myRigidbody;

    //�W�����v�̗�
    private float jumpforce = 600.0f;
    //�i�ޗ�
    private float runforce = 30.0f;
    //���x����
    private float maxRunspeed = 3.0f;
    //�W�����v���x����
    private float damping = -50f;
    private float groundLevel = -1.0f;

    static public bool isEnd = false;

    public GameObject itemEffectPrefab;

    //------------------------------------------------------------------------------------------------------------------
    //�X�^�[�g
    //------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        Application.targetFrameRate = 60;

        this.myanimator = GetComponent<Animator>();

        this.myRigidbody = GetComponent<Rigidbody2D>();        
    }

    //------------------------------------------------------------------------------------------------------------------
    //�A�b�v�f�[�g
    //------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // ���n���Ă��邩�ǂ����𒲂ׂ�
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        myanimator.SetBool("isground", isGround);            

        if (isEnd==false)
        {
            //�v���[���[�̑��x
            float speedx = Mathf.Abs(this.myRigidbody.velocity.x);
          
            if (transform.position.y > -1.77f)
            {
                //�W�����v����
                if (Input.GetKeyDown(KeyCode.Space) && isGround)
                {
                    myRigidbody.AddForce(transform.up * this.jumpforce);
                }

                if (Input.GetKey(KeyCode.Space) == false)
                {
                    if (myRigidbody.velocity.y > 0)
                    {
                        myRigidbody.AddForce(transform.up * this.damping);
                    }
                }
            }

            //���̓L�[�ɉ����Đi�ޕ�����ς���
            int key = 0;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                key = 1;
                myanimator.SetFloat("speed", 1);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                key = -1;
                myanimator.SetFloat("speed", 1);
            }

            //�X�s�[�h����
            if (speedx < this.maxRunspeed)
            {
                this.myRigidbody.AddForce(transform.right * key * this.runforce);
            }

            //���������ɉ����Ĕ��]
            if (key != 0)
            {
                transform.localScale = new Vector3(key * 5, 5, 5);
            }

            if (speedx < 0.01f)
            {
                myanimator.SetFloat("speed", 0);
            }
        }

        //�Q�[���I�[�o�[����
        if(transform.position.y<-7.0f)
        {
            isEnd = true;
            GameObject.Find("Canvas").GetComponent<UIController>().gameOver();
            //���R�������~
            GetComponent<Rigidbody2D>().simulated = false;
            //�Փ˂��Ȃ���
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        
        //�^�C�g���֖߂�
        if(isEnd)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                isEnd = false;
                SceneManager.LoadScene("title");
            }
        }        
    }

    //------------------------------------------------------------------------------------------------------------------
    //�Փ˔���
    //------------------------------------------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�S�[�����B���̏���
        if(other.gameObject.tag=="goal")
        {
            isEnd = true;
            GameObject.Find("Canvas").GetComponent<UIController>().gameClear();
            myanimator.SetFloat("speed", 0);
        }

        //�A�C�e�����莞�̏���
        if(other.gameObject.tag=="cherry")
        {
            myRigidbody.AddForce(transform.right * 500f);
            Destroy(other.gameObject);
            GameObject itemEffect = Instantiate(itemEffectPrefab, this.transform.position, Quaternion.identity);
            Destroy(itemEffect.gameObject, 0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //�G�ɐڐG���̏���
        if(other.gameObject.tag=="enemy")
        {
            isEnd = true;
            GameObject.Find("Canvas").GetComponent<UIController>().gameOver();
            GetComponent<CapsuleCollider2D>().enabled = false;
            myanimator.SetFloat("speed", 0);
        }
    }
}
