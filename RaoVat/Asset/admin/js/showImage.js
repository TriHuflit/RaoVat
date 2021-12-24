
function ShowImage(imageUploader, previewImage) {
   
    if (imageUploader.files) {
        var reader = new FileReader();
        reader.onload = function (e) {         
            $(previewImage).attr('src', e.target.result);          
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}
