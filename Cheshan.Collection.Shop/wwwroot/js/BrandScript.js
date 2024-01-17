const brandPAgeparams = new URLSearchParams(window.location.search);
const brandName = params.get('brandNames');


function selectBrandSex(brandSex) {
    let location = window.location.pathname;

    if (!location.includes('filter')) {
        location += '/filter';
    }
    if (brandSex == "man") {
        location += "?isMan=true";

    }
    else {
        location += "?isMan=false";
    }

    window.location = location;
}