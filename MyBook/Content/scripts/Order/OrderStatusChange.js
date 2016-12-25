$(function () {
    $('#close-popup').click(function () {
        window.parent.$.fancybox.close(true);
    });

    $('#save-status').click(function () {
        var statusID = $('#status').val();

        $.ajax({
            type: 'POST',
            url: statusSaveUrl,
            data: {
                StatusID: statusID
            },
            dataType: 'json',
            success: function (response) {
                if(!response.IsSuccess){
                    alert('error');
                } else {
                    window.parent.OrderGrid.Refresh();

                    window.parent.$.fancybox.close(true);
                }
            }
        });
    });
});