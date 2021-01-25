/*
 内容：Web上で選択した家具のidをjsonファイルから取得し，対応する家具を表示するスクリプト
 記述者：荒武
 最終更新日：2021/1/15
*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine.Networking;
using MiniJSON;

public class Furniture_IdGet : MonoBehaviour
{

    //表示する家具を格納する変数
    [SerializeField]
    private GameObject Chair_1;
    [SerializeField]
    private GameObject Chair_2;

    [SerializeField]
    private GameObject Table_1;
    [SerializeField]
    private GameObject Table_2;
    [SerializeField]
    private GameObject Table_3;

   
    private float timeleft;  //秒数を格納する変数

    private GameObject demoSystem;
    private FurnitureControlle furnitureControlle;

    //private string url = "../php/Demo.json";
    private string url = "http://ec2-54-160-49-162.compute-1.amazonaws.com/php/Demo.json"; //サーバにあるjsonファイルのURL
    private string furniture_date;    //取得した家具のidを格納する変数

    void Start()
    {   
        //unity内からオブジェクトとスクリプトを取得する
        demoSystem = GameObject.Find("DemoSystem");
        furnitureControlle = demoSystem.GetComponent<FurnitureControlle>();
    }

    void Update()
    {
        //0.5秒ごとに実行
        timeleft -= Time.deltaTime;
        if (timeleft <= 0.0)
        {
            timeleft = 0.5f;
            //部屋の外に家具がない場合にGetRequest関数を実行する
            if (furnitureControlle.GetSuffer() != false)
            {
                return;
            }
            StartCoroutine(GetRequest(this.url));
        }

    }

    //jsonファイルにアクセスし，furniture_idを取得する
    //取得したfurniture_idに対応する家具を表示する
    private IEnumerator GetRequest(string url)
    {
        Debug.Log("-------- GET Request Start --------");
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("GET Request Failure");
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("GET Request Success");
                Debug.Log(request.downloadHandler.text);
                Dictionary<string, object> response = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
                furniture_date = (string)response["furniture_id"];

            }

            switch (furniture_date)
            {
                case "1":
                    Chair_1.SetActive(true);
                    break;
                case "2":
                    Chair_2.SetActive(true);
                    break;
                case "3":
                    Table_1.SetActive(true);
                    break;
                case "4":
                    Table_2.SetActive(true);
                    break;
                case "5":
                    Table_3.SetActive(true);
                    break;
            }

        }

    }
}