function update(category) {
    var _category = category.value;
    var categoryColorElements = document.getElementsByClassName("color-square-" + _category);
    var categoryTextElements = document.getElementsByClassName("category-text-" + _category);

    // Получите цвет категории из словаря, используя _category как ключ.
    var categoryColor = categoryColorDictionary[_category];

    if (category.checked == false) {
        for (var i = 0; i < categoryColorElements.length; i++) {
            categoryColorElements[i].style.display = "none";
            categoryColorElements[i].style.backgroundColor = "transparent"; // Очистите цвет
        }
        for (var i = 0; i < categoryTextElements.length; i++) {
            categoryTextElements[i].style.display = "none";
        }
    } else {
        for (var i = 0; i < categoryColorElements.length; i++) {
            categoryColorElements[i].style.display = "block";
            categoryColorElements[i].style.backgroundColor = categoryColor; // Установите цвет
        }
        for (var i = 0; i < categoryTextElements.length; i++) {
            categoryTextElements[i].style.display = "block";
        }
    }
}
