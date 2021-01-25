/*
 内容：他の家具や壁と衝突した際にエラーメッセージを表示するファイル
 記述者：中谷
 最終更新日：2021/1/11
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlapJudge : MonoBehaviour {
    // フィールド
    // 家具及び部屋の壁の当たり判定と重なった場合に表示するテキスト
    [SerializeField]
    private GameObject overlapErrorText;
    // FurnitureControlleスクリプトを参照する
    private FurnitureControlle furnitureControlle;
    // オブジェクトが回転する方向を判断する
    [SerializeField]
    private int direction;

    // Start is called before the first frame update
    void Start() {
        furnitureControlle = GameObject.Find("DemoSystem").GetComponent<FurnitureControlle>();
    }

    // 他のオブジェクトの当たり判定に重なった場合にエラーメッセージを表示する
    public void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "Furniture" || col.gameObject.tag == "Property") {
            overlapErrorText.SetActive(true);
            furnitureControlle.SetIsOverlapping(true);
        }
    }

    // 重なった当たり判定から抜けた場合にエラーメッセージを非表示にする
    public void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Furniture" || col.gameObject.tag == "Property") {
            overlapErrorText.SetActive(false);
            furnitureControlle.SetIsOverlapping(false);
        }
    }

    // 家具を回転させた際に他のオブジェクトと衝突した場合に家具の角度も戻す
    void OnCollisionStay(Collision Collision) {
        if(Collision.gameObject.tag == "Furniture" || Collision.gameObject.tag == "Property") {
            furnitureControlle.Removed();
        }
    }

    public int GetDirection() {
        return this.direction;
    }
}