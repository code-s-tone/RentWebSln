let apiAnalysisData;
let dataManager = new Vue({
    el: '#chartOptions',
    data: {
        distinguish: [1],
        distinguishKey: 0,
        distinguishItem: 0,
    },
    watch: {
        distinguishKey: function () {
            refreshChartDistinguish();
        },
        distinguishItem: function () {
            refreshChartDetail();
        },
    },
});

let ctxDistinguish = document.getElementById("chartDistinguish");
let ctxDetail = document.getElementById("chartDetail");
let chartDistinguish;
let chartDetail;


$(document).ready(function () {
    loadData();
});

//非同步取得資料
function loadData() {
    $.ajax({
        type: "Get",
        url: "/api/Analysis/GetSalesAnalysisData",
        data: "",
        dataType: "json",
        success: function (response) {
            apiAnalysisData = response;
            insertData();
            initializeCharts();
            refreshChartDistinguish(dataManager, chartDistinguish);
        }
    });
}

function insertData() {
    dataManager.distinguish = [];
    dataManager.distinguish.push(makeDistinguishGroup('以分店區分', 'StoreName'));
    dataManager.distinguish.push(makeDistinguishGroup('以種類區分', 'CateName'));
}

function initializeCharts() {
    chartDistinguish = new Chart(ctxDistinguish, {
        type: 'doughnut',
        data: {
            labels: [],//dataManager.distinguish[dataManager.distinguishKey].Labels,
            datasets: [{
                data: [],//dataManager.distinguish[dataManager.distinguishKey].Values,
            }],
        },
        options: {
            maintainAspectRatio: false,
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretSize: 8, //尖端

                //tips內字樣
                callbacks: {
                    label: function (tooltipItem, chart) {//context
                        let idx = tooltipItem.index;
                        let label = chart.labels[idx] || '';
                        let value = chart.datasets[0].data[idx] || '';
                        return label + ':' +
                            value.toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0 });
                    }
                }
            },
            legend: {
                position: 'bottom',
                labels: {
                    usePointStyle: 'true',
                    padding: 30,
                }
            },
            cutoutPercentage: 55,
        },
    });
    chartDetail = new Chart(ctxDetail, {
        type: 'bar',
        data: {
            labels: [],//dataManager.distinguish[dataManager.distinguishKey].DetailsLabels[dataManager.distinguishItem],
            datasets: [{
                label: '銷售額',
                data: [],//dataManager.distinguish[dataManager.distinguishKey].DetailsValues[dataManager.distinguishItem],
                borderWidth: 3,
                hoverBorderColor: "rgb(33, 33, 33)",
            }],
        },
        options: {
            maintainAspectRatio: false,
            scales: {
                // xAxes: [{
                //     ticks: {
                //         maxTicksLimit: 6
                //     },
                //     maxBarThickness: 25,
                // }],
                yAxes: [{
                    ticks: {
                        min: 0,
                        maxTicksLimit: 6,
                        //貨幣格式
                        callback: function (value, index, values) {
                            return value.toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0 });
                        }
                    },
                }],
            },
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                // borderColor
                bodyFontColor: "#858796",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretSize: 6, //尖端
                caretPadding: 10,

                titleMarginBottom: 10,
                titleFontColor: '#6e707e',
                titleFontSize: 14,
                //tips內字樣 > 貨幣格式
                callbacks: {
                    label: function (tooltipItem, chart) {
                        //console.log(tooltipItem)
                        var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                        return datasetLabel + ':' + tooltipItem.yLabel.toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0 });
                    }
                }

            },
            legend: {
                display: false,
                //sort
                //filter
            },
        },
    });
}


//groupBy原型方法
Array.prototype.groupBy = function (prop) {
    return this.reduce(function (groups, item) {
        const val = item[prop]
        groups[val] = groups[val] || []
        groups[val].push(item)
        return groups
    }, {})
};

function makeDistinguishGroup(optionText, groupingProp) {
    //1.api groupBy => { groupingKey:[group的ApiArray] , ... }
    let distinguishGroups = apiAnalysisData.groupBy(groupingProp);
    let labels = Object.keys(distinguishGroups);
    let values = [];
    let detailsLabels = [];// [[],[]]
    let detailsValues = [];
    let RGBs = [];//亂數深色

    //2.每一類
    for (const [key, apiItems_key] of Object.entries(distinguishGroups)) {
        //[主區分key的ApiArray] groupBy =>
        //{
        //    'Ppl':[主區分key且PID的ApiArray] ,
        //    'Pet':...
        //}
        let PIDGroups = apiItems_key.groupBy('PID');
        let TotalSalesAmount = 0;

        //3.每一類中的每一產品
        let PID_Items = [];
        for (const [pID, apiItems_pID_key] of Object.entries(PIDGroups)) {
            //產品總銷售額
            let ProductTotalSalesAmount = apiItems_pID_key.reduce(
                (accumulator, current) => {
                    return accumulator + current.SalesAmount
                }, 0);

            PID_Items.push({ PID: pID, SalesAmount: ProductTotalSalesAmount });

            TotalSalesAmount += ProductTotalSalesAmount;//區分銷售額
        }
        //排序
        PID_Items.sort(function (a, b) {
            return a.SalesAmount < b.SalesAmount ? 1 : -1; //正=>交換  , 負=>不換
        });

        //labels.push(key);//分類標籤
        values.push(TotalSalesAmount);//分類總價

        detailsLabels.push(PID_Items.map(x => x.PID));
        detailsValues.push(PID_Items.map(x => x.SalesAmount));
        RGBs.push(randomColor(33, 222));
    }
    //類key 陣列
    //類總額 陣列
    //類>PID 名稱 二維陣列
    //類>PID 總額 二維陣列
    return {
        OptionText: optionText,
        Labels: labels,
        Values: values,
        RGBs: RGBs,
        DetailsLabels: detailsLabels,
        DetailsValues: detailsValues,
    };

    function randomColor(m, M) {
        let result = [];
        for (let i = 0; i < 3; i++) {
            result.push(Math.floor(Math.random() * (M - m)) + m);
        }
        return result;
    }
}

function refreshChartDistinguish() {
    let tmp = dataManager.distinguish[dataManager.distinguishKey];
    //橫軸標籤、值
    chartDistinguish.config.data.labels = tmp.Labels;
    chartDistinguish.config.data.datasets[0].data = tmp.Values;
    //亂數色
    chartDistinguish.config.data.datasets[0].backgroundColor =
        tmp.RGBs.map(x => `rgb(${x[0]},${x[1]},${x[2]} )`);
    //hover半透明
    chartDistinguish.config.data.datasets[0].hoverBackgroundColor =
        tmp.RGBs.map(x => `rgba(${x[0]},${x[1]},${x[2]},0.6)`);

    chartDistinguish.update();
    refreshChartDetail();
};

function refreshChartDetail() {
    //前十名產品?
    let tmp = dataManager.distinguish[dataManager.distinguishKey];
    let idx = dataManager.distinguishItem;
    //標籤、值
    chartDetail.config.data.labels = tmp.DetailsLabels[idx];
    let target = chartDetail.config.data.datasets[0];
    target.data = tmp.DetailsValues[idx];
    let [R, G, B] = tmp.RGBs[idx];
    //底色 = 漸層色
    target.backgroundColor = setLinearColors(R, G, B, tmp.DetailsLabels.length);
    //框色 = 基底色
    target.borderColor = `rgb(${R},${G},${B})`
    //hover底色 = 互補色
    target.hoverBackgroundColor = `rgb(${255 - R},${255 - G},${255 - B})`

    chartDetail.update();

    function setLinearColors(R, G, B, length) {
        let ColorArray = [];
        for (let i = length; i > 0; i--) {
            let color = `rgba(${R},${G},${B},${i / length} )`
            ColorArray.push(color);
        }
        return ColorArray;
    };
};




