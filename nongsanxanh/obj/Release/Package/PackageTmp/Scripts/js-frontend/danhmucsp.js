$(document).ready(function () {
    $(location).attr('href');
    var pathname = window.location.pathname;
    var listItems = $(".list-cate li");
    listItems.each(function () {
        var cate = $(this).find("a").attr("data-url");
        var id = $(this).find("a").attr("data-id");
        if (pathname.indexOf(cate)>0) {
            $("#"+id).addClass("active");
        } else {
            $("#" + id).removeClass("active");
        }
    });
})