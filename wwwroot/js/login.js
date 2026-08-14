$(document).ready(function () {
    $('#btnLogin').click(function () {
        var username = $('#loginUsername').val();
        var password = $('#loginPassword').val();

        $.ajax({
            url: '/Home/Login', 
            type: 'POST',
            data: {
                Username: username,
                Password: password
            },
            success: function (data) {
                if (data.success) {
                    alert('Login successful! Welcome back.');
                    window.location.href = '/';
                } else {
                    alert('Invalid username or password.');
                }
            },
            error: function () {
                alert('Something went wrong.');
            }
        });
    });
});