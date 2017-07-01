$(document).ready(function() {
    $(document).on("click",
        ".btn-pro-cart",
        function() {
            var id = $(this).attr("data-id");
            $.ajax({
                type: "POST",
                url: '/ShoppingCart/AddProductToCart/',
                data: { productid: id, soluong: 1 },
                success: function (r) {
                    $(".content-header .navbar  #cart").empty();
                    $(".content-header .navbar  #cart").append(r.listpro);
                    $.notify('Đã thêm sản phẩm vào giỏ hàng', "success", "top left"); 
                },

                error: function(req, status, error) {
                }
            });
        });
    
});