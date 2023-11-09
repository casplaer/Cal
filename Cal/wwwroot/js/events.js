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

function checkSelectedEvents() {
    const checkboxes = document.querySelectorAll('input[type="checkbox"][name="selectedEvents"]');
    const shareButton = document.getElementById("checkboxButton");

    let atLeastOneChecked = false;

    checkboxes.forEach(function (checkbox) {
        if (checkbox.checked) {
            atLeastOneChecked = true;
        }
    });

    if (atLeastOneChecked) {
        shareButton.style.backgroundColor = 'black';
        shareButton.style.borderColor = 'black';
        shareButton.disabled = false;
    } else {
        shareButton.style.backgroundColor = 'grey';
        shareButton.style.borderColor = 'grey';
        shareButton.disabled = true;
    }
}

const checkboxes = document.querySelectorAll('input[type="checkbox"][name="selectedEvents"]');
checkboxes.forEach(function (checkbox) {
    checkbox.addEventListener('change', checkSelectedEvents);
});

window.onload = checkSelectedEvents;

function closeC() {
    var newCategoryForm = document.getElementById('newCategoryForm');
    newCategoryForm.style.display = 'block';
    document.getElementById('chooseCategory').style.display = 'none';
    document.getElementById('addCategoryButton').style.display = 'none';
    document.getElementById('backButton').style.display = 'block';
}

function backC() {
    var newCategoryForm = document.getElementById('newCategoryForm');
    newCategoryForm.style.display = 'none';
    document.getElementById('chooseCategory').style.display = 'block';
    document.getElementById('addCategoryButton').style.display = 'block';
    document.getElementById('backButton').style.display = 'none';
}

function myFunction() {
    // Get the text field
    var copyText = document.getElementById("myInput");

    // Select the text field
    copyText.select();

    // Copy the text inside the text field
    navigator.clipboard.writeText(copyText.value);

    // Alert the copied text
    alert("Ссылка успешно скопирована!");
}