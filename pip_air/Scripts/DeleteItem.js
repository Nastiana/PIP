$('.icon-create').click(function () {
    var item = $(this);
    bootbox.dialog({
        size: "small",
        title: 'Удалить запись?',
        message: "Delete?",
        onEscape: true,
        backdrop: true,
        buttons: {
            cancel: {
                label: "Нет",
            },
            yes: {
                label: "Да",
                className: 'btn-danger',
                callback: function () {
                    $.post(deleteUrl, { id: item.parents('.card').attr('id') });
                    item.parents('.card').remove();
                    console.log('deleted');
                }
            },
        }
    });
});