$(function () {

    $(".header__search-input").autocomplete({
        minLength: 1,
        source: function (request, response) {

            $.ajax({
                url: "/Home/ListNameBySearch",
                dataType: "json",
                data: {
                    q: request.term
                },
                success: function (data) {
                    response(data.data);

                }
            });
        },      
        select: function (event, ui) {
            console.log(ui.item.label);
            $(".header__search-input").val(ui.item.label);
            return false;
        }
    }).autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>").append("<a class='header__search-history-link'>" + "<span class='header__search-history-text'>" + item.label + "</span>" + "</a>").appendTo(ul);
    };

});

