/*
 内容：部屋の外に家具があるかどうかを検知し，エラー文を表示したり関数を呼び出すスクリプト
 記述者：荒武
 最終更新日：2021/1/5
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotSuffer : MonoBehaviour {
    //家具が部屋の外にある場合に表示する分を格納する変数
    [SerializeField]
    private GameObject sufferErrorText;

    // FurnitureControlleスクリプトを参照する
    private FurnitureControlle furnitureControlle;

    void Start() {
        furnitureControlle = GameObject.Find("DemoSystem").GetComponent<FurnitureControlle>();
    }

    //部屋の外にある家具を検知する専用のオブジェクトに家具が衝突した場合，エラー文を表示し，関数を呼び出す．
    public void OnTriggerStay(Collider col) {
        furnitureControlle.SufferAdd(col);
        sufferErrorText.SetActive(true);
        furnitureControlle.SetSuffer(true);
        // furnitureControlle.suffer = true;
    }

    //部屋の外にある家具を検知する専用のオブジェクトから家具が離れた場合，エラー文を非表示にし，関数を呼び出す．
    public void OnTriggerExit(Collider col) {
        furnitureControlle.SufferRemoved(col);
        sufferErrorText.SetActive(false);
        furnitureControlle.SetSuffer(false);
        // furnitureControlle.suffer = false;
    }
}