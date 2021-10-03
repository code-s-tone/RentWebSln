var item;
let vm;
let TrueResult;
let FalseResult;
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
            window.location.href = `/Product/ProductDetail/?id=${ProductId}`
        },
        Creat() {
            window.location.href = `/Product/ProductDetail`
        },
        Index(Discontinuation) {
            window.location.href = `/Product/Index/`
        },
        CloseIndex(Discontinuation) {
            window.location.href = `/Product/CloseIndex/?id=${Discontinuation}`
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
    var getUrlString = location.href;
    var url = new URL(getUrlString);
    var DisconTrueOrNot = Boolean(url.searchParams.get('id'));
    const Url = "/api/Product/GetProduct"
    console.log(DisconTrueOrNot);
    fetch(Url,
        {
            method: "Get",
            headers: { 'Content-Type': 'application/json' },
        })
        .then(res => res.json())
        .then(result => {

            if (DisconTrueOrNot != null)
            {
                vm.$data.items = result.filter(x => x.Discontinuation == DisconTrueOrNot);
            }
            else
            {
                vm.$data.items = result.filter(x => x.Discontinuation == true);
            }
        })
        .catch(ex => {
            console.log("資料撈失敗@@");
        })
};


$(document).ready(function () {
    LoadData()
});