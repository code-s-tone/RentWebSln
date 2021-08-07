let map = L.map('map', {
    center: [24.9, 121],
    zoom: 8
});

let osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
let osm = new L.TileLayer(osmUrl, { minZoom: 3, maxZoom: 19 });
map.addLayer(osm);
// map.setView([24.8226948,120.94912], 7);

let marker = L.marker([24.8226948, 120.94912]);
map.addLayer(marker);
