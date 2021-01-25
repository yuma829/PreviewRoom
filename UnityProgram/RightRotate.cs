/*
 内容：選択した家具を右に5度回転させるスクリプト
 記述者：荒武
 最終更新日：2020/12/29
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRotate : MonoBehaviour {
    // フィールド
    private GameObject demoSystem;
    private FurnitureControlle furnitureControlle;
    private OverlapJudge overlapJudge;

    // Start is called before the first frame update
    void Start() {
        demoSystem = GameObject.Find("DemoSystem");
        furnitureControlle = demoSystem.GetComponent<FurnitureControlle>();
    }

    //右回転ボタンがクリックされた場合にfurnitureControllスクリプトの変数cubeがnullでない(家具を選択している)場合，
    //選択されている家具を右に5度回転する
    public void OnClick() {
        Debug.Log("Button click!");
        Transform cube = furnitureControlle.GetCube();
        GameObject furniture = furnitureControlle.GetFurniture();
        overlapJudge = furniture.GetComponent<OverlapJudge>();
        if (cube != null) {
            furnitureControlle.SetReturnAngle(-5);
            if (overlapJudge.GetDirection() == 0) {
                cube.Rotate(new Vector3(0, 0, 5));
            }
            else if (overlapJudge.GetDirection() == 1) {
                cube.Rotate(new Vector3(0, 5, 0));
            }
        }
    }
}