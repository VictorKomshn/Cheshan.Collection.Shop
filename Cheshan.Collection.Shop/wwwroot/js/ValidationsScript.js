const phoneRegex = /^(\+7|8)[(\s]?[0-9]{3}[)]?[-\s]?[0-9]{3}[-\s]?[0-9]{2}[-\s]?[0-9]{2}$/im;
const emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

function isPhoneValid(phone) {
    return phoneRegex.test(phone);
}

function isEmailValid(email) {
    return emailRegex.test(email);
}

function checkField(button, type) {
    let inputCorrect = false;
    if (type == "email") {
        inputCorrect = isEmailValid(button.value);
    }
    else if (type == "phone") {
        inputCorrect = isPhoneValid(button.value);
    }
    else {
        if (button.value != "") {
            inputCorrect = true;
        }
    }

    if (!inputCorrect) {
        button.classList.add("incorrect-input");
    }
    else {
        if (button.classList.contains("incorrect-input")) {
            button.classList.remove("incorrect-input");
        }
    }
}