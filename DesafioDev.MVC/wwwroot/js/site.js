$(document).ready(function () {
    $(".open-time-modal").click(function (e) {
        e.preventDefault();
        var timeId = $(this).data("id");

        $.ajax({
            url: "/Home/Time/" + timeId,
            type: "GET",
            success: function (data) {
                $("#timeDetails").html("<p>Carregando...</p>");
                $("#timeDetails").html(data);
                $("#timeModal").modal("show");
            },
            error: function () {
                $("#timeDetails").html("<p>Erro ao carregar os detalhes do time.</p>");
            }
        });
    });

    document.getElementById("timesSelect").addEventListener("change", function () {
        var timeId = this.value;

        if (timeId && timeId !== "0") {
            $("#timeDetails").html("<p>Carregando...</p>");

            $.ajax({
                url: "/Home/Time/" + timeId,
                type: "GET",
                success: function (data) {
                    $("#timeDetails").html(data);
                    $("#timeModal").modal("show");
                },
                error: function () {
                    $("#timeDetails").html("<p>Erro ao carregar os detalhes do time.</p>");
                }
            });
        }
    });
});

function validarSenhas() {
    const senha = document.getElementById("password").value;
    const confirmarSenha = document.getElementById("confirmPassword").value;
    const erro = document.getElementById("senhaErro");

    if (senha !== confirmarSenha) {
        erro.style.display = "block";
        return false;
    }

    erro.style.display = "none";
    return true;
}