var img = document.getElementsByClassName("product_info-img");
var i;
for (i = 0; i < img.length; i++) {

    img[i].onclick = function () {
        modal.style.display = "flex";
        var p = document.getElementById("Message-Comfirm");
        p.innerHTML = '';
        var image = document.createElement("img");
        image.src = this.src;
        image.style.height = "100%";
        image.style.width = "100%"
        image.style.objectFit = "cover";
        p.appendChild(image);
    }

}
var modal = document.getElementById("eventclick-modal");
function closeModal() {
    modal.style.display = "none";
}