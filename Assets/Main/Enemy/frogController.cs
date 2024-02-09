using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogController : MonoBehaviour
{
    //��ԁi�X�e�[�g�p�^�[���j
    private int stateNumber = 0;

    //�ėp�^�C�}�[
    private float timeCounter = 0f;

    private Animator myanimator;

    private Rigidbody2D myRigidbody;

    //�W�����v�̗�
    private float goForce = 5f;
    private float jumpForce = 35f;
    
    private float groundLevel = -1.1f;

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
        // ���n���Ă��邩�ǂ����𒲂ׂ�
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        //**************************************************************************************************************
        //���������ԏ���
        //**************************************************************************************************************

        //���W�����v�ҋ@1
        if (stateNumber == 0)
        {
            // �A�j���[�V�����ҋ@
            this.myanimator.SetBool("isGround", isGround);
            //1�b�o��
            if (timeCounter > 1.0f)
            {
                //�^�C�}�[���Z�b�g
                timeCounter = 0f;

                //���]
                transform.localScale = new Vector3(5, 5, 5);

                //��Ԃ̑J�ځi���W�����v�ҋ@2�j
                stateNumber = 1;
            }
        }

        //���W�����v�ҋ@2
        else if (stateNumber == 1)
        {
            this.myanimator.SetBool("isGround", isGround);
            //1�b�o��
            if (timeCounter > 1.0f)
            {
                timeCounter = 0f;

                //��Ԃ̑J�ځi���W�����v�j
                stateNumber = 2;
            }
        }

        //���W�����v
        else if (stateNumber == 2)
        {
            myRigidbody.AddForce(transform.right * -goForce);
            myRigidbody.AddForce(transform.up * jumpForce);
            // �A�j���[�V�����@�W�����v
            this.myanimator.SetBool("isGround", isGround);

            //0.5�b�o��
            if (timeCounter > 0.5f)
            {
                timeCounter = 0f;

                //��Ԃ̑J�ځi�E�W�����v�ҋ@1�j
                stateNumber = 3;
            }
        }

        //�E�W�����v�ҋ@1
        else if (stateNumber == 3)
        {
            this.myanimator.SetBool("isGround", isGround);
            //1�b�o��
            if (timeCounter > 1.0f)
            {
                //�^�C�}�[���Z�b�g
                timeCounter = 0f;

                //���]
                transform.localScale = new Vector3(-5, 5, 5);

                //��Ԃ̑J�ځi�E�W�����v�ҋ@2�j
                stateNumber = 4;
            }
        }

        //�E�W�����v�ҋ@2
        else if (stateNumber == 4)
        {
            // �A�j���[�V�����@�W�����v
            this.myanimator.SetBool("isGround", isGround);

            //1�b�o��
            if (timeCounter > 1.0f)
            {
                timeCounter = 0f;

                //��Ԃ̑J�ځi�E�W�����v�j
                stateNumber = 5;
            }
        }

        //�E�W�����v
        else if (stateNumber == 5)
        {
            myRigidbody.AddForce(transform.right * goForce);
            myRigidbody.AddForce(transform.up * jumpForce);
            // �A�j���[�V�����@�W�����v
            this.myanimator.SetBool("isGround", isGround);

            //0.5�b�o��
            if (timeCounter > 0.5f)
            {
                timeCounter = 0f;

                //��Ԃ̑J�ځi���W�����v�ҋ@1�j
                stateNumber = 0;
            }
        }

        //**************************************************************************************************************
        //�Q�[���]�I�[�o�[�Ď�
        //**************************************************************************************************************

        if (playerController.isEnd)
        {
            //�A�j���[�V�����@�ҋ@
            this.myanimator.SetBool("isGround", isGround);

            //�X�e�[�g�p�^�[�����~
            stateNumber = -1;
        }
    }
}
