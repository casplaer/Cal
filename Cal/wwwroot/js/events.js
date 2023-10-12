$(document).ready(function () {
    function update(category) {
        if (category.checked == true) {
            document.getElementsById("color-square").style.display = "block";
        }
        else
        {
            document.getElementsById("color-square").style.display = "none";
        }
    }
});
