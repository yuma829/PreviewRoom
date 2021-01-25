<!-- 
  内容：デモ画面で家具の検索や選択を行う部分のjavascriptを書いた
  記述者：荒武
  最終更新日：1/15
-->
<?php
session_start();
// require('Header.php');
require('Database.php');
require('Demo_selectfurniture.php');
require('FavoriteAdd.php');
?>
<!DOCTYPE html>
<html>


<?php //jsonファイルのデータを初期化
$array = array(
    "furniture_id" => "0"
);
$json = json_encode($array);
$bytes = file_put_contents("Demo.json", $json);
?>

<head>
    <title>【KUT不動産-PreviewRoom-】デモ</title>
    <meta charset="UTF-8">
    <link rel="stylesheet" type="text/css" href="/css/Demo.css">
    <link rel="stylesheet" type="text/css" href="/css/Stickeyheader.css">
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="../js/HalfWidthNumbers.js"></script>

    <script type="text/javascript">
        //選択した家具を格納する配列
        var furnitureArray = [-1];

        function select(img_id) {
            //家具を選択した場合，選択した家具のidをjsonファイルに格納するPHPを呼び出す
            //XMLHttpRequestを扱うことでページを更新せづにデータを受け取ることができる
            var oReq = new XMLHttpRequest(); //XMLHttpRequestを初期化するコンストラクター
            //oReq.addEventListener("load", reqListener2);  //PHPにアクセスできていれば実行する
            oReq.open("POST", `Demo_selectfurniture.php`); //このPHPを呼び出す
            oReq.setRequestHeader('content-type', 'application/x-www-form-urlencoded;charset=UTF-8');
            oReq.send(`clickfurniture=${img_id}`); //PHPにPOSTする変数をPHPの変数に変換する

            //選択した家具が配列に格納されているかを調べる
            var i = this.furnitureArray.indexOf(img_id);
            //選択した家具が配列に入っていなかった場合，データベースに家具idを格納するPHPを呼び出す
            if (i == -1) {
                furnitureArray.push(img_id);
                var oReq = new XMLHttpRequest();
                //oReq.addEventListener("load", reqListener2);
                oReq.open("POST", `Demo_selectfurniture.php`);
                oReq.setRequestHeader('content-type', 'application/x-www-form-urlencoded;charset=UTF-8');
                oReq.send(`newfurniture=${img_id}`);
            }

        }
        //検索ボタンが押された時に家具の種類と家具のサイズの幅を取得し，PHPを呼び出す
        function furniture_select() {
            var obj = document.getElementById("kagu"); //選択されたオプションを取得する
            var idx = obj.selectedIndex;
            var value = obj.options[idx].value; // オプションからvalue(家具の種類)を取得する

            //入力された値を変数に格納する
            var tatemin = document.getElementById("tatemin").value;
            var tatemax = document.getElementById("tatemax").value;
            var yokomin = document.getElementById("yokomin").value;
            var yokomax = document.getElementById("yokomax").value;

            //変数をPHPの変数に変換し，PHPにPOSTする
            var oReq = new XMLHttpRequest();
            oReq.addEventListener("load", reqListener);
            oReq.open("POST", `Demo_selectfurniture.php`);
            oReq.setRequestHeader('content-type', 'application/x-www-form-urlencoded;charset=UTF-8');
            oReq.send(`selectfurniture=${value}&tatemin=${tatemin}&tatemax=${tatemax}&yokomin=${yokomin}&yokomax=${yokomax}`);
            //PHPを呼び出して帰ってきた結果をId(FFF)の位置に出力する
            function reqListener() {
                var element = document.getElementById("FFF");
                element.innerHTML = this.responseText;
            }
        }
    </script>
</head>

<body style="padding: 20px;">
    <?php require('Header.php'); ?>
    <!--  style="position: relative; top:60px" -->

    <iframe src="http://ec2-54-160-49-162.compute-1.amazonaws.com/MainDemo1/" width="1120" height="616">
    </iframe>

    <div class="search">
        <div class="search_filter">
            <div>家具</div>
            <div>
                <select class="madori_syurui" id="kagu">
                    <option>----------</option>
                    <option value="001">椅子</option>
                    <option value="002">テーブル</option>
                    <option value="003">ベッド</option>
                    <option value="004">洗濯機</option>
                    <option value="005">冷蔵庫</option>
                    <option value="006">電子レンジ</option>
                    <option value="007">収納棚</option>
                    <option value="008">炊飯器</option>
                    <option value="009">カーテン</option>
                    <option value="010">ライト</option>
                </select>
                <input type="submit" class="search_button" value="検索" onclick="furniture_select()"></button>

            </div>
        </div>

        <div class="size">
            <p>サイズ(cm)</p>
            <div class="tate">
                縦幅　<input type="text" name="num_only" id="tatemin">　～　<input type="text" name="num_only" id="tatemax">
            </div>
            <div class="yoko">
                横幅　<input type="text" name="num_only" id="yokomin">　～　<input type="text" name="num_only" id="yokomax">
            </div>
        </div>
        <div class="Furniture" id=FFF>
            <?php
            /* 
            for($i = 0; $i < count($furniture); $i++){
                $picture = $furniture[$i]["picture"];
                    echo "<img src= $picture>";
            };*/
            ?>
        </div>
    </div>
    <div class="box">
        <a href="DetailProperty_html.php">
            <div>物件詳細画面に戻る</div>
        </a>
        <a href="FurnitureList_html.php">
            <div>家具リストを確認する</div>
        </a>
        <div class="AddFavoriteProperty" style="cursor: pointer;">お気に入りに追加する</div>
        <script src="../js/FavoriteAdd.js"></script>
    </div>
</body>

</html>