document.getElementsByClassName("chatIcon")[0].addEventListener("click", () => {
    var chatBox = document.getElementsByClassName("chatBox")[0];
    var chatImg = document.querySelector(".chatIcon img");
    if (chatBox.style.display === "none") {
        chatBox.style.display = "block";
        chatImg.src = "img/close2.png"
    }
    else {
        chatBox.style.display = "none";
        chatImg.src = "img/chatIcon3.png"
    }
})