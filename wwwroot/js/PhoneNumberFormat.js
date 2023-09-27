function phonenumberFormat(inputField) {
    console.log("Ejecutando phonenumber script");

    var phoneNumber = inputField.value.replace(/\D/g, ''); // Elimina caracteres no numéricos

    // Limita la longitud del número de teléfono a 10 dígitos
    phoneNumber = phoneNumber.slice(0, 10);

    if (phoneNumber.length >= 3 && phoneNumber.length <= 6) {
        inputField.value = phoneNumber.slice(0, 3) + '-' + phoneNumber.slice(3);
    } else if (phoneNumber.length >= 7) {
        inputField.value = phoneNumber.slice(0, 3) + '-' + phoneNumber.slice(3, 6) + '-' + phoneNumber.slice(6, 10);
    }
}
