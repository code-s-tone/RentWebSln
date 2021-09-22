
// init list block display
let orderDisplayLists = document.querySelectorAll(".member-display-order-detail-list");
for(let i=0;i<orderDisplayLists.length;i++){
    orderDisplayLists[i].setAttribute('style', 'display: none;');
}

//add buttons event
let orderDisplayButton = document.querySelectorAll(".member-display-order-item-show-dynamic-button");
for(let i=0;i<orderDisplayButton.length;i++){
    orderDisplayButton[i].addEventListener('click' , orderIsDisplay);
}

function orderIsDisplay(e){
    let detailList = e.target.parentNode.nextSibling.nextSibling;
    if(detailList.style.display === 'none'){
        detailList.style.display = 'flex';
    }
    else{
        detailList.style.display ='none';
    }
}

let cancel_List = document.querySelectorAll(".btn-cancel");
cancel_List.forEach(function (item) {
    item.addEventListener('click', function () {
        let orderID = parseInt(item.getAttribute("data-id"));

        console.log(`ID: ${orderID}`);

        callOrderApi(orderID, item);
    });
});

const UrlOrderApi = "/api/orderapi/modifyorder";
function callOrderApi(data, item) {
    fetch(UrlOrderApi,
    {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(res => {
        return res.json();

    }).then(res => {
        console.log(res);

        if (res.IsSuccessful == true) {
            let step1 = item.parentNode;
            let step2 = step1.parentNode;
            let step3 = step2.parentNode;
            let step4 = step3.parentNode;
            let step5 = step4.querySelector('.order-status');
            step5.style.color = "black";
            step5.textContent = "¤w¨ú®ø";
            step3.remove();
        }
    });
}






