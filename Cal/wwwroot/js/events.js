function update(category) {
    var _category = category.value;
    if (category.checked == false) {
        document.getElementById("category-text-" + _category).style.display = "none";
        document.getElementById("color-square-" + _category).style.display = "none";
    }
    else {
        document.getElementById("color-square-" + _category).style.display = "block";
        document.getElementById("category-text-" + _category).style.display = "block";
    }
}