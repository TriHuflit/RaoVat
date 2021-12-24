$("#Birth").change(function () {
  
    var dateString = document.getElementById("Birth").value;
    var regex = /(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
    var sender = document.getElementById("sender");
   
    if (regex.test(dateString)) {

        var parts = dateString.split("/");
        var dtDOB = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
        var dtCurrent = new Date();

        if (dtCurrent.getFullYear() - dtDOB.getFullYear() < 15) {
            sender.innerHTML = "Bạn phải ít nhất 15 tuổi trở lên !!!"
            $("#btn-infouser").prop("disabled", "true");
            return;
        }

        if (dtCurrent.getFullYear() - dtDOB.getFullYear() == 15) {

           
            if (dtCurrent.getMonth() < dtDOB.getMonth()) {
                sender.innerHTML = "Bạn phải ít nhất 15 tuổi trở lên !!!"
                $("#btn-infouser").prop("disabled", "true");
                return;
            }
            if (dtCurrent.getMonth() == dtDOB.getMonth()) {
              
                if (dtCurrent.getDate() < dtDOB.getDate()) {
                    sender.innerHTML = "Bạn phải ít nhất 15 tuổi trở lên !!!"
                    $("#btn-infouser").prop("disabled", "true");
                    return;
                }
            }
        }
        $("#btn-infouser").removeAttr("disabled");
        sender.innerHTML = "";
    } else {
        sender.innerHTML = "Enter date in dd/MM/yyyy format ONLY."

    }
}
)