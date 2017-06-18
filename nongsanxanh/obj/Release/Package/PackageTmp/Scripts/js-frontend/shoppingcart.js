$(document).ready(function() {
    $(document).on("click",
        ".pro-cart",
        function() {
            var id = $(this).attr("data-id");
            $.ajax({
                type: "POST",
                url: '/ShoppingCart/AddProductToCart/',
                data: { productid: id, soluong: 1 },
                success: function (r) {
                    $(".content-header .navbar  #cart").empty();
                    $(".content-header .navbar  #cart").append(r.listpro);
                    console.log(r);
                },

                error: function(req, status, error) {
                }
            });
        });
    //$(document).on("click",
    //    ".action_button > .btn-remove",
    //    function() {
    //        var id = $(this).attr("data-id");
    //        $.ajax({
    //            type: "POST",
    //            url: '/ShoppingCart/RemoveItemCart/',
    //            data: { productid: id },
    //            success: function (r) {
    //                $(".content-header .navbar  #cart").empty();
    //                $(".content-header .navbar  #cart").append(r.listpro);
    //                //console.log(r);
    //            },

    //            error: function (req, status, error) {
    //            }
    //        });
    //    });
    //$(document).on("click",
    //    ".action_button > .btn-add-item",
    //    function () {
    //        var id = $(this).attr("data-id");
    //        $.ajax({
    //            type: "POST",
    //            url: '/ShoppingCart/PlusItemCart/',
    //            data: { productid: id },
    //            success: function (r) {
    //                $(".content-header .navbar  #cart").empty();
    //                $(".content-header .navbar  #cart").append(r.listpro);
    //                //console.log(r);
    //            },

    //            error: function (req, status, error) {
    //            }
    //        });
    //    });
    //$(document).on("click",
    //    ".action_button > .btn-substract-item",
    //    function () {
    //        var id = $(this).attr("data-id");
    //        $.ajax({
    //            type: "POST",
    //            url: '/ShoppingCart/SubstractItemCart/',
    //            data: { productid: id },
    //            success: function (r) {
    //                $(".content-header .navbar  #cart").empty();
    //                $(".content-header .navbar  #cart").append(r.listpro);
    //                //console.log(r);
    //            },

    //            error: function (req, status, error) {
    //            }
    //        });
    //    });
});