/*
 内容：デモ画面のPlayerの操作を行うスクリプト
 記述者：荒武
 最終更新日：2021/1/7
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlle : MonoBehaviour {
    //内見カメラの親オブジェクトを格納する変数
    public Transform PlayerTransform;
    //内見カメラを格納する変数
    public Transform CameraTransform;
    //public GameObject Camera;
    private float rest;
    // 初期位置
    private Vector3 defaultPosition;

    //内見カメラの初期値を代入する
    void Start() {
        defaultPosition = new Vector3(-9.6f, 2.3f, -0.56f);
        PlayerTransform.position = defaultPosition;
    }

    void Update() {
        //マウスの差表を変数に格納
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");

        //視野のy座標が行き過ぎないよう上限と下限を設定する
        rest = CameraTransform.transform.localEulerAngles.x;
        if (rest > 340 && rest < 360 || rest > 0 && 20 > rest) {
            CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
        }
        else {
            if (rest > 300) {
                if (Input.GetAxis("Mouse Y") < 0) {
                    CameraTransform.transform.Rotate(-Y_Rotation * 2, 0, 0);
                }
            }
            else {
                if (Input.GetAxis("Mouse Y") > 0) {
                    CameraTransform.transform.Rotate(-Y_Rotation * 2, 0, 0);
                }
            }
        }

        //マウスのx座標に合わせて内見カメラの親オブジェクトをy座標を変更する
        PlayerTransform.transform.Rotate(0, X_Rotation * 1.5f, 0);

        //内見カメラの角度を変数に格納する
        float angleDir = PlayerTransform.transform.eulerAngles.y * (Mathf.PI / 180.0f);

        //押されたキーに合わせて内見カメラを動かす
        Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));
        Vector3 dir2 = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir));
        if (Input.GetKey(KeyCode.D)) {
            PlayerTransform.transform.position += dir1 * 3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W)) {
            PlayerTransform.transform.position += dir2 * 3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            PlayerTransform.transform.position += -dir2 * 3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            PlayerTransform.transform.position += -dir1 * 3 * Time.deltaTime;
        }
    }

    // 初期位置に戻す
    public void setDefaultTransform() {
        this.PlayerTransform.position = this.defaultPosition;
    }
}