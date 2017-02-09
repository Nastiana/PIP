var filteredColumnId = 'name';
var items = [];

$(document).ready(function () {
    items = $(".card");
});

/**
 * Column for filter
 */
$(".books-filter .dropdown-menu li").click(function () {
    var item = $(this);
    item.parents(".input-group-btn").find(".filter-title").text(item.text());
    filteredColumnId = item.attr("id");

    var input = $(".books-filter input");
    input.val("");
    input.focus();
    items.each(function () {
        $(this).show();
    });
});

/**
 * Books filter
 */
$(".books-filter input").on("input", function (event) {
    var inputText = $(this).val();

    items.each(function () {
        var item = $(this);
        var q = item.find('.' + filteredColumnId).text();
        if (item.find('.' + filteredColumnId).text().toLowerCase().indexOf(inputText.toLowerCase()) < 0) {
            item.hide();
        } else {
            item.show();
        }
    });
});