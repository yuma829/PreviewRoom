/*
 内容：家具の移動や他の家具や壁との重なりを判定するスクリプト
 記述者：荒武
 最終更新日：2021/1/11
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureControlle : MonoBehaviour {
    // フィールド
    private Plane plane;
    private GameObject furniture;
    private bool isGrabbing;
    // マウスで選択したオブジェクトの位置を格納
    private Transform cube; 
    // 他の家具および壁と衝突した際に戻す角度
    private int returnAngle;
    // マウスで掴んだオブジェクトのboxColliderを格納する変数
    private BoxCollider boxCollider;
    // マウスでオブジェクトを掴んだ際の初期位置の座標を格納する変数
    private Vector3 hitPosition;
    // マウスで掴んでいるオブジェクトが他のオブジェクトと重なっているかどうかを判定する変数
    private bool isOverlapping;
    // マウスでオブジェクトを掴んだ瞬間かどうかの判定
    private bool grabMoment;
    // 家具が初期位置にすでにあるかを判断する
    private bool suffer;
    // 部屋の外にあるオブジェクトを格納するリスト
    private List<string> sufferList = new List<string>();
    // クリックした家具のOverlapJudgeスクリプトを取得
    private OverlapJudge overlapJudge;

    // Start is called before the first frame update
    void Start() {
        plane = new Plane(Vector3.up, Vector3.up);
        isOverlapping = false;
        grabMoment = false;
        suffer = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //マウスの位置の直線状を変数rayに格納する
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                //rayにオブジェクトが当たった際にそのオブジェクトが家具であった場合，家具と家具のtransformを変数に格納する
                if (hit.collider.tag == "Furniture") {
                    isGrabbing = true;
                    furniture = hit.collider.gameObject;
                    cube = hit.transform;
                    // 掴んでいるオブジェクトのBoxColliderを取得
                    boxCollider = hit.collider.gameObject.GetComponent<BoxCollider>();
                    // 掴んでいる間はisTriggerをtrueにしてすり抜けるようにする
                    boxCollider.isTrigger = true;
                    if (!grabMoment) {
                        hitPosition = hit.collider.gameObject.transform.position;
                        grabMoment = true;
                    }
                }
            }
         }

        // オブジェクトを掴んでいる場合
        if (isGrabbing) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            plane.Raycast(ray, out rayDistance);
            //オブジェクトの位置をマウスの位置に変更する
            cube.position = ray.GetPoint(rayDistance);

            //マウスの左クリックを離した場合
            if (Input.GetMouseButtonUp(0)) {
                isGrabbing = false;
                // BoxColliderをのisTriggerをfalseにすることですり抜けないようにする
                boxCollider.isTrigger = false;
                grabMoment = false;
                // isOverlappingがtrueなら持ち上げていたオブジェクトを初期位置に戻す
                if (isOverlapping) {
                    cube.position = hitPosition;
                }
            }
        } 
    }

    // furnitureの値を他のスクリプトから取得する
    public GameObject GetFurniture () {
        return this.furniture;
    }

    // cubeの値を他のスクリプトから取得する
    public Transform GetCube () {
        return this.cube;
    }

    // cubeの値を他のスクリプトから取得する
    public bool GetSuffer () {
        return this.suffer;
    }

    // returnAngleの値を他のスクリプトから変更する
    public void SetReturnAngle (int angle) {
        this.returnAngle = angle;
    }

    // sufferの値を他のスクリプトから変更する
    public void SetSuffer (bool b) {
        this.suffer = b;
    }

    // isOverlappingの値を他のスクリプトから変更する
    public void SetIsOverlapping (bool b) {
        this.isOverlapping = b;
    }

    //家具を回転させた際に他の家具や壁に衝突した場合に回転した方向とは逆に5度回転させる
    public void Removed() {
        overlapJudge = furniture.GetComponent<OverlapJudge>();
        if (overlapJudge.GetDirection() == 0) {
            cube.Rotate(new Vector3(0, 0, returnAngle));
        }
        else if (overlapJudge.GetDirection() == 1) {
            cube.Rotate(new Vector3(0, returnAngle, 0));
        }
    }

    //部屋の外にある家具をリストに格納する
    public void SufferAdd(Collider col) {
        sufferList.Add(col.gameObject.name);
    }

    //家具が部屋の中に移動もしくは非表示になった場合にリストに格納する
    public void SufferRemoved(Collider col) {
        sufferList.Remove(col.gameObject.name);
    }

    //選択された家具が部屋の外にある場合はtrueを部屋の中にある場合はfalseを返す
    public bool SufferBool() {
        if (sufferList.Contains(furniture.name)) {
            return true;
        } else {
            return false;
        }
    }
}