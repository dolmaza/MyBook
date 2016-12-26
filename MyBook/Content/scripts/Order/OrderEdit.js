$(function() {

    $("#add-new-order-detail")
        .click(function() {
            OrderDetailsGrid.AddNewRow();
            return false;
        });


    $("#deliveryTime")
        .datepicker({
            dateFormat: formatDateJs,
            minDate: 0
        });

    $("#mobile").mask("(599)-999-999");

    $("#order-properties-save")
        .click(function() {
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
                beforeSend: function () {
                    validation.hideErrors();
                },
                success: function (response) {
                    var data = response.Data;
                    if (response.IsSuccess) {
                        bootbox.alert('<i class="fa fa-check"></i> ' + data.Message);
                    } else if (data.Message) {
                        bootbox.alert('<i class="fa fa-times"></i> ' + data.Message);
                    } else if (data.RedirectUrl) {
                        window.location = data.RedirectUrl;
                    }else if (data.ErrorsJson) {
                        validation.init({
                                errorsJson: data.ErrorsJson
                            }).showErrors();
                    }
                }
            });

            return false;
        });
});