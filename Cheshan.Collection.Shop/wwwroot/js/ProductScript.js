const sizeButtons = document.getElementsByClassName("size-wrapper");
const sizeInfo = document.getElementById("size-info");
const addToCartButton = document.getElementById("add-to-cart-button");
const productId = document.getElementById("product-id").textContent;
const productCartCounters = document.getElementsByClassName("cart-counter");
const photosSlider = document.getElementById("photos-slideshow");
const sizeswindow = document.getElementById("product-sizes-window");
const brand = document.getElementById("product-brand");
const anySizes = document.getElementById("any-sizes");
const notificationSizeWrappers = document.getElementsByClassName("notification-size-wrapper");
const notificationSelectedSizeInput = document.getElementById("notification-selected-size");
const subscribeOnProductButton = document.getElementById("subscribe-on-product-button");


for (let i = 0; i < sizeButtons.length; i++) {
    sizeButtons[i].addEventListener('click', function () {
        addActive(sizeButtons[i], sizeInfo);

        removeInactiveButton(addToCartButton);
    });
}

for (let i = 0; i < notificationSizeWrappers.length; i++) {
    notificationSizeWrappers[i].addEventListener('click', function () {
        saveSizeForNotification(notificationSizeWrappers[i]);
    });
}

function addActive(sizeButton, sizeInfo) {
    if (sizeButton != null) {
        addSelectionMark(sizeButton, sizeButtons);
        sizeInfo.textContent = "РАЗМЕР: " + sizeButton.textContent;
        addToCartButton.textContent = "В КОРЗИНУ";
        addToCartButton.dataset.size = sizeButton.textContent;
    }
}

function saveSizeForNotification(notificationSizeButton) {
    addSelectionMark(notificationSizeButton, notificationSizeWrappers);
    notificationSelectedSizeInput.value = notificationSizeButton.dataset.size;
}

function addSelectionMark(sizeButton, sizeButtonsArray) {
    for (let i = 0; i < sizeButtonsArray.length; i++) {
        if (sizeButtonsArray[i].classList.contains('selected-size')) {
            sizeButtonsArray[i].classList.remove('selected-size');
        }
    }
    sizeButton.classList.add('selected-size');
}


function removeSelectedSise() {
    for (var i = 0; i < sizeButtons.length; i++) {
        let sizeButton = sizeButtons[i]
        if (sizeButton.classList.contains("selected-size")) {
            sizeButton.classList.remove("selected-size");
            addToCartButton.dataset.size = null;
            sizeInfo.textContent = "РАЗМЕР";
        }

    }
}
addToCartButton.onclick = function () {

    let buttonIsActive = checkButtonValidity(this)
    if (buttonIsActive == false) {
        return
    }
    if (this.dataset.size != null && this.dataset.size != "") {

        return jQuery.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'type': 'POST',
            'url': "/cart/add/" + productId,
            'data': JSON.stringify(this.dataset.size),
            'dataType': 'json',
            'success': function (res) {
                addInactiveButton(addToCartButton);
                addToCartButton.textContent = "ВЫБЕРИТЕ РАЗМЕР";

                removeSelectedSise();

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
                    }
                });

                openCartMenu();

            }
        })
    };
};

if (subscribeOnProductButton != null) {
    subscribeOnProductButton.onclick = function () {
        let selectedSize = notificationSelectedSizeInput.value;
        let email = document.getElementById("email-to-notificate").value;
        let productId = document.getElementById("notification-product-id").value
        return jQuery.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'type': 'POST',
            'url': "/notifications/notify/" + productId,
            'data': JSON.stringify({ size: selectedSize, email: email }),
            'dataType': 'json',
            'success': function (res) {
                alert("Success");
            }
        })
    }
}


//slides

let slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

let touchstartX = 0
let touchendX = 0

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("slider-photo-wrapper");
    if (n > slides.length) {
        slideIndex = 1
    }
    if (n < 1) {
        slideIndex = slides.length
    }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slideIndex - 1].style.display = "block";
}

photosSlider.addEventListener('touchstart', e => {
    touchstartX = e.changedTouches[0].screenX;
})

photosSlider.addEventListener('touchend', e => {
    touchendX = e.changedTouches[0].screenX;
    checkDirection();
})

function checkDirection() {
    if (touchendX < touchstartX) {
        plusSlides(-1);
    }
    if (touchendX > touchstartX) {
        plusSlides(1);
    }
}


if (brand.textContent.toLowerCase() == "cult gaia") {
    sizeswindow.style.width = "235px";
}
else if (brand.textContent.toLowerCase() == "jil sander" ||
    brand.textContent.toLowerCase() == "karl lagerfeld") {
    sizeswindow.style.width = "234px";
}