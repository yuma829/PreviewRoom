/*
 内容：家具配置画面を表示するカメラとデモ画面を表示するカメラを切り替えるスクリプト
 記述者：荒武
 最終更新日：2020/11/15
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControll : MonoBehaviour { 
    // フィールド
    private GameObject mainCamera;  //家具配置画面のカメラのオブジェクトデータを格納する変数
    private GameObject subCamera;   //内見画面のカメラのオブジェクトデータを格納する変数
    private GameObject subCanvas;   //内見画面時に表示されるUIを格納するオブジェクトデータを格納する変数
    private GameObject mainCanvas;  //家具配置画面時に表示されるUIを格納するオブジェクトデータを格納する変数

    

    private EventSystem eventSystem;
    //private Camera camera;          

    //レイキャストを使うための変数の宣言
    private Ray ray;
    private RaycastHit hit;
    private GameObject selectedGameObject;

    private PlayerControlle playerControlle;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        mainCamera = GameObject.Find("Main Camera");
        subCamera = GameObject.Find("Sub Camera");
        mainCanvas = GameObject.Find("mainCanvas");
        subCanvas = GameObject.Find("subCanvas");
        playerControlle = subCamera.GetComponent<PlayerControlle>();

        //サブカメラを非アクティブにする
        subCamera.SetActive(false);
        subCanvas.SetActive(false);
    }

        // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            selectedGameObject = eventSystem.currentSelectedGameObject;
            //内見画面遷移ボタンがクリックされた場合
            //家具配置カメラと家具配置時UIを非表示にして，内見カメラと内見時UIを表示する，
            if (selectedGameObject.tag == "ItemListButton1") { 
                mainCamera.SetActive(false);
                mainCanvas.SetActive(false);
                subCamera.SetActive(true);
                subCanvas.SetActive(true);
                playerControlle.setDefaultTransform();
            }
            //家具配置画面遷移ボタンがクリックされた場合
            //内見カメラと内見時UIを非表示にして，家具配置カメラと家具配置時UIを表示する
            else if (selectedGameObject.tag == "ItemListButton2") { 
                subCamera.SetActive(false);
                subCanvas.SetActive(false);
                mainCamera.SetActive(true);
                mainCanvas.SetActive(true);
            }
        }
    }
}