
    $('.home-product-item__like').click(function (e) {
            var data = $(this).attr("id");
            $.ajax({
                dataType:"Html",
                type: "post",
                url: "/Home/AddWishList",
                data: {IDNews: data},
                success: function (result) {
                    if (result == '""') {
                         window.location.href = "/UserLogin";
                    }
                    else {
                        if (result == '"True"') {
                            $('#' + data).find('i').removeClass('far fa-heart');
                            $('#' + data).find('i').addClass('fas fa-heart');
                            toastr.success('Lưu Tin Rao Thành Công','Thông Báo');
                        }
                        else {
                            $('#' + data).find('i').removeClass('fas fa-heart');
                            $('#' + data).find('i').addClass('far fa-heart');
                            toastr.error('Tin rao đã được hủy', 'Thông Báo');
                        }
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

