$(document).ready(function () {
    //nhấn thêm 1 chức năng
    var loading =
    '<div class="loading" style="position: relative;left: 500px"><span class="text-center " style="color:black"><i class="mdi mdi-spin mdi-18px "></i></span></div>';
    $(document)
    .on("click",
        ".btn-them",
        function () {
            $.ajax({
                type: "POST",
                url: '/Admin/Product/AddRow/',
                data: {},
                success: function (r) {
                    $('.tblAttrVal  .loading').remove();
                    $('.tblAttrVal tbody tr:first').after(r);
                    $("#AttributeId").addClass("select2");
                },

                error: function (req, status, error) {
                }
            });
        });
    //open popup add attr
    $(document)
    .on("click",
        ".btnAddAttr",
        function () {
            $("#addAttribute").modal({ backdrop: 'static', keyboard: false });
            $.ajax({
                type: "POST",
                url: '/Admin/Product/AddAttr',
                data: {},
                success: function (r) {
                    $("#list-Atrr").html(r);
                },

                error: function (req, status, error) {
                }
            });
        });
    // save new attr from popup
    $(document)
        .on("click",
            ".btnSaveAttr",
            function() {
                var attrName = $("#list-Atrr #AttributeName").val();
                var attrType = $("#list-Atrr #AttributeType").val();
                var id = $("#list-Atrr #Id").val();
                if (attrName === "") {
                    $(".err-attr").innerHTML("Chưa nhập tên thuộc tính");
                    return false;
                } else {
                    $.ajax({
                        type: "POST",
                        url: '/Admin/Product/SaveAttr',
                        data: { AttributeType: attrType, AttributeName:attrName,Id:id},
                        success: function (r) {
                            $("#addAttr").hide();
                        },

                        error: function (req, status, error) {
                        }
                    });
                }
            });

    //thêm chi tiết
    $(document)
        .on("click",
            ".btn-chitiet",
            function () {
                var postid = $(this).attr("data-id");
                $.ajax({
                    type: "POST",
                    url: '/Admin/Product/LoadAttrByProId',
                    data: {proid: postid},
                    success: function (r) {
                        $("#list-Info").html(r);
                    },

                    error: function (req, status, error) {
                    }
                });
            });
})