$(function () {
    $('#orders-grid').on('click', '.order-status-change-button', function () {
        var url = $(this).attr('href');
        
        FancyBox.Init({
            href: url,
            width: 500,
            height: 227,
            closeBtn: false
        }).ShowPopup();

        return false;
    });

    $('#orders-grid').on('click', '.order-paper', function () {
        var url = $(this).attr('href');

        FancyBox.Init({
            href: url,
            width: 800,
            height: 540,
            closeBtn: false
        }).ShowPopup();

        return false;
    });

});