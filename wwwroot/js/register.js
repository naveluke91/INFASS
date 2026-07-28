$(document).ready(function () {
    $('#btnRegister').click(function () {
        // Kuhaon ang values gikan sa imong sakto nga IDs
        var username = $('#regUsername').val();
        var password = $('#regPassword').val();
        var confirmPassword = $('#regConfirm').val();

        // I-send padulong sa imong HomeController (Register action)
        $.ajax({
            url: '/Home/Register',
            type: 'POST',
            data: {
                Username: username,
                Password: password,
                ConfirmPassword: confirmPassword
            },
            success: function (data) {
                // Mo-pop up na ang INSERT INTO query diri
                alert(data.queryMessage);
            },
            error: function () {
                alert('Something went wrong sa AJAX.');
            }
        });
    });
});