var numberInput = document.getElementById('numberInput');

// Deshabilita la entrada de texto
numberInput.addEventListener('keydown', function (event) {
    //Se pueden usar las flechas del teclado

    if ([37, 38, 39, 40, 13].indexOf(event.keyCode) === -1) {
        event.preventDefault()
    }
});

function setFieldsValue(button) {
    var itemName = button.getAttribute('data-item-name');
    var itemQty = button.getAttribute('data-quantity');
    var itemPhotoName = button.getAttribute('data-item-img')

    var itemQuantityInput = document.querySelector('#editModal input[name="quantity"]');
    var itemNameInput = document.querySelector('#editModal input[name="itemName"]');
    var itemImg = document.querySelector('#editModel #itemPhoto')

    itemNameInput.value = itemName;
    itemQuantityInput.value = itemQty;

    let itemImage = document.getElementById("itemPhoto");
    itemImage.src = "/images/Comida/" + itemPhotoName;
}


function confirmDelete() {
    return confirm("Are you sure you want to delete this item from your order?");
}