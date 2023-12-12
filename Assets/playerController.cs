using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    Animator myanimator;
    Rigidbody2D myRigidbody;

    private float jumpforce = 600.0f;
    private float runforce = 40.0f;
    private float jumpDownforce = -3.0f;
    private float runDownforce = -7.0f;
    private float maxRunspeed = 6.0f;
    //�W�����v���x����
    private float dump = -50f;
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
        myRigidbody.AddForce(transform.right * this.runDownforce);
        myRigidbody.AddForce(transform.up * this.jumpDownforce);
        


        if (isEnd==false)
        {
            //�v���[���[�̑��x
            float speedx = Mathf.Abs(this.myRigidbody.velocity.x);

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGround)
            {
                myRigidbody.AddForce(transform.up * this.jumpforce);
            }

            if (Input.GetKey(KeyCode.UpArrow) == false)
            {
                if (myRigidbody.velocity.y > 0)
                {
                    myRigidbody.AddForce(transform.up * this.dump);
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

        if(transform.position.y<-5.0f)
        {
            isEnd = true;
            GameObject.Find("Canvas").GetComponent<UIController>().gameOver();
            //���R�������~
            myRigidbody.velocity = Vector2.zero;
            //�Փ˂��Ȃ���
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        
        if(isEnd)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
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
        }

        if(other.gameObject.tag=="cherry")
        {
            myRigidbody.AddForce(transform.right * 500f);
            Destroy(other.gameObject);
            GameObject itemEffect = Instantiate(itemEffectPrefab, this.transform.position, Quaternion.identity);
            Destroy(itemEffect.gameObject, 0.3f);
        }
    }
}
