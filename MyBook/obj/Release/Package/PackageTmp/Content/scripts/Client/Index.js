$(function () {
    $("#add-new").click(function () {
        ClientGrid.AddNewRow();
        return false;
    });

    $("#clients-grid")
        .on("click", ".create-order", function () {
            var url = $(this).attr("href");
            var clientID = $(this).attr("data-client-id");
            
            $.ajax({
                type: "POST",
                url: url,
                data: {
                    clientID: clientID
                },
                dataType: "json",
                success: function(response) {
                    var data = response.Data;
                    if (data.RedirectUrl) {
                        window.location = data.RedirectUrl;
                    } else {
                        alert(data.Message);
                    }
                }
            });

            return false;
        });
});