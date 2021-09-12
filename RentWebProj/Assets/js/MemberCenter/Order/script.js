
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







