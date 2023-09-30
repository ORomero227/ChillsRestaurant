function phonenumberFormat(inputField) {
    console.log("Ejecutando phonenumber script");

    let phoneNumber = inputField.value.replace(/\D/g, ''); // Elimina caracteres no numéricos

    // Limita la longitud del número de teléfono a 10 dígitos
    phoneNumber = phoneNumber.slice(0, 10);

    if (phoneNumber.length >= 3 && phoneNumber.length <= 6) {
        inputField.value = phoneNumber.slice(0, 3) + '-' + phoneNumber.slice(3);
    } else if (phoneNumber.length >= 7) {
        inputField.value = phoneNumber.slice(0, 3) + '-' + phoneNumber.slice(3, 6) + '-' + phoneNumber.slice(6, 10);
    }
}

function selectAvatar(image) {

    console.log("image script")
    let selectedValue = image.getAttribute("data-value");

    let radioButton = document.querySelector('input[name="profileAvatar"][value="' + selectedValue + '"]');

    if (radioButton) {
        radioButton.checked = true;
    }

    let profileImage = document.getElementById("defaultAvatar");

    profileImage.src = "/images/profileAvatars/" + selectedValue
}

function resetAll() {
    let profileImage = document.getElementById("defaultAvatar");

    profileImage.src = "/images/profileAvatars/avatar-default.png"
}