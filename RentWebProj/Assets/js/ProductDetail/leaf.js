
// google map
function initMap() {
    // The location of Uluru
    const uluru = { lat: 24.7609326, lng: 120.952988 };
    // The map, centered at Uluru
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 14,
        center: uluru,
    });
    // The marker, positioned at Uluru
    const marker = new google.maps.Marker({
        position: uluru,
        map: map,
    });
}


///////////////////// leaf js
// 初始化地圖
let map = L.map('mapId', {
    center: [24.7609326, 120.952988],
    zoom: 15
});

// 設定圖資來源
let osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
let osm = new L.TileLayer(osmUrl, { minZoom: 5, maxZoom: 25 });
map.addLayer(osm);

//總部座標設定
let marker_1 = L.marker([24.7609326, 120.952988]).addTo(map);
let marker_2 = L.marker([24.8020852, 120.9727511]).addTo(map);
let marker_3 = L.marker([24.8034877, 121.0309443]).addTo(map);


//圓圈設定
let circle = L.circle([24.7609326, 120.952988], {
    color: 'orange',
    fillColor: '#ADADAD',
    fillOpacity: 0.3,
    radius: 300
}).addTo(map);

//彈跳視窗
marker_1.bindPopup(`<b>歡迎光臨</b><br><a class="leaf-style" href="https://www.google.com.tw/maps/place/Chung+Hua+University/@24.7610781,120.955573,15.87z/data=!4m5!3m4!1s0x34684a5cb60622fd:0xb0ead5088c97b0db!8m2!3d24.7598187!4d120.9529895" target="_blank">新竹總部</a>`).openPopup();
marker_2.bindPopup(`<b>歡迎光臨</b><br><a class="leaf-style" href="https://www.google.com.tw/maps/place/Hsinchu/@24.7987612,120.9789725,15z/data=!4m5!3m4!1s0x346835e9c2e07205:0x5e8cb484291aeeba!8m2!3d24.8016287!4d120.9715638" target="_blank">新竹站前分店</a>`).openPopup();
marker_3.bindPopup(`<b>歡迎光臨</b><br><a class="leaf-style" href="https://www.google.com.tw/maps/place/Hsinchu/@24.8067863,121.0314579,15z/data=!4m5!3m4!1s0x346837075f185ca5:0xf379bf8f5529d143!8m2!3d24.8081649!4d121.0402445" target="_blank">新竹高鐵分店</a>`).openPopup();