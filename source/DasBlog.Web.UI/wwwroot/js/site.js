
function commentManagement(url, commentText, httpVerb) {
    if (confirm(commentText)) {
        var oReq = new XMLHttpRequest();

        oReq.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                location.href = url;
            }
        };

        oReq.open(httpVerb, url);
        oReq.send();
    }
}

function deleteEntry(entryUrl, entryTitle) {
    if (confirm("Are you sure you want to delete this item? \n\n" + entryTitle)) {
        location.href = entryUrl;
    }
}

function linkToUser(emailAddress, linkAbility) {
    if (linkAbility === "disabled") {
        alert("You must complete the current operation (create, edit or delete user), before you can select another");
        return;
    }
    location.href = emailAddress;
}

function showLastUserError(showError) {
    if (showError) {
        alert("You can't delete your own user record or reduce its privileges");
        return;
    }
    window.maintenanceForm.submit();
}

(function ($) {
    'use strict';

    var navSearch = $('.main-nav__search');
    var popupSearch = $('.search-popup');
    var popupSearchClose = $('.search-popup__close');

    var navToggle = $('.nav-toggle__icon');
    var nav = $('.main-nav');
    var contentOverlay = $('.content-overlay');

    navSearch.on('click', function () {
        popupSearch.addClass('search-popup--active').find('input[type="text"]').focus();
    });

    popupSearchClose.on('click', function () {
        popupSearch.removeClass('search-popup--active');
    });

    navToggle.on('click', function () {
        nav.addClass('main-nav--mobile');
        contentOverlay.addClass('content-overlay--active');
    });

    contentOverlay.on('click', function () {
        nav.removeClass('main-nav--mobile');
        contentOverlay.removeClass('content-overlay--active');
    });

})(jQuery);
