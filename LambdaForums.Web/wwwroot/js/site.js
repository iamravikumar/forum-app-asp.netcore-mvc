
// Get file name after upload
$(".custom-file-input").on("change", function() {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

// Make button submit active after upload image
$(".custom-file-input").on("change", function () {
    var eleman = document.getElementById("registerBtnProfile");
    eleman.removeAttribute("disabled"); 
});