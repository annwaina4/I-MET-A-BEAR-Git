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

    //ジャンプの力
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
        // 着地しているかどうかを調べる
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        //**************************************************************************************************************
        //ここから状態処理
        //**************************************************************************************************************

        //左ジャンプ待機1
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

                //状態の遷移（左ジャンプ待機2）
                stateNumber = 1;
            }
        }

        //左ジャンプ待機2
        else if (stateNumber == 1)
        {
            this.myanimator.SetBool("isGround", isGround);
            //1秒経過
            if (timeCounter > 1.0f)
            {
                timeCounter = 0f;

                //状態の遷移（左ジャンプ）
                stateNumber = 2;
            }
        }

        //左ジャンプ
        else if (stateNumber == 2)
        {
            myRigidbody.AddForce(transform.right * -goForce);
            myRigidbody.AddForce(transform.up * jumpForce);
            // アニメーション　ジャンプ
            this.myanimator.SetBool("isGround", isGround);

            //0.5秒経過
            if (timeCounter > 0.5f)
            {
                timeCounter = 0f;

                //状態の遷移（右ジャンプ待機1）
                stateNumber = 3;
            }
        }

        //右ジャンプ待機1
        else if (stateNumber == 3)
        {
            this.myanimator.SetBool("isGround", isGround);
            //1秒経過
            if (timeCounter > 1.0f)
            {
                //タイマーリセット
                timeCounter = 0f;

                //反転
                transform.localScale = new Vector3(-5, 5, 5);

                //状態の遷移（右ジャンプ待機2）
                stateNumber = 4;
            }
        }

        //右ジャンプ待機2
        else if (stateNumber == 4)
        {
            // アニメーション　ジャンプ
            this.myanimator.SetBool("isGround", isGround);

            //1秒経過
            if (timeCounter > 1.0f)
            {
                timeCounter = 0f;

                //状態の遷移（右ジャンプ）
                stateNumber = 5;
            }
        }

        //右ジャンプ
        else if (stateNumber == 5)
        {
            myRigidbody.AddForce(transform.right * goForce);
            myRigidbody.AddForce(transform.up * jumpForce);
            // アニメーション　ジャンプ
            this.myanimator.SetBool("isGround", isGround);

            //0.5秒経過
            if (timeCounter > 0.5f)
            {
                timeCounter = 0f;

                //状態の遷移（左ジャンプ待機1）
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
}
