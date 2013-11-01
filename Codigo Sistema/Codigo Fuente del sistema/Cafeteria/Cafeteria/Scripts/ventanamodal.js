$(function () {
    $('.modal').click(function () {
        $('<div/>').appendTo('body').dialog({
            close: function (event, ui) {
                dialog.remove();
            },
            modal: true,
            width: 300,
            title: "Asignar Perfiles ",
            position: "center",

        }).load(this.href, {});

        return false;
    });
});