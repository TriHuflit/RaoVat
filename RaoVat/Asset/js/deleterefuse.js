var IDNews;
$(".delete-refuse-btn").click(function () {
   
    IDNews = $(this).attr("value");
    console.log(IDNews);
    $("#eventclick-modal").attr("style", "display:flex;");
    $("#Message-Comfirm").attr("style", "display:block;");
    
})

function ComfirmDelete() {
    $.ajax({
        type: "post",
        url: "/UserProfile/Delete",
        data: { IDNews: IDNews },
        success: function (msg) {
          
            console.log(msg);
            if (msg == "False") {
                LoadDataRefuse();
            }
            if (msg == "True") {
                LoadDataNews();
            }
          
        }
    })
}
function LoadDataRefuse() {
    IDUser = IDNews;
    $.ajax({
        type: "GET",
        url: "/News/newsRefuse",
        data: { IDUser: IDUser },
        success: function (data) {
            $("#news-of-refuse").html(data);
            closeModal();
        }
    })
}

var modal = document.getElementById("eventclick-modal");
function closeModal() {
    modal.style.display = "none";
}