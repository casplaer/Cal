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
        for (var i = 0; i < categoryColorElements.length; i++)
        {
            categoryTextElements[i].style.display = "block";
        }
    }
}

function updateIndex(category) {
    var listItems = document.querySelectorAll("li[class^='element-" + category.value + "']");
    var listDescriptionItems = document.querySelectorAll("div[class^='description description-" + category.value + "']");

    if (category.checked == false) {
        for (var i = 0; i < listItems.length; i++) {
            listItems[i].style.display = "none";
            if (listDescriptionItems[i].style.display === "block") {
                listDescriptionItems[i].style.display = "none";
            }
        }
    }
    else {
        for (var i = 0; i < listItems.length; i++) {
            listItems[i].style.display = "flex";
        }
    }
}
function toggleDescription(eventId) {
    var descriptionElement = document.querySelector("#description-" + eventId);
    if (descriptionElement.style.display === "none" || descriptionElement.style.display === "") {
        descriptionElement.style.display = "block";
    } else {
        descriptionElement.style.display = "none";
    }
}

