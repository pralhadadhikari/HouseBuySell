"use strict";

function initMenu() {
    $("#menu ul").hide(), $("#menu ul").children(".current").parent().show(), $("#menu li a").click(function () {
        var e = $(this).next();
        return (!e.is("ul") || !e.is(":visible")) && (e.is("ul") && !e.is(":visible") ? ($("#menu ul:visible").slideUp("normal"), e.slideDown("normal"), !1) : void 0)
    });
}


$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

$("#menu-toggle-2").click(function (e) {    
    e.preventDefault();
    $("#wrapper").toggleClass("toggled-2");
    $("#menu ul").hide();
});

$(document).ready(function () {
    initMenu();
});