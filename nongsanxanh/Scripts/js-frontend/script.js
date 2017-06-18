
/**
 * @function      Include
 * @description   Includes an external scripts to the page
 * @param         {string} scriptUrl
 */
function include(scriptUrl) {
    document.write('<script src="' + scriptUrl + '"></script>');
}


(function ($) {
    var o = $('.rd-navbar');
    if (o.length > 0) {
        include('/Scripts/js-frontend/jquery.rd-navbar.min.js'); 

        $(document).ready(function () {
            o.RDNavbar({
                stickUpClone: false,
                stickUpOffset: 170
            });
        });
    }
})(jQuery);