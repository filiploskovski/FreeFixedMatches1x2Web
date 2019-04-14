$(document).ready(function () {
 
    $.getJSON("/JsonFiles/MonthlyOffers.json",
        function (data) {
            $.each(data,
                function (key, value) {
                    $(".js-monthly-offers").append(
                        '<div class="col-md-12 mb-2">' +
                        '<div class="card h-100">' +
                        '<div class="card-body">' +
                        '<h2 class="card-title">' + value.NameOffer + '</h2>' +
                        '<p class="card-text">' + value.TextOffer + '</p>' +
                        '<h5 class="card-text">Price: ' + value.PriceOffer + ' euros</h5></div></div></div>');
                });
        });
    $.getJSON("/JsonFiles/vipTicketOffers.json",
        function (data) {
            $.each(data,
                function (key, value) {
                    $(".js-vip-offers").append(
                        '<div class="col-md-12 mb-2">' +
                        '<div class="card h-100">' +
                        '<div class="card-body">' +
                        '<h2 class="card-title">' + value.NameOffer + '</h2>' +
                        '<p class="card-text">' + value.TextOffer + '</p>' +
                        '<h5 class="card-text">Price: ' + value.PriceOffer + ' euros</h5></div></div></div>');
                });
        });
});
