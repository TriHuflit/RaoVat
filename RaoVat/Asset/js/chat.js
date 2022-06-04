const messages = document.getElementById('chat-box');
function appendMessage() {
    const message = document.getElementsByClassName('message')[0];
    if (message != null) {
        const newMessage = message.cloneNode(true);
        messages.appendChild(newMessage);
    }

}

function getMessages() {
    // Prior to getting your messages.
    shouldScroll = messages.scrollTop + messages.clientHeight === messages.scrollHeight;
    /*
     * Get your messages, we'll just simulate it by appending a new one syncronously.
     */
    appendMessage();
    // After getting your messages.
    if (!shouldScroll) {
        scrollToBottom();
    }
}

function scrollToBottom() {
    messages.scrollTop = messages.scrollHeight;
}

scrollToBottom();


$(function () {

    var chatHub = $.connection.chat;
    loadClient(chatHub);
    $.connection.hub.start().done(function () {
        //$("#btnLogin").click(function () {
        //    var name = $("#txtUserName").val();
        //    chatHub.server.connect(name);
        //});

        $('#btnSend').click(function () {
            
            var msg = $('#txtMessage').val();
            if (msg != "") {
                var name = $('#username').val();
                console.log(name);
                chatHub.server.message(name,msg);
                $('#txtMessage').val('').focus();
            }
         
        });
    });

});

function loadClient(chatHub) {

    chatHub.client.message = function (name, msg) {
        console.log($("#username").val());
        if (name === $("#username").val()) {
            $('#chat-box').append("<div class='content-chat-user'> <div> <div class='content-user'><p class='messsage'>" +  msg + "</p></div></div></div>");
            scrollToBottom();
        }
        else {
            $('#chat-box').append("<div class='content-chat-otheruser'> <div> <div class='content-otheruser'><p class='messsage'>" + msg + "</p></div></div></div>");
            scrollToBottom();
        }

    }
    chatHub.client.connect = function (name) {
        $('#hName').val(name);
    }
}
$(".friends-item").click(function () {
    var ft = document.querySelectorAll(".friends-item");
    for (let i = 0; i < ft.length; i++) {
        $(ft[i]).removeClass("friends-item-active")
    }
    $(this).addClass("friends-item-active");
})