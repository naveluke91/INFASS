$(document).ready(function () {

    // Load accounts into the table
    function loadAccounts() {
        $.ajax({
            url: '/Home/GetAccounts',
            type: 'GET',
            success: function (data) {
                var tbody = $('#accountsTableBody');
                tbody.empty();

                if (data.accounts.length === 0) {
                    tbody.append('<tr><td colspan="4" class="text-center text-muted">No accounts yet. Click "Add Account" to insert one.</td></tr>');
                } else {
                    $.each(data.accounts, function (i, acc) {
                        var maskedPw = '*'.repeat(acc.password.length);
                        var row = `
                            <tr>
                                <td>${acc.index + 1}</td>
                                <td>${acc.username}</td>
                                <td>${maskedPw}</td>
                                <td class="text-center">
                                    <button class="btn btn-sm btn-info text-white me-1 btn-view"
                                        data-index="${acc.index}"
                                        data-username="${acc.username}"
                                        data-password="${acc.password}">
                                        <i class="fas fa-eye"></i> View
                                    </button>
                                    <button class="btn btn-sm btn-warning me-1 btn-update"
                                        data-index="${acc.index}"
                                        data-username="${acc.username}">
                                        <i class="fas fa-edit"></i> Update
                                    </button>
                                    <button class="btn btn-sm btn-danger btn-delete"
                                        data-index="${acc.index}"
                                        data-username="${acc.username}">
                                        <i class="fas fa-trash"></i> Delete
                                    </button>
                                </td>
                            </tr>`;
                        tbody.append(row);
                    });
                }
            }
        });
    }

    // Load on page ready
    loadAccounts();

    // INSERT - Add Account button in the modal
    $('#btnConfirmInsert').click(function () {
        var username = $('#insertUsername').val();
        var password = $('#insertPassword').val();

        if (!username || !password) {
            alert('Please fill in all fields.');
            return;
        }

        $.ajax({
            url: '/Home/InsertAccount',
            type: 'POST',
            data: { Username: username, Password: password },
            success: function (data) {
                if (data.success) {
                    bootstrap.Modal.getOrCreateInstance(document.getElementById('insertAccountModal')).hide();
                    $('#insertUsername').val('');
                    $('#insertPassword').val('');
                    loadAccounts();
                } else {
                    alert('Insert failed.');
                }
            }
        });
    });

    // VIEW button
    $(document).on('click', '.btn-view', function () {
        $('#viewId').text($(this).data('index') + 1);
        $('#viewUsername').text($(this).data('username'));
        $('#viewPassword').text($(this).data('password'));
        new bootstrap.Modal(document.getElementById('viewAccountModal')).show();
    });

    // UPDATE button - open modal pre-filled
    $(document).on('click', '.btn-update', function () {
        $('#updateIndex').val($(this).data('index'));
        $('#updateUsername').val($(this).data('username'));
        $('#updatePassword').val('');
        new bootstrap.Modal(document.getElementById('updateAccountModal')).show();
    });

    // CONFIRM UPDATE
    $('#btnConfirmUpdate').click(function () {
        $.ajax({
            url: '/Home/UpdateAccount',
            type: 'POST',
            data: {
                index: $('#updateIndex').val(),
                newUsername: $('#updateUsername').val(),
                newPassword: $('#updatePassword').val()
            },
            success: function (data) {
                if (data.success) {
                    bootstrap.Modal.getOrCreateInstance(document.getElementById('updateAccountModal')).hide();
                    loadAccounts();
                } else {
                    alert('Update failed.');
                }
            }
        });
    });

    // DELETE button - open modal
    $(document).on('click', '.btn-delete', function () {
        $('#deleteIndex').val($(this).data('index'));
        $('#deleteUsername').text($(this).data('username'));
        new bootstrap.Modal(document.getElementById('deleteAccountModal')).show();
    });

    // CONFIRM DELETE
    $('#btnConfirmDelete').click(function () {
        $.ajax({
            url: '/Home/DeleteAccount',
            type: 'POST',
            data: { index: $('#deleteIndex').val() },
            success: function (data) {
                if (data.success) {
                    bootstrap.Modal.getOrCreateInstance(document.getElementById('deleteAccountModal')).hide();
                    loadAccounts();
                } else {
                    alert('Delete failed.');
                }
            }
        });
    });

});
