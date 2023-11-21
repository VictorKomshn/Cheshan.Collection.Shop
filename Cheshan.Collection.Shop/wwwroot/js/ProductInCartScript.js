


function increaseAmount(element) {
    let productId = element.dataset.itemid;
    let size = element.dataset.size;
    return jQuery.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'type': 'POST',
        'url': "/cart/add/" + productId,
        'data': JSON.stringify(size),
        'dataType': 'json',
        'success': function (res) {
            for (let i = 0; i < cartCounters.length; i++) {
                cartCounters[i].textContent = "(" + res + ")";
                cartCounters[i].dataset.amount = res;
            }
            jQuery.ajax({
                url: "/cart/products",
                type: "GET",
                data: {},
                success: function (res) {
                    $("#cart-menu-content").html(res);
                    getSideCartSumPrice();

                    if ($("#cart-page-products-wrapper") != null) {
                        $("#cart-page-products-wrapper").html(res);
                        getCartPageSumPrice()
                    }

                }
            });
        }
    });
}

function removeSingleItemFromCart(element) {
    let productId = element.dataset.itemid;
    let size = element.dataset.size;
    return jQuery.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'type': 'POST',
        'url': "/cart/remove-single/" + productId,
        'data': JSON.stringify(size),
        'dataType': 'json',
        'success': function (res) {
            for (let i = 0; i < cartCounters.length; i++) {
                cartCounters[i].textContent = "(" + res + ")";
                cartCounters[i].dataset.amount = res;
                if (res == 0) {
                    document.getElementById("proceed-to-cart-button").style.display = "none";
                }

            }
            jQuery.ajax({
                url: "/cart/products",
                type: "GET",
                data: {},
                success: function (res) {
                    $("#cart-menu-content").html(res);
                    getSideCartSumPrice();

                    if ($("#cart-page-products-wrapper") != null) {
                        $("#cart-page-products-wrapper").html(res);
                        getCartPageSumPrice()
                    }
                }
            });
        }
    })
}


function getCartPageSumPrice() {
    let productsWrapper = document.getElementById("cart-page-products-wrapper");
    if (productsWrapper != null && productsWrapper != undefined) {
        let products = productsWrapper.getElementsByClassName("product-in-cart-content-wrapper");

        let sumPrice = 0;

        for (let i = 0; i < products.length; i++) {
            let price = parseInt(products[i].getElementsByClassName('product-in-cart-menu-price')[0].textContent);
            let amount = parseInt(products[i].getElementsByClassName('product-in-cart-amount')[0].dataset.amount);

            sumPrice += (price * amount);
        }

        if (sumPrice != 0) {
            document.getElementById("sum-price").textContent = sumPrice + "₽";
            document.getElementById("result-sum").textContent = "ИТОГОВАЯ СУММА: " + sumPrice + "₽";
            document.getElementById("desktop-sum-price").textContent = sumPrice + "₽";
            document.getElementById("desktop-result-sum").textContent = "ИТОГОВАЯ СУММА: " + sumPrice + "₽";
        }
        else {
            document.getElementsByClassName("cart-info-wrapper")[0].style.display = "none";

        }
    }
}