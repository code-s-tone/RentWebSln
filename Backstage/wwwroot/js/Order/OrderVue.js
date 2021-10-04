
    let vm;
    let item;
        vm = new Vue({
        el: "#app",
            data: {
        isBusy: true,
                items: [],
                fields: [
                    {key: 'orderID', label: '訂單編號', sortable: true, sortDirection: 'desc', class: 'text-center' },
                    //{key: 'memberID', label: '會員編號', sortable: true, sortDirection: 'desc', class: 'text-center' },
                    {key: 'fullName', label: '訂單人姓名', sortable: false, sortDirection: 'desc', class: 'text-center' },
                    {key: 'storeName', label: '分店', sortable: false, sortDirection: 'desc', class: 'text-center' },
                    {key: 'phone', label: '聯絡電話', sortable: false, sortDirection: 'desc', class: 'text-center' },
                    {key: 'email', label: '聯絡信箱', sortable: false, sortDirection: 'desc', class: 'text-center' },
                    //{key: 'TotalAmount', label: '訂單總額', sortable: true, sortDirection: 'desc' },
                    {key: 'orderStatusID', label: '付款狀態', sortable: true, sortDirection: 'desc', class: 'text-center' },
                    {key: 'goodsStatusID', label: '出貨狀態', sortable: true, sortDirection: 'desc', class: 'text-center' },
                    {key: 'orderDate', label: '成立時間', sortable: true, sortDirection: 'desc', class: 'text-center' },

                    //{key: 'age', label: 'Person age', sortable: true, class: 'text-center' },

                    //{
        //    key: 'isActive',
        //    label: 'Is Active',
        //    formatter: (value, key, item) => {
        //        return value ? 'Yes' : 'No'
        //    },
        //    sortable: true,
        //    sortByFormatted: true,
        //    filterByFormatted: true
        //},
        { key: 'actions', label: '管理' }
                ],
                totalRows: 1,
                currentPage: 1,
                perPage: 5,
                pageOptions: [5, 10, 15, {value: 100, text: "Show a lot" }],
                sortBy: '',
                sortDesc: false,
                sortDirection: 'asc',
                filter: null,
                filterOn: [],
                infoModal: {
        id: 'info-modal',
                    title: '',
                    content: '',
                }
            },
            computed: {
        sortOptions() {
                    // Create an options list from our fields
                    return this.fields
                        .filter(f => f.sortable)
                        .map(f => {
                            return {text: f.label, value: f.key }
                        })
                }
            },
            mounted() {
        // Set the initial number of items
        this.totalRows = this.items.length
    },
            methods: {
        info(item, index, button) {
        this.infoModal.title = `訂單編號 : ${item.orderID}`
                    this.infoModal.content = item;
                    this.$root.$emit('bv::show::modal', this.infoModal.id, button);
                },
                resetInfoModal() {
        this.infoModal.title = ''
                    this.infoModal.content = ''
                },
                onFiltered(filteredItems) {
        // Trigger pagination to update the number of buttons/pages due to filtering
        this.totalRows = filteredItems.length
                    this.currentPage = 1
                },
                link(orderID) {
        window.location.href = `/Order/OrderDetail/${orderID}`;
                }
            },
            watch: {
        items: function () {
        this.isBusy = false;
                }
            },
        });

        const GUrl = "/api/OrderApi/GetOrder";

        function LoadData() {
        fetch(GUrl,
            {
                method: "Get",
                headers: {
                    //'RequestVerificationToken':'@GetAntiXsrfRequestToken()',
                    'Content-Type': 'application/json'
                },

            }).then(res => res.json())
            .then(result => {
                console.log(result);
                vm.$data.items = result;
                
            })
            .catch(ex => {
                console.log("錯了");
            })
    };

        $(document).ready(function () {
        LoadData();
        });