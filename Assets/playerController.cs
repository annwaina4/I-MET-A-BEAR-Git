using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    Animator myanimator;
    Rigidbody2D myRigidbody;

    private float jumpforce = 600.0f;
    private float runforce = 30.0f;
    private float maxRunspeed = 2.5f;
    //�W�����v���x����
    private float damping = -50f;
    private float groundLevel = -1.0f;

    //float timeCounter = 0;
    //float span = 1.0f;

    static public bool isEnd = false;

    public GameObject itemEffectPrefab;
    
    void Start()
    {
        Application.targetFrameRate = 60;

        this.myanimator = GetComponent<Animator>();

        this.myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        //timeCounter += Time.deltaTime;
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        myanimator.SetBool("isground", isGround);            

        if (isEnd==false)
        {
            //�v���[���[�̑��x
            float speedx = Mathf.Abs(this.myRigidbody.velocity.x);

            if (transform.position.y > -1.77f)
            {
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

        if(transform.position.y<-7.0f)
        {
            isEnd = true;
            GameObject.Find("Canvas").GetComponent<UIController>().gameOver();
            //���R�������~
            GetComponent<Rigidbody2D>().simulated = false;
            //�Փ˂��Ȃ���
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        
        if(isEnd)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                isEnd = false;
                SceneManager.LoadScene("SampleScene");
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="goal")
        {
            isEnd = true;
            GameObject.Find("Canvas").GetComponent<UIController>().gameClear();
            myanimator.SetFloat("speed", 0);
        }

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
        if(other.gameObject.tag=="enemy")
        {
            isEnd = true;
            GameObject.Find("Canvas").GetComponent<UIController>().gameOver();
            GetComponent<CapsuleCollider2D>().enabled = false;
            myanimator.SetFloat("speed", 0);
        }
    }
}
