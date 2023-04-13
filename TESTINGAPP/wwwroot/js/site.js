var deletePhotoCheckbox = document.getElementById("DeletePhoto");
var uploadPhotoDiv = document.getElementById("upload-photo");
deletePhotoCheckbox.addEventListener("change", function () {
    if (deletePhotoCheckbox.checked) {
        uploadPhotoDiv.style.display = "block";
    } else {
        uploadPhotoDiv.style.display = "none";
    }
});
