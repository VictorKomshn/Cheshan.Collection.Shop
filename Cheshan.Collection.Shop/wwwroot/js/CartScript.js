const selfPickUpWrapper = document.getElementsByClassName("self-pick-up-options-wrapper")[0];
const adressInputWrapper = document.getElementsByClassName("adress-input-wrapper")[0];
const deliveryTypeInput = document.getElementById("delivery-type-input");
const deliveryAdressInput = document.getElementById("delivery-adress-input");

$("#purchase-form input").not("#submitPurchaseButton").keydown(function (event) {
    if (event.keyCode == 13) {
        event.preventDefault();
        return false;
    }
});

function selectDelivery(element) {
    clearActiveButtons("delivery-button");
    this.classList.add("selected-button");
    var deliveryType = element.data.deliverytype; 
    if (deliveryType == "courier")
    {
        adressInputWrapper.style.display = "block";
        selfPickUpWrapper.style.display = "none";
        deliveryTypeInput.value = "courier";
    }
    else if (deliverytype == "sdek")
    {
        adressInputWrapper.style.display = "block";
        selfPickUpWrapper.style.display = "none";
        deliveryTypeInput.value = "sdek";
    }
    else if (deliverytype == "selfpickup")
    {
        selfPickUpWrapper.style.display = "flex";
        adressInputWrapper.style.display = "none";
        deliveryTypeInput.value = "selfpickup";
    }
}

function clearActiveButtons(buttonsClass) {
    let filterCategoryButtons = document.getElementsByClassName(buttonsClass);
    for (let i = 0; i < filterCategoryButtons.length; i++) {
        if (filterCategoryButtons[i].classList.contains("selected-button")) {
            filterCategoryButtons[i].classList.remove("selected-button")
        }
    }
}