
let dNoneOnPhone = document.querySelectorAll(".dNoneOnPhone");
console.log(document.querySelector("#keywordInput"));
document.querySelector("#keywordInput").addEventListener("click", function () {
    dNoneOnPhone.forEach(x => x.classList.remove('dNoneOnPhone'));
});


/*Create_map();*/

//function Create_map() {
//    let map = L.map('map', {
//        center: [24.9, 121],
//        zoom: 7,
//    });

//    let osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
//    let osm = new L.TileLayer(osmUrl, { minZoom: 3, maxZoom: 18 });
//    map.addLayer(osm);
//    // map.setView([24.8226948,120.94912], 7);
//    let marker = L.marker([24.8226948, 120.94912]);
//    map.addLayer(marker);
//}
