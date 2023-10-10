$(document).ready(function() {
    $('.food-images').hide();


    selectCategory(document.querySelector('select'));
});


function selectCategory(selectElement) {
    var selectedCategory = selectElement.value;

    $('.food-images').hide()

    if (selectedCategory == "Burgers") {
        $('.burgers-images').show()
        $('.select-image-message').hide()
    } else if (selectedCategory == "Pasta") {
        $('.pastas-images').show()
        $('.select-image-message').hide()
    } else if (selectedCategory == "Desserts") {
        $('.desserts-images').show()
        $('.select-image-message').hide()
    }

    console.log(selectedCategory)
}

function selectImage(image) {

    console.log("image script")
    let selectedValue = image.getAttribute("data-value");

    let radioButton = document.querySelector('input[name="profileAvatar"][value="' + selectedValue + '"]');

    if (radioButton) {
        radioButton.checked = true;
    }

    let itemImage = document.getElementById("defaultItemImage");

    itemImage.src = "/images/Comida/" + selectedValue
}

function resetAll() {
    let itemImage = document.getElementById("defaultItemImage");

    itemImage.src = "/images/Comida/default-food.png"
}



// Obtén el elemento del input
var numberInput = document.getElementById('priceInput');

// Deshabilita la entrada de texto
numberInput.addEventListener('keydown', function (event) {
    //Se pueden usar las flechas del teclado

    if ([37, 38, 39, 40, 13].indexOf(event.keyCode) === -1) {
        event.preventDefault()
    }
});