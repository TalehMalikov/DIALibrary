﻿const box = document.getElementById("studentSection");

function handleRadioClick() {
    if (document.getElementById('hide').checked) {
        box.style.display = 'none';
    }
    else {
        box.style.display = 'block'
    }
}

const radioButtons = document.querySelectorAll('input[name="section"]');
radioButtons.forEach(radio => {
    radio.addEventListener('click', handleRadioClick);
});



$((function () {
    var url;
    var redirectUrl;
    var target;

    $('body').append(`
      <div div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Silmək istədiyinizə əminsiz?</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-footer">
                    <button id="cancel-delete" type="button" class="btn btn-secondary" data-dismiss="modal">Ləğv et</button>
                    <button id="confirm-delete" type="button"  class="btn btn-danger">Təsdiq et</button>
                </div>
            </div>
        </div>
      </div>
    `);
    

    /*Delete confirmation*/

    $(".delete").on('click', (e) => {
        e.preventDefault();

        target = e.target;
        var id = $(target).data('id');
        var controller = $(target).data('controller');
        var action = $(target).data('action');
        redirectUrl = $(target).data('redirect-url');

        url = "/" + controller + "/" + action + "/" + id;
        $("#deleteModal").modal('show');
    });

    $("#confirm-delete").on('click', () => {
        $.get(url)
            .done((result) => {
                if (!redirectUrl) {
                    return $(target).parent().parent().hide();
                }
                window.location.href = redirectUrl;
            })
            .fail((error) => {
                if (redirectUrl)
                    window.location.href = redirectUrl;
            }).always(() => {
                $('#deleteModal').modal('toggle');
            });
        $('#deleteModal').modal('toggle');
    });

}()));





