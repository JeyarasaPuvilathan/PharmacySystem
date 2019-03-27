$(document).ready(function () {
    $('input[type="password"]').after('<input type="checkbox" class="sp_checkbox"/>show password');
    $('.sp_checkbox').change(function () {
        var prev = $(this).prev();

        var value = $(this).val();
        var type = prev.attr('type');

        var name = prev.attr('name');
        var id = prev.attr('id');

        var new_type = (type == 'password') ? 'text' : 'password';
        prev.remove();
        $(this).before('<input type="' + new_type + '" value="' + value + '" id="' + id + '" />');


    });
});