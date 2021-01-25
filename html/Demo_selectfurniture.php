<?php
/*
 内容：検索された家具をデータベースからもってくるPHP
 内容：選択された家具が初めて選択された場合にデータベースに追加するPHP
 内容：家具が選択された場合にjsonファイルに家具idを格納するPHP
 記述者：荒武
 最終更新日：2021/01/11
*/

session_start();
require('Database.php');
/*
家具の種類を選択し，検索ボタンが押された場合に該当する家具をデータベースで検索しする
サイズを指定した場合はその範囲内の家具を検索するが，何も指定されなかった場合，
下限は0，上限は1000が指定される
*/
if(isset($_POST['selectfurniture'])){
    try {
        $sql = "SELECT * FROM furniture WHERE furniture_type_tag = :furniture_type_tag
                AND horizontal BETWEEN :yokomin AND :yokomax
                AND vertical BETWEEN :tatemin AND :tatemax";

        $furniture_stmt = $pdo->prepare($sql);
        $param = array('furniture_type_tag' => $_POST['selectfurniture'],
                'tatemin' => $_POST['tatemin'] != "" ? $_POST['tatemin']: 0,  
                'tatemax' => $_POST['tatemax'] != "" ? $_POST['tatemax']: 1000,
                'yokomin' => $_POST['yokomin'] != "" ? $_POST['yokomin']: 0,
                'yokomax' => $_POST['yokomax'] != "" ? $_POST['yokomax']: 1000,
                );
        $furniture_stmt->execute($param);
        $furniture = $furniture_stmt->fetchALL(PDO::FETCH_ASSOC);
    } catch (PDOException $e) {
        echo $e-$_GETLine() . PHP_EOL . " - " . $e-$_GETMessage();
    }
    //変数furnitureに格納されている家具データの数分，echo関数を実行して家具の写真を表示する
    //家具の選択をonclick関数で取得し，select関数を呼び出す
    for($i = 0; $i < count($furniture); $i++){
        $picture = $furniture[$i]["picture"];
        $picture_id = $furniture[$i]["furniture_id"];
        echo "<img src= $picture width=\"139\" height = \"141\" onclick = \"select($picture_id)\">";
    }
//選択した家具が初めて選択された家具であった場合に実行
//$_SESSIONに格納されているuser_idとproperty_idを主キーとして配列に家具を格納する
}else if(isset($_POST['newfurniture'])){
    try {
        $sql = "INSERT INTO demo(user_id, property_id, room_id, furniture_id) VALUES (:user_id,:property_id,:room_id,:furniture_id)";
        $demo_stmt = $pdo->prepare($sql);
        $param = array(':user_id' => $_SESSION['user_id'], ':property_id' => $_SESSION['property_id'], ':room_id' => 1, ':furniture_id' => $_POST['newfurniture']);
        $demo_stmt->execute($param);
    } catch (PDOException $e) {
        echo("ooksoakkdoakg");
        echo $e->getLine() . PHP_EOL . " - " . $e->getMessage();
    }
}
//家具を選択した場合に実行
//選択された家具のfurniture_idをjsonファイルに格納する(1つだけ)
if(isset($_POST['clickfurniture'])){
    $furniture  =  $_POST['clickfurniture'];
    $array = array(
        "furniture_id" => $furniture
    );
    $json = json_encode($array);
    $bytes = file_put_contents("Demo.json", $json);
}


