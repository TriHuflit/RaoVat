var IDNews;
//var btndelete = document.querySelectorAll(".delete-news-btn");
//for (let i = 0; i < btndelete.length; i++) {
//    btndelete[i].addEventListener('click', function (ev) {
//        IDNews = $(btndelete[i]).attr("value");
//        console.log(IDNews);
//        $("#eventclick-modal").attr("style", "display:flex;");
//        $("#Message-Comfirm").attr("style", "display:block;");
//    }, false);

//}
function DeteleNews(ev) {
    IDNews = $(ev).attr("value");
    $("#eventclick-modal").attr("style", "display:flex;");
    $("#Message-Comfirm").attr("style", "display:block;");
}
//$(".delete-news-btn").click(function () {

//    IDNews = $(this).attr("value");
//    console.log(IDNews);
//    $("#eventclick-modal").attr("style", "display:flex;");
//    $("#Message-Comfirm").attr("style", "display:block;");

//})


function LoadDataNews() {
    IDUser = IDNews;
    $.ajax({
        type: "GET",
        url: "/News/newsOfUser",
        data: { IDUser: IDUser },
        success: function (data) {
            $("#news-of-user").html(data);
            closeModal();
        }
    })
}

var modal = document.getElementById("eventclick-modal");
function closeModal() {
    modal.style.display = "none";
}