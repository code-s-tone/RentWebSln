var item;
let vm;
vm = new Vue({
    el: "#app",
    data: {
        isBusy: true,
        items: [],
        fields: [
            { key: 'ProductId', label: '產品編號', class: 'text-center' },
            { key: 'ProductName', label: '產品名子', class: 'text-center' },
            { key: 'DailyRate', label: '單價', class: 'text-center' },
            { key: 'LaunchDate', label: '上架日期', class: 'text-center' },
            { key: 'WithdrawalDate', label: '下架時間', class: 'text-center', },
            { key: 'UpdateTime', label: '更新時間', class: 'text-center', },
            { key: 'actions', label: '動作' }
        ],
        totalRows: 1,
        currentPage: 1,
        perPage: 5,
        pageOptions: [5, 10, 15, { value: 1000, text: "全部" }],
        sortBy: '',
        sortDesc: false,
        sortDirection: 'asc',
        filter: null,
        filterOn: [],
        infoModal: {
            id: 'info-modal',
            title: '',
            content: ''
        }

    },
    computed: {
        sortOptions() {
            return this.fields
                .map(f => {
                    return { text: f.label, value: f.key }
                })
        }
    },
    mounted() {
        this.totalRows = this.items.length
    },
    methods: {
        onFiltered(filteredItems) {
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        link(ProductId) {
            window.location.href = `../Product/ProductDetail/${ProductId}`
        },
        Creat() {
            window.location.href = `../Product/ProductDetail`
        },
    },
    watch: {
        items: function () {
            this.isBusy = false
        }
    },
    filters: {
        dateFormat: function (value) {
            let result = new moment(value).format('LLLL');
            return result;
        }
    },

})

function LoadData() {

    const Url = "/api/Product/GetProduct"

    fetch(Url,
        {
            method: "Get",
            headers: { 'Content-Type': 'application/json' },
        })
        .then(res => res.json())
        .then(result => {
            vm.$data.items = result;
        })
        .catch(ex => {
            console.log("資料撈失敗@@");
        })
};


$(document).ready(function () {
    LoadData()
});