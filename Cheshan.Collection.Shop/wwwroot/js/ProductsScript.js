const filtersMenu = document.getElementById("products-fitler-wrapper");
const sizes = document.getElementsByClassName("size-wrapper");
const applyFiltersButton = document.getElementById("apply-filters-button");
const sortButtonText = document.getElementById("sort-button-current-span");
const sortContent = document.getElementById("sort-dropdown-content");
var currentLocation = document.location;

const minPriceInput = document.getElementById("price-min");
const maxPriceInput = document.getElementById("price-max");

const params = new URLSearchParams(window.location.search);
const selectedCategory = params.get('category');
const selectedMaxPrice = params.get('HighestPrice')
const selectedMinPrice = params.get('LowestPrice')
const startIndex = params.get('StartIndex');
const sortType = params.get('sort');
const brandNames = params.get('brandNames');
const isMan = params.get('isMan');

const categoriesArray = [];
let allCategory = "-1";
const sizesArray = [];
const brandsArray = [];
var minPrice = 0;
var maxPrice = 99999999;


let womanFiltersWrapper = document.getElementById("woman-filter-categories");
let manFiltersWrapper = document.getElementById("man-filter-categories");
let brandFiltersWrapper = document.getElementById("brand-filter-categories");


let womanCategoriesButtons = [];
let manCategoriesButtons = [];
let brandCategoriesButtons = [];
if (womanFiltersWrapper != null) {
    womanCategoriesButtons = womanFiltersWrapper.getElementsByClassName("filter-button");
}

if (manFiltersWrapper != null) {
    manCategoriesButtons = manFiltersWrapper.getElementsByClassName("filter-button");
}

if (brandFiltersWrapper != null) {
    brandCategoriesButtons = brandFiltersWrapper.getElementsByClassName("filter-button");
}

let productsGrid = document.getElementsByClassName("products-inner")[0];

window.addEventListener ?
    window.addEventListener("load", downloadBrandsForFilter, false) :
    window.attachEvent && window.attachEvent("onload", downloadBrandsForFilter);

function downloadBrandsForFilter() {

    $.ajax({
        url: "/brands/true",
        type: "GET",
        success: function (res) {
            $("#brands-filter-collapsible-wrappper").html(res);

            var brandsFilterWrapper = document.getElementsByClassName("brands-filter")[0];
            if (brandsFilterWrapper != null) {

                let brandButtons = brandsFilterWrapper.getElementsByClassName("filter-button");

                for (let i = 0; i < brandButtons.length; i++) {
                    brandButtons[i].onclick = function () {
                        addBrandToFilter(brandButtons[i])
                    }
                }
            }
        }
    })
};



if (isMan == undefined || isMan == null) {
    manFiltersWrapper.style.display = "none";
    womanFiltersWrapper.style.display = "none";
    brandFiltersWrapper.style.display = "flex";
}
else if (isMan === "true") {
    manFiltersWrapper.style.display = "flex";
    womanFiltersWrapper.style.display = "none";
    brandFiltersWrapper.style.display = "none";

}
else {
    womanFiltersWrapper.style.display = "flex";
    manFiltersWrapper.style.display = "none";
    brandFiltersWrapper.style.display = "none";

}

if (sortType == null || sortType == 0) {
    sortButtonText.textContent = "ПО НОВИЗНЕ";
    sortContent.children[0].style.display = "none";
}
else if (sortType == 1) {
    sortButtonText.textContent = "ПО УБЫВАНИЮ ЦЕНЫ"
    sortContent.children[1].style.display = "none";
}
else if (sortType == 2) {
    sortButtonText.textContent = "ПО ВОЗРАСТАНИЮ ЦЕНЫ"
    sortContent.children[2].style.display = "none";
}

else if (sortType == 3) {
    sortButtonText.textContent = "ПО СКИДКАМ"
    sortContent.children[3].style.display = "none";
}


function openFiltersMenu() {
    filtersMenu.style.display = "inline-flex";
    html.style.overflowY = "hidden";

}

for (let i = 0; i < sizes.length; i++) {
    sizes[i].onclick = function () {
        addSizeToFilter(sizes[i])
    }
}

if (isMan === "true") {
    for (let i = 0; i < manCategoriesButtons.length; i++) {
        manCategoriesButtons[i].onclick = function () {
            addCategoryToFilter(manCategoriesButtons[i]);
        }
    }
}
else if (isMan == "false") {
    for (let i = 0; i < womanCategoriesButtons.length; i++) {
        womanCategoriesButtons[i].onclick = function () {
            addCategoryToFilter(womanCategoriesButtons[i]);
        }
    }
}
else {
    for (let i = 0; i < brandCategoriesButtons.length; i++) {
        brandCategoriesButtons[i].onclick = function () {
            addCategoryToFilter(brandCategoriesButtons[i]);
        }
    }
}

function addSizeToFilter(size) {
    if (size.classList.contains("selected")) {
        size.classList.remove("selected");
        for (let i = 0; i < sizesArray.length; i++) {
            if (sizesArray[i] === size.dataset.size) {
                sizesArray.splice(i, 1);
            }
        }
    }
    else {
        size.classList.add("selected");
        sizesArray.push(size.dataset.size);
    }
}


function addCategoryToFilter(button) {
    let categoryName = button.dataset.category;

    if (button.classList.contains("selected")) {
        button.classList.remove("selected");
        for (let i = 0; i < categoriesArray.length; i++) {
            if (categoriesArray[i] === categoryName) {
                categoriesArray.splice(i, 1);
            }
        }
    }
    else {
        button.classList.add("selected");
        categoriesArray.push(categoryName);
    }
}

let allCategoryButtons = document.getElementsByClassName("all-category")

for (let i = 0; i < allCategoryButtons.length; i++) {
    allCategoryButtons[i].onclick = function () {
        addAllCategoryToFilter(allCategoryButtons[i]);
    }
}

function addAllCategoryToFilter(button) {
    let categoryName = button.dataset.category;

    if (button.classList.contains("selected")) {
        button.classList.remove("selected");
        allCategory = "all";
    }
    else {
        for (let i = 0; i < allCategoryButtons.length; i++) {
            allCategoryButtons[i].classList.remove("selected");
        }
        button.classList.add("selected");
        allCategory = categoryName;
    }
}

function addBrandToFilter(button) {
    let brandName = button.dataset.brand;

    if (button.classList.contains("selected")) {
        button.classList.remove("selected");
        for (let i = 0; i < categoriesArray.length; i++) {
            if (brandsArray[i] === brandName) {
                brandsArray.splice(i, 1);
            }
        }
    }
    else {
        button.classList.add("selected");
        brandsArray.push(brandName);
    }
}


applyFiltersButton.onclick = function () {

    let location = window.location.pathname;

    if (!location.includes('filter')) {
        location += '/filter?';
    }
    else {
        location += '?';
    }

    if (isMan != null) {
        location = addNewFilter(location, "isMan", isMan);
    }

    if (sortType != null) {
        location = addNewFilter(location, "sort", sortType);
    }

    if (categoriesArray.length != 0) {
        location = addNewFilter(location, "categories", categoriesArray.toString());
    }
    else if (allCategory != "-1") {

        location = addNewFilter(location, "categoryType", allCategory);
    }

    if ((selectedMinPrice != null && selectedMinPrice != undefined) || (minPrice != null && minPrice != undefined && minPrice != 0)) {

        let newMinPrice = selectedMinPrice;
        if (minPrice != newMinPrice) {
            newMinPrice = minPrice;
        }
        location = addNewFilter(location, "LowestPrice", newMinPrice);
    }
    if ((selectedMaxPrice != null && selectedMaxPrice != undefined) || (maxPrice != null && maxPrice != undefined && maxPrice != 99999999)) {
        let newMaxPrice = selectedMaxPrice;
        if (maxPrice != newMaxPrice) {
            newMaxPrice = maxPrice;
        }
        location = addNewFilter(location, "HighestPrice", newMaxPrice);
    }
    if (sizesArray.length != 0) {
        location = addNewFilter(location, "Sizes", sizesArray.toString());
    }
    if (brandsArray.length != 0) {
        if (location.includes('filter')) {
            let brandLocation = brandsArray.toString();
            brandLocation = brandLocation.replace('\\', '');
            location = location.replace('filter', 'brands/' + brandLocation + '/filter');
        }
    }

    window.location = location;
}

function addNewFilter(location, filterName, filterValue) {

    if (location.includes(filterName)) {
        location = location.replace(/filterName=*&/gi, filterName + "=" + filterValue + "&");
    }
    if (location.endsWith("?")) {
        location += filterName + '=' + filterValue;
    }
    else {
        location += "&" + filterName + "=" + filterValue;
    }

    return location;
}

function sortProducts(button) {
    let location = window.location.pathname;

    if (!location.includes('filter')) {
        location += '/filter?';
    }
    else {
        location +="?"
    }

    if (sortType != null) {
        params.set('sort', button.dataset.sortoptions);
        window.location = location + params.toString();
    }
    else if (params.size >0) {
        window.location = location + params.toString() + "&sort=" + button.dataset.sortoptions;
    }
    else {
        window.location = location + "sort=" + button.dataset.sortoptions;
    }
}

function removeFilters() {
    if (isMan != null) {
        window.location = location.origin + '/products/filter?isMan=' + isMan;
    }
    else {
        window.location = location.origin + '/products';
    }
}

const pageButtonsContent = document.getElementsByClassName("product-page-button-content");

document.addEventListener('DOMContentLoaded', function () {
    let maxAmount = parseInt(document.getElementById("max-amount-counter")?.dataset.amount);

    if (maxAmount !== maxAmount || maxAmount == undefined || maxAmount == 0) {
        return;
    }

    let pageIndex = 0;
    let pageStartIndex = parseInt(startIndex);

    if (startIndex == 0 || startIndex == null) {
        pageIndex = 1;
        pageStartIndex = 0;
    }
    else {
        pageIndex = (startIndex / 16) + 1;
    }

    if (maxAmount <= 16) {
        pageButtonsContent[0].parentElement.parentElement.style.display = "none";
    }
    else if (maxAmount <= 32) {
        pageButtonsContent[3].parentElement.style.display = "none";
        pageButtonsContent[4].parentElement.style.display = "none";

        if (pageStartIndex + 16 >= maxAmount) {
            pageButtonsContent[1].textContent = pageIndex - 1;
            pageButtonsContent[2].textContent = pageIndex;

            pageButtonsContent[2].parentElement.classList.add("active");
            pageButtonsContent[5].parentElement.style.display = "none";
        }
        else {
            pageButtonsContent[1].textContent = pageIndex;
            pageButtonsContent[2].textContent = pageIndex + 1;

            pageButtonsContent[1].parentElement.classList.add("active");
            pageButtonsContent[0].parentElement.style.display = "none";

        }
    }
    else if (maxAmount <= 48) {
        pageButtonsContent[4].parentElement.style.display = "none";

        if (pageStartIndex + 16 >= maxAmount) {
            pageButtonsContent[1].textContent = pageIndex - 2;
            pageButtonsContent[2].textContent = pageIndex - 1;
            pageButtonsContent[3].textContent = pageIndex;

            pageButtonsContent[3].parentElement.classList.add("active");
            pageButtonsContent[5].parentElement.style.display = "none";
        }
        else if (pageStartIndex + 32 >= maxAmount) {
            pageButtonsContent[1].textContent = pageIndex - 1;
            pageButtonsContent[2].textContent = pageIndex;
            pageButtonsContent[3].textContent = pageIndex + 1;

            pageButtonsContent[2].parentElement.classList.add("active");
        }
        else {
            pageButtonsContent[1].textContent = pageIndex;
            pageButtonsContent[2].textContent = pageIndex + 1;
            pageButtonsContent[3].textContent = pageIndex + 2;

            pageButtonsContent[1].parentElement.classList.add("active");
            pageButtonsContent[0].parentElement.style.display = "none";

        }
    }
    else {
        if (pageStartIndex + 16 >= maxAmount) {
            pageButtonsContent[1].textContent = pageIndex - 3;
            pageButtonsContent[2].textContent = pageIndex - 2;
            pageButtonsContent[3].textContent = pageIndex - 1;
            pageButtonsContent[4].textContent = pageIndex;

            pageButtonsContent[4].parentElement.classList.add("active");
            pageButtonsContent[5].parentElement.style.display = "none";

        }
        else if (pageStartIndex + 32 >= maxAmount) {
            pageButtonsContent[1].textContent = pageIndex - 2;
            pageButtonsContent[2].textContent = pageIndex - 1;
            pageButtonsContent[3].textContent = pageIndex;
            pageButtonsContent[4].textContent = pageIndex + 1;

            pageButtonsContent[3].parentElement.classList.add("active");
        }
        else if (pageStartIndex + 48 >= maxAmount) {
            pageButtonsContent[1].textContent = pageIndex - 1;
            pageButtonsContent[2].textContent = pageIndex;
            pageButtonsContent[3].textContent = pageIndex + 1;
            pageButtonsContent[4].textContent = pageIndex + 2;

            pageButtonsContent[2].parentElement.classList.add("active");
        }
        else {
            pageButtonsContent[1].textContent = pageIndex;
            pageButtonsContent[2].textContent = pageIndex + 1;
            pageButtonsContent[3].textContent = pageIndex + 2;
            pageButtonsContent[4].textContent = pageIndex + 3;

            pageButtonsContent[1].parentElement.classList.add("active");

            if (pageIndex == 1) {
                pageButtonsContent[0].parentElement.style.display = "none";

            }
        }
    }

    for (var i = 1; i < pageButtonsContent.length - 1; i++) {
        let buttonIndex = pageButtonsContent[i].textContent;
        let buttonPageStartIndex = (buttonIndex - 1) * 16;
        pageButtonsContent[i].parentElement.onclick = function () {
            moveToStartIndex(buttonPageStartIndex);
        }
    }
    pageButtonsContent[0].parentElement.onclick = function () {
        let buttonPageStartIndex = (pageIndex - 2) * 16;
        moveToStartIndex(buttonPageStartIndex);
    }

    pageButtonsContent[5].parentElement.onclick = function () {
        let buttonPageStartIndex = pageIndex * 16;
        moveToStartIndex(buttonPageStartIndex);
    }

}, false);

function moveToStartIndex(buttonPageStartIndex) {
    let location = window.location.href;
    let regex = /StartIndex=(\d+)/;
    if (location.includes('brands') && !location.includes('filter')) {
        location += '/filter';
    }
    if (location.match(regex) != null) {
        location = location.replace(regex, 'StartIndex=' + buttonPageStartIndex);
    }
    else if (location.includes('?')) {
        location += '&StartIndex=' + buttonPageStartIndex;
    }
    else {
        location += '?StartIndex=' + buttonPageStartIndex;
    }
    window.location = location;
}

function selectSex(sex) {
    let location = window.location.pathname;

    if (!location.includes('filter')) {
        location += '/filter';
    }
    if (sex == "man") {
        location += "?IsMan=true";

    }
    else {
        location += "?IsMan=false";
    }

    window.location = location;
}

// price input
minPriceInput.addEventListener("change", (element) => {
    setMinPrice(element.srcElement);
})
maxPriceInput.addEventListener("change", (element) => {
    setMaxPrice(element.srcElement);
})
maxPriceInput.addEventListener('keyup mouseup', function (element) {
    setMaxPrice(element.srcElement);
});
minPriceInput.addEventListener('keyup mouseup', function () {
    setMinPrice(element.srcElement);
});

function setMinPrice(element) {
    minPrice = element.value;
}

function setMaxPrice(element) {
    maxPrice = element.value;
}