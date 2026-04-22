// 抓取玻璃導覽列元素
const sidebar = document.getElementById('sidebar');

// 監聽滑鼠在導覽列上方移動的事件
sidebar.addEventListener('mousemove', (e) => {
    // 取得導覽列相對於視窗的座標位置
    const rect = sidebar.getBoundingClientRect();
    
    // 計算滑鼠「相對於導覽列內部」的 X 與 Y 座標
    const x = e.clientX - rect.left;
    const y = e.clientY - rect.top;

    // 將座標寫入 CSS 變數中，CSS 的 radial-gradient 就會自動跟著變動
    sidebar.style.setProperty('--mouse-x', `${x}px`);
    sidebar.style.setProperty('--mouse-y', `${y}px`);
});

// 當滑鼠離開導覽列時，把光效移出畫面外
sidebar.addEventListener('mouseleave', () => {
    sidebar.style.setProperty('--mouse-x', `-100px`);
    sidebar.style.setProperty('--mouse-y', `-100px`);
});