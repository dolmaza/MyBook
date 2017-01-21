$(function () {
    
    $('#orders-grid')
        .on('click',
            '.order-paper',
            function () {
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

    $('#unarchive-orders')
        .click(function () {
            var url = $(this).attr('data-url');
            var orderIDs = OrderArchivedGrid.GetSelectedKeysOnPage();

            $.ajax({
                type: 'POST',
                url: url,
                data: {
                    orderIDs: orderIDs
                },
                dataType: 'json',
                success: function (response) {
                    if (response.IsSuccess) {
                        OrderArchivedGrid.Refresh();
                        bootbox.alert('<i class="fa fa-check"></i> ' + response.Data.Message);
                    } else {
                        bootbox.alert('<i class="fa fa-times"></i> ' + response.Data.Message);
                    }
                }
            });

            console.log(orderIDs);

        });

});