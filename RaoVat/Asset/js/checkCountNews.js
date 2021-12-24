

$('.public-news').click(function (e) {
    $.ajax({
        dataType: "Html",
        type: "GET",
        url: '/UserProfile/createOfNews',
        success: function (msg) {
            console.log(msg);
            if (msg == '"False"') {
                window.location.href = "/UserLogin";
            }
            if(msg =='"True"')
            {
                toastr.error("Bạn đã đăng quá số lượng tin cho phép", 'Thông Báo');
            }
            if (msg != '"False"' && msg != '"True"') {
                window.location.href = "UserProfile/createOfNews";
            }

        }
    })
})
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-bottom-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "3000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}