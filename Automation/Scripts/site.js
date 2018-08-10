$(document).on("mouseenter", '[data-click=panel-collapse]', function (a) {
    $(this).attr("data-init") || ($(this).tooltip({ title: "Collapse / Expand", placement: "bottom", trigger: "hover", container: "body" }), $(this).tooltip("show"), $(this).attr("data-init", !0))
});

$(document).on("mouseenter", '[data-click=column-remove]', function (a) {
    $(this).attr("data-init") || ($(this).tooltip({ title: "Remove Column", placement: "bottom", trigger: "hover", container: "body" }), $(this).tooltip("show"), $(this).attr("data-init", !0))
});

$(document).on("click", "[data-click=column-remove]", function (a) {
    $(this).tooltip("hide");
    $(this).closest('.column').remove();
})

$(document).on("click", "[data-click=column-collapse]", function (a) {
    $(this).tooltip("hide");
    $(this).closest('.column').slideToggle();
})

$(document).on("click", "[data-click=threshold-edit]", function (a) {
    $(this).tooltip("hide");

    $('#threshold').load('/SCOM/CreateModal?metricType=SCOMNew&Type=Edit');
    setTimeout(function () {
        var warning = $('#SCOMPartial .SCOMNew').find('#panel-threshold #threshold-warning')[0].innerHTML;
        var error = $('#SCOMPartial .SCOMNew').find('#panel-threshold #threshold-error')[0].innerHTML;
        $('#ThresholdModal').find('#warning')[0].value = warning;
        $('#ThresholdModal').find('#error')[0].value = error;
        $('#ThresholdModal').modal('show');
    }, 1000);
})

$(document).on("click", "[data-click=threshold-edit-SCOMClusterHealth]", function (a) {
    $(this).tooltip("hide");
    var warningw = $(this).parents('.cardpanel').find('#panel-threshold #threshold-warning-w')[0].innerHTML;
    var warninge = $(this).parents('.cardpanel').find('#panel-threshold #threshold-warning-e')[0].innerHTML;
    var errorw = $(this).parents('.cardpanel').find('#panel-threshold #threshold-error-w')[0].innerHTML;
    var errore = $(this).parents('.cardpanel').find('#panel-threshold #threshold-error-e')[0].innerHTML;
    $('#threshold').load('/SCOM/CreateModal?metricType=split&Type=Edit&content=SCOMClusterHealth');

    setTimeout(function () {

        $('#ThresholdModal').find('#warningw')[0].value = warningw;
        $('#ThresholdModal').find('#warninge')[0].value = warninge;
        $('#ThresholdModal').find('#errorw')[0].value = errorw;
        $('#ThresholdModal').find('#errore')[0].value = errore;
        $('#ThresholdModal').modal('show');
    }, 1000);
})

$(document).on("click", "[data-click=threshold-edit-SCOMDFSHealth]", function (a) {
    $(this).tooltip("hide");
    var warningw = $(this).parents('.cardpanel').find('#panel-threshold #threshold-warning-w')[0].innerHTML;
    var warninge = $(this).parents('.cardpanel').find('#panel-threshold #threshold-warning-e')[0].innerHTML;
    var errorw = $(this).parents('.cardpanel').find('#panel-threshold #threshold-error-w')[0].innerHTML;
    var errore = $(this).parents('.cardpanel').find('#panel-threshold #threshold-error-e')[0].innerHTML;
    $('#threshold').load('/SCOM/CreateModal?metricType=split&Type=Edit&content=SCOMDFSHealth');

    setTimeout(function () {

        $('#ThresholdModal').find('#warningw')[0].value = warningw;
        $('#ThresholdModal').find('#warninge')[0].value = warninge;
        $('#ThresholdModal').find('#errorw')[0].value = errorw;
        $('#ThresholdModal').find('#errore')[0].value = errore;
        $('#ThresholdModal').modal('show');
    }, 1000);
})

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

$(document).on("click", "[data-click=panel-remove]", function (a) {
    var ctx = $(this).tooltip("hide");
    ctx.closest(".cardpanel").remove();
});



