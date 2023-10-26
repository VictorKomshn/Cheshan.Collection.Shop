const cartCounters = document.getElementsByClassName("cart-counter");
const closeIcons = document.getElementsByClassName("close-icon-wrapper");
const sideMenu = document.getElementById("side-menu-wrapper");
const sideMenuCategory = document.getElementById("side-menu-buttons");
const filtersInner = document.getElementById("filters-inner");
const brandsInner = document.getElementById("brands-wrapper");
var cartMenuWrapper = document.getElementById("cart-menu-wrapper");
const html = document.getElementsByTagName('html')[0];



window.onload = function () {
    $.ajaxSetup({ cache: false });
    $.ajax({
        url: "/cart/count",
        type: "GET",
        data: {},
        success: function (res) {
            for (let i = 0; i < cartCounters.length; i++) {
                cartCounters[i].textContent = "(" + res + ")";
                cartCounters[i].dataset.amount = res;
            }
        }
    });


    $.ajax({
        url: "/cart/products",
        type: "GET",
        data: {},
        success: function (res) {
            $("#cart-menu-content").html(res);
        }
    });
}

const sideMenuManCategoriesButtons = document.getElementById("filters-mobile-men-inner").getElementsByClassName("filter-button");
const sideMenuWomanCategoriesButtons = document.getElementById("filters-mobile-women-inner").getElementsByClassName("filter-button");

for (let i = 0; i < sideMenuManCategoriesButtons.length; i++) {
    sideMenuManCategoriesButtons[i].onclick = function () {
        filterCategory(sideMenuManCategoriesButtons[i]);
    }
}

for (let i = 0; i < sideMenuWomanCategoriesButtons.length; i++) {
    sideMenuWomanCategoriesButtons[i].onclick = function () {
        filterCategory(sideMenuWomanCategoriesButtons[i]);
    }
}

function getSideCartSumPrice() {
    let productsInCart = document.getElementsByClassName("product-in-cart-wrapper");
    let sumPrice = 0;
    for (let i = 0; i < productsInCart.length; i++) {
        let price = parseInt(productsInCart[i].getElementsByClassName('product-in-cart-menu-price')[0].textContent);
        let amount = parseInt(productsInCart[i].getElementsByClassName('product-in-cart-amount')[0].dataset.amount);

        sumPrice += (price * amount);
    }

    if (sumPrice != 0) {
        document.getElementById("cart-menu-sumprice").style.display = "block";
        document.getElementById("cart-menu-sumprice").textContent = "СУММА ЗАКАЗА: " + sumPrice + "₽";
    }
    else {
        document.getElementById("cart-menu-sumprice").style.display = "none";
    }
}

for (let i = 0; i < closeIcons.length; i++) {
    closeIcons[i].onclick = function () {
        let elementToCloseId = this.dataset.close;
        let elementToClose = document.getElementById(elementToCloseId);
        elementToClose.classList.remove("active");
        elementToClose.style.display = "none";
        html.style.overflowY = "scroll";
        let mainElement = document.getElementById("main-content-wrapper-to-hide");
        if (mainElement != null && mainElement != undefined) {
            mainElement.style.display = "unset";
        }
        clearActive();
    }
}

function openSideMenu(buttonOpener) {
    cartMenuWrapper.style.display = "none"
    sideMenu.style.display = "inline-flex";
    let buttonToOpenName = "filter-button-" + buttonOpener.dataset.buttonName;
    let buttonToOpen = document.getElementById(buttonToOpenName);
    let categoryToOpen = buttonOpener.dataset.buttonName;
    if (categoryToOpen == "brands") {
        brandsInner.style.display = "flex";
    }
    else {
        filtersInner.style.display = "block";
        if (categoryToOpen == "woman") {
            document.getElementById("woman-categories-inner").style.display = "flex";
        }
        else {
            document.getElementById("man-categories-inner").style.display = "flex";
        }
    }
    sideMenuCategory.dataset.selectedcategory = buttonOpener.dataset.buttonName;
    buttonToOpen.classList.add("button-active");
    html.style.overflowY = "hidden";
}

function openCartMenu() {
    sideMenu.style.display = "none";
    cartMenuWrapper.style.display = "inline-flex";
    html.style.overflowY = "hidden";

    var proceedToCartButton = document.getElementById("proceed-to-cart-button");
    if (cartCounters[0].dataset.amount == null || cartCounters[0].dataset.amount == "0") {
        proceedToCartButton.style.display = "none";
    }
    else {
        proceedToCartButton.style.display = "inline-flex";
    }

    getSideCartSumPrice();
}

function clearActive() {
    let filterCategoryButtons = document.getElementsByClassName("filter-category-button");
    for (let i = 0; i < filterCategoryButtons.length; i++) {
        if (filterCategoryButtons[i].classList.contains("button-active")) {
            filterCategoryButtons[i].classList.remove("button-active")
        }
    }
}

function selectCategory(buttonActivator) {
    clearActive();
    if (!buttonActivator.classList.contains("button-active")) {
        buttonActivator.classList.add("button-active");
        sideMenuCategory.dataset.selectedcategory = buttonActivator.dataset.category;
        let categoryToOpen = buttonActivator.dataset.category;
        if (categoryToOpen == "brands") {
            brandsInner.style.display = "flex";
            filtersInner.style.display = "none";
        }
        else {
            filtersInner.style.display = "block";
            if (categoryToOpen == "woman") {
                document.getElementById("woman-categories-inner").style.display = "flex";
                document.getElementById("man-categories-inner").style.display = "none";
            }
            else {
                document.getElementById("man-categories-inner").style.display = "flex";
                document.getElementById("woman-categories-inner").style.display = "none";
            }
            brandsInner.style.display = "none";
        }
    }
}

function filterCategory(button) {
    let categoryName = button.dataset.category;
    let selectedCategory = sideMenuCategory.dataset.selectedcategory;

    let isMan = "";
    if (selectCategory == null || selectCategory == "" || selectedCategory == undefined) {
        selectedCategory = button.parentNode.parentNode.dataset.sex;
    }
    if (selectedCategory == "man") {
        isMan = true;
    }
    else if (selectedCategory == "woman") {
        isMan = false;
    }
    else {
        window.location = '/products';
    }

    if (categoryName == "all") {
        window.location = '/products/filter?isMan=' + isMan;
    }
    else if (categoryName == "new") {
        window.location = '/products/filter?isMan=' + isMan + "&sort=0";;
    }
    else if (categoryName == "sale") {
        window.location = '/products/filter?isMan=' + isMan + "&sort=3";
    }
    else {
        window.location = '/products/filter?isMan=' + isMan + "&categories=" + categoryName;
    }
}


function closeOnClick(elementName) {
    let element = document.getElementById(elementName);
    element.style.display = "none";
    html.style.overflowY = "initial";
}


//collapsibles
var coll = document.getElementsByClassName("collapsible-button");

for (let j = 0; j < coll.length; j++) {
    coll[j].addEventListener("click", function () {
        let content = this.nextElementSibling;
        if (content.classList.contains("collapsible-active")) {
            this.children[1].classList.remove("collapsible-active-icon");
            content.classList.remove("collapsible-active");
        }
        else {
            this.children[1].classList.add("collapsible-active-icon");
            content.classList.add("collapsible-active");
        }
    });
}

function subscribeOnNotifications() {
    let name = document.getElementById("notifications-subscriber-name-input").value;
    let email = document.getElementById("notifications-subscriber-email-input").value;

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: "/notifications/add/" + email,
        type: "POST",
        data: JSON.stringify(name),
        dataType: 'json',
        success: function (res) {
            let elementToClose = document.getElementsByClassName("notifications-content-info-inner")[0];
            elementToClose.style.display = "none";
            elementToClose = document.getElementsByClassName("notificaitons-subscribe-wrapper")[0];
            elementToClose.style.display = "none";

            let elementToOpen = document.getElementsByClassName('gratitude-cover')[0];
            elementToOpen.style.display = "flex";
        }
    });
}


function requestHelp() {
    let name = document.getElementById("help-name-input").value;
    let email = document.getElementById("help-email-input").value;
    let phone = document.getElementById("help-phone-input").value;
    let helpMessage = document.getElementById("help-text-input").value;

    var data = JSON.stringify({
        'name': name,
        'email': email,
        'phone': phone,
        'message': helpMessage
    });
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: "/help",
        type: "POST",
        data: data,
        dataType: 'json',
        success: function (res) {
            let elementToClose = document.getElementsByClassName("help-content-initial")[0];
            elementToClose.style.display = "none";
            elementToClose = document.getElementsByClassName("help-wrapper-info")[0];
            elementToClose.style.display = "none";


            let elementToOpen = document.getElementsByClassName('help-success-wrapper')[0];
            elementToOpen.style.display = "flex";
        }
    });
}

function openWindow(windowId, display) {
    document.getElementById(windowId).style.display = display;
}

function removeItemFromCart(element) {
    return jQuery.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'type': 'POST',
        'url': "/cart/remove/" + element.dataset.productid,
        'data': JSON.stringify(element.dataset.size),
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

function clearCart() {
    return jQuery.ajax({
        url: "/cart/remove",
        type: "POST",
        data: {},
        success: function (res) {
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
    });
}



const mobileMenu = document.getElementById("mobile-menu-wrapper");
function openMobileMenu() {
    mobileMenu.style.display = "flex";
    let mainElement = document.getElementById("main-content-wrapper-to-hide");
    if (mainElement != null && mainElement != undefined) {

        mainElement.style.display = "none";
    }
}