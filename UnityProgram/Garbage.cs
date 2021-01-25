/*
 内容：選択した家具を削除するスクリプト
 記述者：荒武
 最終更新日：2020/12/19
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour　{
    // フィールド
    private GameObject demoSystem;
    private FurnitureControlle furnitureControlle;
    private GameObject notSufferObject;
    private NotSuffer notSuffer;

    // Start is called before the first frame update
    void Start() {
        demoSystem = GameObject.Find("DemoSystem");
        furnitureControlle = demoSystem.GetComponent<FurnitureControlle>();
        notSufferObject = GameObject.Find("SufferObject");
        notSuffer = notSufferObject.GetComponent<NotSuffer>();
    }

    //ゴミ箱ボタンがクリックされた場合にfurnitureControllスクリプトの変数cubeがnullでない(家具を選択している)場合，
    //furnitureControllスクリプト内の変数furnitureを非表示にする
    public void OnClick() {
        Debug.Log("Button click!");
        Transform cube = furnitureControlle.GetCube();
        GameObject furniture = furnitureControlle.GetFurniture();
        if (cube != null) {
            if(furnitureControlle.SufferBool()) {
                notSuffer.OnTriggerExit(furniture.GetComponent<Collider>());
            }
            furniture.SetActive(false);
        }
        /*
        if (furnitureControlle.cube != null) {
            Destroy(furnitureControlle.furniture);
        }
        */
    }
}