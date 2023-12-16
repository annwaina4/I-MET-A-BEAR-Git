using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogController : MonoBehaviour
{
    //状態（ステートパターン）
    private int stateNumber = 0;

    //汎用タイマー
    private float timeCounter = 0f;

    private Animator myanimator;

    private Rigidbody2D myRigidbody;

    //private float walkforce = 33.5f;

    private float goForce = 5f;
    private float jumpForce = 35f;
    private float groundLevel = -1.1f;

    //------------------------------------------------------------------------------------------------------------------
    //スタート
    //------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        Application.targetFrameRate = 60;

        this.myanimator = GetComponent<Animator>();

        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    //------------------------------------------------------------------------------------------------------------------
    //オリジナル関数
    //------------------------------------------------------------------------------------------------------------------

    //距離を求める（2D）
    float getLength2D(Vector2 current, Vector2 target)
    {
        return Mathf.Sqrt(((current.x - target.x) * (current.x - target.x)) + ((current.y - target.y) * (current.y - target.y)));
    }

    //方向を求める（2D） ※オイラー（-180～0～+180)
    float getEulerAngle2D(Vector2 current, Vector2 target)
    {
        Vector3 value = target - current;
        return Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg; //ラジアン→オイラー
    }

    //方向を求める（2D） ※ラジアン
    float getRadian2D(Vector2 current, Vector2 target)
    {
        Vector3 value = target - current;
        return Mathf.Atan2(value.x, value.y);
    }

    //------------------------------------------------------------------------------------------------------------------
    //アップデート
    //------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        //タイマー加算
        timeCounter += Time.deltaTime;
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        //**************************************************************************************************************
        //ここから状態処理
        //**************************************************************************************************************

        //左ジャンプ待機
        if (stateNumber == 0)
        {
            // アニメーション待機
            this.myanimator.SetBool("isGround", isGround);
            //1秒経過
            if (timeCounter > 1.0f)
            {
                //タイマーリセット
                timeCounter = 0f;

                //反転
                transform.localScale = new Vector3(5, 5, 5);

                //状態の遷移（右反転）
                stateNumber = 1;
            }
        }

        //右反転
        else if (stateNumber == 1)
        {
            this.myanimator.SetBool("isGround", isGround);
            //1秒経過
            if (timeCounter > 1.0f)
            {
                timeCounter = 0f;

                //状態の遷移（右ジャンプ待機）
                stateNumber = 2;
            }
        }

        //右反転
        else if (stateNumber == 2)
        {
            //this.myRigidbody.velocity = new Vector2(-4.5f, -4.5f);
            myRigidbody.AddForce(transform.right * -goForce);
            myRigidbody.AddForce(transform.up * jumpForce);
            // アニメーション　ジャンプ
            this.myanimator.SetBool("isGround", isGround);

            //0.5秒経過
            if (timeCounter > 0.5f)
            {
                timeCounter = 0f;

                //状態の遷移（右ジャンプ待機）
                stateNumber = 3;
            }
        }

        //右ジャンプ待機
        else if (stateNumber == 3)
        {
            this.myanimator.SetBool("isGround", isGround);
            //0.5秒経過
            if (timeCounter > 1.0f)
            {
                //タイマーリセット
                timeCounter = 0f;

                //反転
                transform.localScale = new Vector3(-5, 5, 5);

                //状態の遷移（左反転）
                stateNumber = 4;
            }
        }

        //右反転
        else if (stateNumber == 4)
        {
            // アニメーション　ジャンプ
            this.myanimator.SetBool("isGround", isGround);

            //0.5秒経過
            if (timeCounter > 1.0f)
            {
                timeCounter = 0f;

                //状態の遷移（右ジャンプ待機）
                stateNumber = 5;
            }
        }

        //左反転
        else if (stateNumber == 5)
        {
            //this.myRigidbody.velocity = new Vector2(4.5f, 4.5f);
            myRigidbody.AddForce(transform.right * goForce);
            myRigidbody.AddForce(transform.up * jumpForce);
            // アニメーション　ジャンプ
            this.myanimator.SetBool("isGround", isGround);

            //0.5秒経過
            if (timeCounter > 0.5f)
            {
                timeCounter = 0f;

                //状態の遷移（左ジャンプ待機）
                stateNumber = 0;
            }
        }


        //**************************************************************************************************************
        //ゲーム‐オーバー監視
        //**************************************************************************************************************

        if (playerController.isEnd)
        {
            //アニメーション　待機
            this.myanimator.SetBool("isGround", isGround);

            //ステートパターンを停止
            stateNumber = -1;
        }

    }

    //------------------------------------------------------------------------------------------------------------------
    //衝突判定
    //------------------------------------------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("OnCollisionEnter:" + other.gameObject.tag);

        if (other.gameObject.tag == "Player")
        {
            //myanimator.SetFloat("speed", 0);
        }

        //Debug.Log("残りHP" + this.HP);

        /*if (nowHP <= 0)
        {
            this.myanimator.SetBool("death", true);

            //ステートパターンを停止
            stateNumber = -1;

            //自由落下を停止
            myRigidbody.useGravity = false;
            //衝突をなくす
            GetComponent<CapsuleCollider>().enabled = false;

            //3秒後に破棄
            Destroy(this.gameObject, 3.0f);
        }*/
    }
}
