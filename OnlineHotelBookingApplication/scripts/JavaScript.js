$(document).ready(function () {
    $('.edit-on-click').click(function () {
        var $text = $(this),
          $input = $('<input type="text" />')

        $text.hide()
          .after($input);

        $('.controls-update').show();

        $input.val($text.html()).show().focus()
          .keypress(function (e) {
              var key = e.which
              $('.controls-update').click(function () // enter key
              {
                  $input.hide();
                  $text.html($input.val())
                    .show();
                  return false;
              });
          })
          .focusout(function () {
              $input.hide();
              $text.show();
          })
    });

    $('.controls-update').click(function () {
        $('.controls-update').hide();
    });
});
