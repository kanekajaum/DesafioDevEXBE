document.getElementById("timesSelect").addEventListener("change", function () {
    var timeId = this.value;

    if (timeId && timeId !== "0") {
        var form = document.createElement("form");
        form.method = "POST";
        form.action = "/Home/Time";

        var input = document.createElement("input");
        input.type = "hidden";
        input.name = "id";
        input.value = timeId;

        form.appendChild(input);
        document.body.appendChild(form);
        form.submit();
    }
});