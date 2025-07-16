function OpenInNewTab(url) {
    var win = window.open(url, '_blank');
    win.focus();
}

function AutoWidthForDisabledSelect() {

    $('select').each(function () {
        //alert($(this).attr('multiple'));
        if ($(this).attr('disabled') == "disabled") {
            $(this).css("width", "auto");
            //$(this).removeAttr('disabled');
            //$(this).attr('readonly', 'readonly');
        }

    });
}

function AutoWidthForDisabledSelectOnLoad() {
    $(document).ready(function () {
        AutoWidthForDisabledSelect();
    });
}

