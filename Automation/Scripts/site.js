$(document).on("mouseenter", '[data-click=panel-collapse]', function (a) {
    $(this).attr("data-init") || ($(this).tooltip({ title: "Collapse / Expand", placement: "bottom", trigger: "hover", container: "body" }), $(this).tooltip("show"), $(this).attr("data-init", !0))
});

$(document).on("click", "[data-click=panel-collapse]", function (a) {
    a.preventDefault(), $(this).closest(".panel").find(".panel-body").slideToggle()
});


    