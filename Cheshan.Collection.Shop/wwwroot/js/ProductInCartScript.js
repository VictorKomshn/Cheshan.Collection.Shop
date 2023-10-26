
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
                }
            });
        }
    })
}