var modal = document.getElementById("eventclick-modal");
function closeModal() {
    modal.style.display = "none";
}
$("#previewImage").click(() => {
    modal.style.display = "flex";
})