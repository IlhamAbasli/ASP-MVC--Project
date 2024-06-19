$(function () {
    $(document).on('click', '.btn-plus', function () {

        let id = parseInt($(this).parent().parent().parent().parent().attr("data-id"));
        let productTotal = $(this).parent().parent().parent().next().children().first()
        $.ajax({
            url: `basket/IncreaseProductCount?id=${id}`,
            type: 'POST',
            success: function (response) {
                $(".product-count").text(response.count);
                $(".total-price").text(`$${response.totalPrice}`)
                productTotal.html(`${response.productTotalPrice} $`)
            },
        });
    })
    $(document).on('click', '.btn-minus', function () {

        let id = parseInt($(this).parent().parent().parent().parent().attr("data-id"));
        let productTotal = $(this).parent().parent().parent().next().children().first()
        $.ajax({
            url: `basket/DecreaseProductCount?id=${id}`,
            type: 'POST',
            success: function (response) {
                $(".product-count").text(response.count);
                $(".total-price").text(`$${response.totalPrice}`)
                productTotal.html(`${response.productTotalPrice} $`)
            }, 
        });
    })


    $(document).on('click', '.remove-prod', function () {

        let id = parseInt($(this).parent().parent().attr("data-id"));
        console.log(id)

        $.ajax({
            url: `basket/DeleteProduct?id=${id}`,
            type: 'POST',
            success: function (response) {
                $(".product-count").text(response.count);
                $(".total-price").text(`$${response.totalPrice}`);
                if (response.count == 0) {
                    $(".basket-area").addClass("d-none");
                    $("#basket-area .container").html(`<div class=" text-center alert alert-warning cart-alert mt-4" role="alert">Products not added yet</div>`)
                }
                $(`[data-id = ${id}]`).remove();
            },
        });
    })
})