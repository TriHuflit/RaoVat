$(document).ready(function () {
    $("#IDCategory").change(function () {
        var IDCategory = $(this).val();
        $.ajax({
            type: "post",
            url: "/UserProfile/GetSubCategory?IDCategory=" + IDCategory,
            contentType: "html",
            success: function (response) {
                $("#IDSubCategory").empty();
                $("#IDBrand").empty();
                $("#IDBrand").append("<option value=''>Chọn dòng</option>")
                $("#IDSubCategory").append(response);
            }
        })
    })
    $("#IDSubCategory").change(function () {
        var IDSubCategory = $(this).val();
        $.ajax({
            type: "post",
            url: "/UserProfile/GetBrand?IDSubCategory=" + IDSubCategory,
            contentType: "html",
            success: function (response) {
                $("#IDBrand").empty();
                $("#IDBrand").append(response);
            }
        })
    })
});
