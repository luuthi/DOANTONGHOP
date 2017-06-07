$(document).ready(function() {
    $(document).on("click",
        ".pro-cart",
        function() {
            var id = $(this).attr("data-id");
            $.ajax({
                type: "POST",
                url: '/ShoppingCart/AddProductToCart/',
                data: { productid: id, soluong: 1 },
                success: function(r) {
                    $(".content-header > .navbar > #cart").html(r);
                    console.log(r);
                },

                error: function(req, status, error) {
                }
            });
        });
});