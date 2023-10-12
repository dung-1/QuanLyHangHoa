$(document).ready(function () {
    $(".updatecartitem").click(function (event) {
        event.preventDefault();
        var productid = $(this).attr("data-productid");
        var quantity = $("#quantity-" + productid).val();
        $.ajax({
            type: "POST",
            url: "@Url.RouteUrl("updatecart")",
            data: {
                productid: productid,
                quantity: quantity
            },
            success: function (result) {
                window.location.href = "@Url.RouteUrl("Cart")";
            }
        });
    });
});
$(document).ready(function () {
    $("#btnCheckout").click(function () {
        $.ajax({
            type: "POST",
            url: "@Url.Action("Checkout", "User")",
            success: function (result) {
                if (result.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Thanh toán thành công!',
                        showConfirmButton: false,
                        timer: 2000 // Tự đóng sau 2 giây
                    }).then(function () {
                        window.location.href = "@Url.RouteUrl("Cart")";
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Đã xảy ra lỗi!',
                        text: 'Không thể thanh toán đơn hàng.'
                    });
                }
            }
        });
    });
});