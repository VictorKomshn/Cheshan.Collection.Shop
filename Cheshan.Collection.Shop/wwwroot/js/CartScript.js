const selfPickUpWrapper = document.getElementsByClassName("self-pick-up-options-wrapper")[0];
const adressInputWrapper = document.getElementsByClassName("adress-input-wrapper")[0];
const deliveryTypeInput = document.getElementById("delivery-type-input");
const deliveryAdressInput = document.getElementById("delivery-adress-input");
const pamentTypeInput = document.getElementById("payment-type-input");

const submitButton = document.getElementById("submitPurchaseButton");


const paymentButtons = document.getElementsByClassName("payment-type-radio-button");

for (var i = 0; i < paymentButtons; i++) {
    paymentButtons[i].onclick = function () {
        cartPaymentTypeValid = true;
    }
}


$("#purchase-form input").not("#submitPurchaseButton").keydown(function (event) {
    if (event.keyCode == 13) {
        event.preventDefault();
        return false;
    }
});

function selectDelivery(element) {
    clearActiveButtons("delivery-button");
    element.classList.add("selected-button");
    var deliveryType = element.dataset.deliverytype;
    if (deliveryType == "courier") {
        adressInputWrapper.style.display = "block";
        selfPickUpWrapper.style.display = "none";
        deliveryTypeInput.value = "courier";
    }
    else if (deliveryType == "sdek") {
        adressInputWrapper.style.display = "block";
        selfPickUpWrapper.style.display = "none";
        deliveryTypeInput.value = "sdek";
    }
    else if (deliveryType == "selfpickup") {
        selfPickUpWrapper.style.display = "flex";
        adressInputWrapper.style.display = "none";
        deliveryTypeInput.value = "selfpickup";
    }

    validateCart();
}

function clearActiveButtons(buttonsClass) {
    let filterCategoryButtons = document.getElementsByClassName(buttonsClass);
    for (let i = 0; i < filterCategoryButtons.length; i++) {
        if (filterCategoryButtons[i].classList.contains("selected-button")) {
            filterCategoryButtons[i].classList.remove("selected-button")
        }
    }
}

function disablePurchaseButton() {
    submitButton.disabled = true;
    addInactiveButton(submitButton);

}

function enablePurchaseButton() {
    submitButton.disabled = false;
    removeInactiveButton(submitButton);
}

let cartNameInputValid = false;
let cartSecondNameInputValid = false;
let cartEmailInputValid = false;
let cartPhoneInputValid = false;
let cartDeliveryTypeValid = false;
let cartAdressValid = false;
let cartPaymentTypeValid = false;


document.getElementById("name-input").addEventListener("input", function (e) {
    if (this.value != "") {
        cartNameInputValid = true;
    }
    else {
        cartNameInputValid = false;
    }

    validateCart();
})

document.getElementById("secondname-input").addEventListener("input", function (e) {
    if (this.value != "") {
        cartSecondNameInputValid = true;
    }
    else {
        cartSecondNameInputValid = false;
    }

    validateCart();
})

document.getElementById("email-input").addEventListener("input", function (e) {
    if (isEmailValid(this.value)) {
        cartEmailInputValid = true;
    }
    else {
        cartEmailInputValid = false;
    }

    validateCart();
})

document.getElementById("phone-input").addEventListener("input", function (e) {
    if (isPhoneValid(this.value)) {
        cartPhoneInputValid = true;
    }
    else {
        cartPhoneInputValid = false;
    }

    validateCart();
})

document.getElementById("phone-input").addEventListener("input", function (e) {
    if (this.value != "") {
        cartPhoneInputValid = true;
    }
    else {
        cartPhoneInputValid = false;
    }

    validateCart();
})

document.getElementById("adress-input").addEventListener("input", function (e) {

    if (this.value != "") {
        cartAdressValid = true;
    }
    else {
        cartAdressValid = false;
    }

    validateCart();

})

function validateCart() {
    if (cartNameInputValid == true &&
        cartSecondNameInputValid == true &&
        cartEmailInputValid == true &&
        deliveryTypeInput.value != "" &&
        pamentTypeInput.value != "") {
        if (deliveryTypeInput.value == "courier") {
            if (cartAdressValid == true) {
                enablePurchaseButton();
            }
            else {
                disablePurchaseButton();
            }
        }
        else {
            enablePurchaseButton();
        }
    }
    else {
        disablePurchaseButton();
    }
}