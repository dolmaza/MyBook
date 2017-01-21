$(function() {
    $('#orders-grid')
        .on('click',
            '.order-status-change-button',
            function() {
                var url = $(this).attr('href');

                FancyBox.Init({
                        href: url,
                        width: 500,
                        height: 227,
                        closeBtn: false
                    })
                    .ShowPopup();

                return false;
            });

    $('#orders-grid')
        .on('click',
            '.order-paper',
            function() {
                var url = $(this).attr('href');

                FancyBox.Init({
                        href: url,
                        width: 800,
                        height: 540,
                        closeBtn: false
                    })
                    .ShowPopup();

                return false;
            });

    $('#archive-orders')
        .click(function () {
            var url = $(this).attr('data-url');
            var orderIDs = OrderGrid.GetSelectedKeysOnPage();

            $.ajax({
                type: 'POST',
                url: url,
                data: {
                    orderIDs: orderIDs
                },
                dataType: 'json',
                success: function(response) {
                    if (response.IsSuccess) {
                        OrderGrid.Refresh();
                        bootbox.alert('<i class="fa fa-check"></i> ' + response.Data.Message);
                    } else {
                        bootbox.alert('<i class="fa fa-times"></i> ' + response.Data.Message);
                    }
                }
            });

            console.log(orderIDs);

        });

});