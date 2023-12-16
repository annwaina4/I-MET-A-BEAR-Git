using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bearController : MonoBehaviour
{
    //状態（ステートパターン）
    private int stateNumber = 0;

    //汎用タイマー
    private float timeCounter = 0f;

    private Animator myanimator;

    private Rigidbody2D myRigidbody;

    private GameObject player;

    //private float walkforce = 33.5f;

    private float rightVelocity = 3.0f;

    //------------------------------------------------------------------------------------------------------------------
    //スタート
    //------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        Application.targetFrameRate = 60;

        this.myanimator = GetComponent<Animator>();

        this.myRigidbody = GetComponent<Rigidbody2D>();

        this.player = GameObject.Find("player");

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

        //方向を求める
        float direction = getEulerAngle2D(this.transform.position, player.transform.position);

        //距離を求める
        float length = getLength2D(this.transform.position, player.transform.position);

        //**************************************************************************************************************
        //ここから状態処理
        //**************************************************************************************************************

        //待機
        if (stateNumber == 0)
        {
            //プレーヤーの方向を向く
            //this.transform.rotation = Quaternion.Euler(direction, 0f, 0f);

            //1秒経過
            if (timeCounter > 1.0f)
            {
                //タイマーリセット
                timeCounter = 0f;

                // アニメーション　前進
                this.myanimator.SetFloat("speed", 1.0f);

                //状態の遷移（前進）
                stateNumber = 1;
            }
            //プレーヤーが近い時
            /*else if (length < 2.0f)
            {
                //タイマーリセット
                timeCounter = 0f;

                //アニメーション　攻撃
                this.myanimator.SetTrigger("attack");

                //状態の遷移（攻撃）
                stateNumber = 2;
            }*/

        }

        //前進
        else if (stateNumber == 1)
        {
            //プレーヤーの方向を向く
            //this.transform.rotation = Quaternion.Euler(direction, 0f, 0f);

            //移動
            //this.myRigidbody.AddForce(transform.right * walkforce);
            this.myRigidbody.velocity = transform.right * rightVelocity;

            //5秒経過
            if (timeCounter > 6.0f)
            {
                timeCounter = 0f;

                //アニメーション　待機
                this.myanimator.SetFloat("speed", 0);

                //状態の遷移（待機）
                stateNumber = 0;
            }
            //プレーヤーが近い時
            /*else if (length < 2.0f)
            {
                //タイマーリセット
                timeCounter = 0f;

                //アニメーション　待機
                this.myanimator.SetFloat("speed", 0);

                //アニメーション　攻撃
                this.myanimator.SetTrigger("attack");

                //状態の遷移（攻撃
                stateNumber = 2;
            }*/
        }

        /*else if (stateNumber == 2)
        {
            //攻撃モーション終わり
            if (timeCounter > 1.2f)
            {
                //タイマーリセット
                timeCounter = 0f;

                //状態の遷移（待機）
                stateNumber = 0;
            }
        }*/

        //**************************************************************************************************************
        //ゲーム‐オーバー監視
        //**************************************************************************************************************

        if (playerController.isEnd)
        {
            //アニメーション　待機
            this.myanimator.SetFloat("speed", 0);

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

        if(other.gameObject.tag=="wall")
        {
            myanimator.SetFloat("speed", 1.0f);
            myRigidbody.velocity = transform.right * rightVelocity;
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
