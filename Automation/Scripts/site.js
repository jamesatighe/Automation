﻿$(document).on("mouseenter", '[data-click=panel-collapse]', function (a) {
    $(this).attr("data-init") || ($(this).tooltip({ title: "Collapse / Expand", placement: "bottom", trigger: "hover", container: "body" }), $(this).tooltip("show"), $(this).attr("data-init", !0))
});

$(document).on("click", "[data-click=panel-collapse]", function (a) {
    var ctx = $(this).closest(".cardpanel");
    if (ctx.find(".panel-body").css('display') == 'none') {
        if (ctx.hasClass("pieChart") || ctx.hasClass("barChart")) {
            ctx.css("height", "400px !important");
            a.preventDefault(), ctx.find('.panel-body').slideToggle()
        }
        else {
            ctx.animate({ height: '255' });
            a.preventDefault(), ctx.find(".panel-body").slideToggle()
        }
    }
    else {
        if (ctx.hasClass("pieChart") || ctx.hasClass("barChart")) {
            ctx.css("min-height", "70px !important");
            a.preventDefault(), ctx.find('.panel-body').slideToggle()
            ctx.animate({ height: '70' });
        }
        else {
            a.preventDefault(), ctx.find(".panel-body").slideToggle()
            ctx.animate({ height: '70' });
        }
    }

});



