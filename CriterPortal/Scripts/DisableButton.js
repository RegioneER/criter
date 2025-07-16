

function disableBtnPomiager(btn, newText) {
    var $btn = $(btn);

    if ($btn.attr("isSubmitting"))
        return false;

    $btn.attr("isSubmitting", "true");
    $btn.val(newText);
    return true;
}

function disableBtn(btnID, newText) {

    var btn = document.getElementById(btnID);
    setTimeout("setImage('" + btnID + "')", 10);
    btn.disabled = true;
    btn.value = newText;
}

function setImage(btnID) {
    var btn = document.getElementById(btnID);
    //btn.style.background = 'url(12501270608.gif)';
}