using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bearController : MonoBehaviour
{
    //��ԁi�X�e�[�g�p�^�[���j
    private int stateNumber = 0;

    //�ėp�^�C�}�[
    private float timeCounter = 0f;

    private Animator myanimator;

    private Rigidbody2D myRigidbody;

    private GameObject player;

    //private float walkforce = 33.5f;

    private float rightVelocity = 3.0f;

    //------------------------------------------------------------------------------------------------------------------
    //�X�^�[�g
    //------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        Application.targetFrameRate = 60;

        this.myanimator = GetComponent<Animator>();

        this.myRigidbody = GetComponent<Rigidbody2D>();

        this.player = GameObject.Find("player");

    }

    //------------------------------------------------------------------------------------------------------------------
    //�I���W�i���֐�
    //------------------------------------------------------------------------------------------------------------------

    //���������߂�i2D�j
    float getLength2D(Vector2 current, Vector2 target)
    {
        return Mathf.Sqrt(((current.x - target.x) * (current.x - target.x)) + ((current.y - target.y) * (current.y - target.y)));
    }

    //���������߂�i2D�j ���I�C���[�i-180�`0�`+180)
    float getEulerAngle2D(Vector2 current, Vector2 target)
    {
        Vector3 value = target - current;
        return Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg; //���W�A�����I�C���[
    }

    //���������߂�i2D�j �����W�A��
    float getRadian2D(Vector2 current, Vector2 target)
    {
        Vector3 value = target - current;
        return Mathf.Atan2(value.x, value.y);
    }

    //------------------------------------------------------------------------------------------------------------------
    //�A�b�v�f�[�g
    //------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        //�^�C�}�[���Z
        timeCounter += Time.deltaTime;

        //���������߂�
        float direction = getEulerAngle2D(this.transform.position, player.transform.position);

        //���������߂�
        float length = getLength2D(this.transform.position, player.transform.position);

        //**************************************************************************************************************
        //���������ԏ���
        //**************************************************************************************************************

        //�ҋ@
        if (stateNumber == 0)
        {
            //�v���[���[�̕���������
            //this.transform.rotation = Quaternion.Euler(direction, 0f, 0f);

            //1�b�o��
            if (timeCounter > 1.0f)
            {
                //�^�C�}�[���Z�b�g
                timeCounter = 0f;

                // �A�j���[�V�����@�O�i
                this.myanimator.SetFloat("speed", 1.0f);

                //��Ԃ̑J�ځi�O�i�j
                stateNumber = 1;
            }
            //�v���[���[���߂���
            /*else if (length < 2.0f)
            {
                //�^�C�}�[���Z�b�g
                timeCounter = 0f;

                //�A�j���[�V�����@�U��
                this.myanimator.SetTrigger("attack");

                //��Ԃ̑J�ځi�U���j
                stateNumber = 2;
            }*/

        }

        //�O�i
        else if (stateNumber == 1)
        {
            //�v���[���[�̕���������
            //this.transform.rotation = Quaternion.Euler(direction, 0f, 0f);

            //�ړ�
            //this.myRigidbody.AddForce(transform.right * walkforce);
            this.myRigidbody.velocity = transform.right * rightVelocity;

            //5�b�o��
            if (timeCounter > 6.0f)
            {
                timeCounter = 0f;

                //�A�j���[�V�����@�ҋ@
                this.myanimator.SetFloat("speed", 0);

                //��Ԃ̑J�ځi�ҋ@�j
                stateNumber = 0;
            }
            //�v���[���[���߂���
            /*else if (length < 2.0f)
            {
                //�^�C�}�[���Z�b�g
                timeCounter = 0f;

                //�A�j���[�V�����@�ҋ@
                this.myanimator.SetFloat("speed", 0);

                //�A�j���[�V�����@�U��
                this.myanimator.SetTrigger("attack");

                //��Ԃ̑J�ځi�U��
                stateNumber = 2;
            }*/
        }

        /*else if (stateNumber == 2)
        {
            //�U�����[�V�����I���
            if (timeCounter > 1.2f)
            {
                //�^�C�}�[���Z�b�g
                timeCounter = 0f;

                //��Ԃ̑J�ځi�ҋ@�j
                stateNumber = 0;
            }
        }*/

        //**************************************************************************************************************
        //�Q�[���]�I�[�o�[�Ď�
        //**************************************************************************************************************

        if (playerController.isEnd)
        {
            //�A�j���[�V�����@�ҋ@
            this.myanimator.SetFloat("speed", 0);

            //�X�e�[�g�p�^�[�����~
            stateNumber = -1;
        }

    }

    //------------------------------------------------------------------------------------------------------------------
    //�Փ˔���
    //------------------------------------------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("OnCollisionEnter:" + other.gameObject.tag);

        if(other.gameObject.tag=="wall")
        {
            myanimator.SetFloat("speed", 1.0f);
            myRigidbody.velocity = transform.right * rightVelocity;
        }

        //Debug.Log("�c��HP" + this.HP);

        /*if (nowHP <= 0)
        {
            this.myanimator.SetBool("death", true);

            //�X�e�[�g�p�^�[�����~
            stateNumber = -1;

            //���R�������~
            myRigidbody.useGravity = false;
            //�Փ˂��Ȃ���
            GetComponent<CapsuleCollider>().enabled = false;

            //3�b��ɔj��
            Destroy(this.gameObject, 3.0f);
        }*/
    }
}
