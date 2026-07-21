$(document).ready(function () {
    $('#btnRegister').click(function () {
        var username = $('#regUsername').val();
        var password = $('#regPassword').val();
        var confirmPassword = $('#regConfirm').val();

        $.ajax({
            url: '/Home/RegisterUser',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ username: username, password: password, confirmPassword: confirmPassword }),
            success: function (data) {
                if (data.success) {
                    alert('Registration Successful!\n\nUsername: ' + username + '\nPassword: ' + password);
                    window.location.href = '/Home/Login';
                } else {
                    alert(data.message);
                }
            },
            error: function () {
                alert('Something went wrong.');
            }
        });
    });
});
