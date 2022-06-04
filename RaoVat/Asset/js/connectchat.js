$(function () {

    var chatHub = $.connection.chat;
    $.connection.hub.start().done(function () {
        $("#btnLogin").click(function () {
            debugger
            var name = $("#txtUserName").val();
            chatHub.server.connect(name);
            console.log("ok nha");
        });
    });
});