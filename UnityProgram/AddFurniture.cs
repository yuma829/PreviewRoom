/*
 内容：家具配置画面を表示するカメラとデモ画面を表示するカメラを切り替えるスクリプト
 記述者：荒武
 最終更新日：2020/11/15
*/

//using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;
//using MySql.Data;
using MySql.Data.MySqlClient;
//using UnityEngine.Networking;

public class AddFurniture : MonoBehaviour
{
    // フィールド
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

    private User_idGet user_id;
    private GameObject demoSystem;
    private FurnitureControlle furnitureControlle;

    private string SERVER = "database-2.crjrdcbc45me.us-east-1.rds.amazonaws.com";
    private string DATABASE = "group10";
    private string USERID = "admin";
    private string PORT = "3306";
    private string PASSWORD = "softwaregroup10";
    private string connCmd = "";
    private List<object> furnitureList = new List<object>();
    private float timeleft;
    MySqlConnection conn;


    void Start()
    {
        connCmd =
             "server=" + SERVER + ";" +
             "database=" + DATABASE + ";" +
             "userid=" + USERID + ";" +
             "port=" + PORT + ";" +
             "password=" + PASSWORD;

        demoSystem = GameObject.Find("DemoSystem");
        user_id = GameObject.Find("DemoSystem").GetComponent<User_idGet>();
        furnitureControlle = demoSystem.GetComponent<FurnitureControlle>();
        
    }


    void Update()
    {
        //だいたい1秒ごとに処理を行う
        timeleft -= Time.deltaTime;
        if (timeleft <= 0.0)
        {
            timeleft = 0.1f;

            if (furnitureControlle.GetSuffer() != false)
            {
                return;
            }
            /*
                if(user_id.userId == "")
                {
                   return;
                }
            */
            conn = new MySqlConnection(connCmd);
            //try
            //{
                Debug.Log("MySQLと接続中...");
                conn.Open();
                Debug.Log("ok1");

                string sql1 = "SELECT furniture_id FROM demo";
                MySqlCommand cmd = new MySqlCommand(sql1, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                Debug.Log("ok2");

                while (rdr.Read())
                {

                    int num = furnitureList.IndexOf(rdr[0]);
                    if (num > -1)
                    {
                        continue;
                    }
                    else
                    {
                        furnitureList.Add(rdr[0]);
                        Debug.Log("ok3");

                        switch (rdr.GetString(0))
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
                rdr.Close();
           // }
           // catch (Exception ex)
           // {
           //     Debug.Log(ex.ToString());
           // }
            conn.Close();
            Debug.Log("接続を終了しました");
        }
    }
}