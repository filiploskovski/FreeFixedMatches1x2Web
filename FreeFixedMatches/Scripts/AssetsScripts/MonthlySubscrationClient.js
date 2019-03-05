
var list = [];
$(document).ready(function () {
    $.getJSON("/JsonFiles/MonthlyOffers.json", function (result) {
        $.each(result, function (key, value) {
            list.push(value);
            $(".js-Offers").append(
                '<div class="col-md-4 mb-5"><div class="card h-100"><div class="card-body">' +
                '<h2 class="card-title">' + value.NameOffer + '</h2>' +
                '<p class="card-text">' + value.TextOffer +
                '</p></div></div></div>'
            );
        });
    });
});