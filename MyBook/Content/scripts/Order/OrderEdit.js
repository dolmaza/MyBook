$(function () {

    $("#add-new-order-detail").click(function () {
        OrderDetailsGrid.AddNewRow();
        return false;
    });


    $("#deliveryTime").datepicker({
        dateFormat: formatDateJs
    });

    $("#mobile").mask("(599)-999-999");

    $("#order-properties-save")
        .click(function () {
            var url = $(this).attr("href");
            var firstname = $("#firstname").val();
            var lastname = $("#lastname").val();
            var address = $("#address").val();
            var mobile = $("#mobile").val();
            var deliveryTime = $("#deliveryTime").val();
            var note = $("#note").val();

            $.ajax({
                type: "POST",
                url: url,
                data: {
                    Firstname: firstname,
                    Lastname: lastname,
                    Address: address,
                    Mobile: mobile,
                    DeliveryTime: deliveryTime,
                    Note: note
                },
                dataType: "json",
                success: function(response) {
                    if (response.IsSuccess) {
                        alert("success");
                    } else {
                        alert("error");
                    }
                }
            });

            return false;
        });
})