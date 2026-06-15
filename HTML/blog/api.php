<?php
error_reporting(0);
header('Content-Type: application/json; charset=utf-8');
date_default_timezone_set('Asia/Taipei');

// 允許 GitHub Pages 跨網域請求
header('Access-Control-Allow-Origin: https://issiki9401.github.io');
header('Access-Control-Allow-Methods: GET, POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    exit(0);
}

// 資料庫連線設定 (指向學校獨立資料庫主機)
$DB_HOST = 'webdb.nttu.edu.tw';
$DB_NAME = 'd50';
$DB_USER = 'd50'; 
$DB_PASS = '0968578863'; 

try {
    $pdo = new PDO("mysql:host=$DB_HOST;dbname=$DB_NAME;charset=utf8mb4", $DB_USER, $DB_PASS, [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC
    ]);
} catch (PDOException $e) {
    echo json_encode(['success' => false, 'message' => '資料庫連線失敗: ' . $e->getMessage()]);
    exit;
}

$UPLOAD_DIR = 'uploads/';
$action = $_GET['action'] ?? '';

// 圖片上傳功能
if ($action === 'upload') {
    if (!is_dir($UPLOAD_DIR)) {
        if (!@mkdir($UPLOAD_DIR, 0777, true)) {
            echo json_encode(['success' => false, 'message' => '無法建立資料夾']); exit;
        }
    }
    if (isset($_FILES['file']) && $_FILES['file']['error'] === UPLOAD_ERR_OK) {
        $ext = pathinfo($_FILES['file']['name'], PATHINFO_EXTENSION);
        $filename = uniqid() . '.' . $ext;
        $target = $UPLOAD_DIR . $filename;
        if (move_uploaded_file($_FILES['file']['tmp_name'], $target)) {
            $protocol = (!empty($_SERVER['HTTPS']) && $_SERVER['HTTPS'] !== 'off') ? "https://" : "http://";
            $fullUrl = $protocol . $_SERVER['HTTP_HOST'] . dirname($_SERVER['SCRIPT_NAME']) . '/' . $target;
            echo json_encode(['success' => true, 'url' => $fullUrl]); exit;
        }
    }
    echo json_encode(['success' => false, 'message' => '上傳失敗']); exit;
}

$input = json_decode(file_get_contents('php://input'), true);

// 登入驗證功能
if ($action === 'login') {
    $password = $input['password'] ?? '';
    $stmt = $pdo->prepare("SELECT * FROM admin_users WHERE username = 'admin' AND password = ?");
    $stmt->execute([$password]);
    if ($stmt->fetch()) {
        echo json_encode(['success' => true]);
    } else {
        http_response_code(401);
        echo json_encode(['success' => false, 'message' => '密碼錯誤']);
    }
    exit;
}

// 獲取文章列表
if ($action === 'get_posts') {
    $stmt = $pdo->query("SELECT * FROM posts ORDER BY id DESC");
    $posts = $stmt->fetchAll();
    echo json_encode($posts); exit;
}

// 新增文章
if ($action === 'add_post') {
    $title = $input['title'] ?? '';
    $cover = $input['cover'] ?? '';
    $content = $input['content'] ?? '';
    $date = date('Y/m/d');

    $stmt = $pdo->prepare("INSERT INTO posts (title, cover, content, date) VALUES (?, ?, ?, ?)");
    $success = $stmt->execute([$title, $cover, $content, $date]);
    
    echo json_encode(['success' => $success]); exit;
}

// 刪除文章
if ($action === 'delete_post') {
    $deleteId = $input['id'] ?? '';
    $stmt = $pdo->prepare("DELETE FROM posts WHERE id = ?");
    $success = $stmt->execute([$deleteId]);
    
    echo json_encode(['success' => $success]); exit;
}

echo json_encode(['success' => false]);
?>