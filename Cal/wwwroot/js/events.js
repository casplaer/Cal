function update(category) {
    var categoryColor = category.dataset.color;
    var categoryColorElements = document.getElementsByClassName("color-square-" + categoryColor);
    var categoryTextElements = document.getElementsByClassName("category-text-" + category.value);

    if (category.checked == false)
    {
        for (var i = 0; i < categoryColorElements.length; i++)
        {
            categoryColorElements[i].style.display = "none";
            categoryColorElements[i].style.backgroundColor = "transparent";
        }
        for (var i = 0; i < categoryColorElements.length; i++)
        {
            categoryTextElements[i].style.display = "none";
        }
    }
    else
    {
        for (var i = 0; i < categoryColorElements.length; i++)
        {
            categoryColorElements[i].style.display = "block";
            categoryColorElements[i].style.backgroundColor = categoryColor;
        }
        for (var i = 0; i < categoryTextElements.length; i++)
        {
            categoryTextElements[i].style.display = "block";
        }
    }
}
